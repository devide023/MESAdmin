﻿{
  isgradequery: true,
  isoperate: true,
  operate_fnlist: [{
      label: '上传',
      btntype: 'upload',
      action: 'http://172.16.201.125:7002/api/upload/dzgy_pdf',
      callback: function (response, file) {
        if (response.code === 1) {
          this.$loading().close();
          this.$message.success(response.msg);
          let rowid = response.extdata.rowkey;
          let finditem = this.$basepage.list.find(function (i) {
            return i.rowkey === rowid;
          });
          if (finditem) {
            finditem.jwdx = file.size;
            finditem.gymc = file.name;
            finditem.gybh = '';
            finditem.wjlj = response.files[0].fileid;
            finditem.scry = this.$store.getters.name;
            finditem.scsj = this.$parseTime(new Date());
          }
        } else {
          this.$message.error(response.msg);
        }
      },
      condition: [{
          field: 'wjfl',
          oper: '=',
          val: '工艺'
        }
      ]
    }, {
      label: '下载',
      fnname: 'download_dzgypdf',
      btntype: 'text',
      condition: [{
          field: 'wjfl',
          oper: '=',
          val: '工艺'
        }, {
          field: 'jwdx',
          oper: '>',
          val: 0
        }
      ]
    }, {
      label: '查看',
      fnname: 'view_dzgypdf',
      btntype: 'text',
      condition: [{
          field: 'wjfl',
          oper: '=',
          val: '工艺'
        }, {
          field: 'jwdx',
          oper: '>',
          val: 0
        }
      ]
    }, {
      label: '上传',
      btntype: 'uploadvideo',
      action: 'http://172.16.201.125:7002/api/upload/video',
      callback: function (response, file) {
        if (response.code === 1) {
          this.$loading().close();
          this.$message.success(response.msg);
          let rowid = response.extdata.rowkey;
          let finditem = this.$basepage.list.find(function (i) {
            return i.rowkey === rowid;
          });
          if (finditem) {
            finditem.jwdx = file.size;
            finditem.gymc = file.name;
            finditem.wjlj = response.files[0].fileid;
            finditem.scry = this.$store.getters.name;
            finditem.scsj = this.$parseTime(new Date());
          }
        } else {
          this.$message.error(response.msg);
        }
      },
      condition: [{
          field: 'wjfl',
          oper: '=',
          val: '视频'
        }
      ]
    }, {
      label: '下载',
      fnname: 'download_dzgyMp4',
      btntype: 'text',
      condition: [{
          field: 'wjfl',
          oper: '=',
          val: '视频'
        }, {
          field: 'jwdx',
          oper: '>',
          val: 0
        }
      ]
    }, {
      label: '查看',
      fnname: 'view_dzgymp4',
      btntype: 'text',
      condition: [{
          field: 'wjfl',
          oper: '=',
          val: '视频'
        }, {
          field: 'jwdx',
          oper: '>',
          val: 0
        }
      ]
    }
  ],
  isbatoperate: true,
  isfresh: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.adduser = this.$store.getters.name;
      row.addtime = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_dzgypdf: function (row) {
      var _this = this;
      this.$request('get', "download/ftp2web", {
        wjlx: '电子工艺',
        wjlj: row.wjlj
      }).then(function (res) {
        if (res.code === 1) {
          window.open("http://172.16.201.125:7002/api/download/downloadpdf?wjlj=" + row.wjlj);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
    view_dzgypdf: function (row) {
      window.open("http://172.16.201.125:81/电子工艺/" + row.wjlj);
    },
    download_dzgyMp4: function (row) {
      var _this = this;
      this.$request('get', "download/ftp2web", {
        wjlx: '视频',
        wjlj: row.wjlj
      }).then(function (res) {
        if (res.code === 1) {
          window.open("http://172.16.201.125:7002/api/download/downloadpdf?wjlj=" + row.wjlj);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
    view_dzgymp4: function (row) {
      window.open("http://172.16.201.125:81/视频/" + row.wjlj);
    },
    select_scx: function (collist, val, row) {
      var _this = this;
      row.scxzx = '';
      this.$request('get', '/lbj/baseinfo/getscxzx', {
        scx: val
      }).then(function (res) {
        if (res.code === 1) {
          row.scxzxs = res.list.map(function (i) {
            return {
              label: i.scxzxmc,
              value: i.scxzx
            };
          });
        } else {
          _this.$message.error(res.msg);
        }
      });
    }
  },
  batoperate: {
    import_by_add: function (vm, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          vm.$request('get', '/lbj/dzgy/readxls', {
            fileid: fid
          }).then(function (result) {
            vm.$loading().close();
            if (result.code === 1) {
              vm.$message.success(result.msg);
              vm.getlist(vm.queryform);
            } else if (result.code === 0) {
              vm.$message.error(result.msg);
            }
          });
        } catch (error) {
          vm.$message.error(error);
        }
      } else {
        vm.$loading().close();
      }
    },
    export_excel(_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields, expdatalist);
        } else if (res.code === 0) {
          this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {},
    import_by_zh(_this, res) {},
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'left',
      width: 120,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: [],
      change_fn_name: 'select_scx',
      sortable: true,
    }, {
      coltype: 'list',
      label: '线别',
      prop: 'scxzx',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      optionconfig: {
        method: 'get',
        url: '/lbj/baseinfo/scxzx',
        querycnf: [{
            scx: 'scx'
          }
        ]
      },
      width: 80,
      relation: 'scxzxs',
      hideoptionval: true,
    }, {
      coltype: 'string',
      prop: 'gybh',
      label: '工艺编号',
      headeralign: 'center',
      align: 'center',
      width: 150,
      overflowtooltip: true,
      sortable: true,
    }, {
      coltype: 'list',
      prop: 'statusno',
      label: '产品',
      headeralign: 'center',
      align: 'center',
      width: 150,
      multiple: false,
      sortable: true,
      allowcreate: true,
      filterable: true,
      remote: function (q, _this, row) {
        if (q) {
          if (q.length >= 3) {
            _this.$request('get', '/lbj/baseinfo/wlbm_by_key', {
              key: q
            }).then(function (res) {
              if (res.code === 1) {
                row.remotelist = res.list;
              } else {
                return [];
              }
            });
          }
        }
      }
    }, {
      coltype: 'string',
      prop: 'gymc',
      label: '工艺名称',
      headeralign: 'center',
      align: 'left',
      width: 280,
      overflowtooltip: true,
      sortable: true,
    }, {
      coltype: 'string',
      prop: 'gyms',
      label: '工艺描述',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'jwdx',
      label: '文件大小(M)',
      headeralign: 'center',
      align: 'center',
      width: 150,
      sortable: true,
    }, {
      coltype: 'list',
      prop: 'wjfl',
      label: '文件分类',
      headeralign: 'center',
      align: 'left',
      width: 80,
      options: [{
          label: '工艺',
          value: '工艺'
        }, {
          label: '视频',
          value: '视频'
        }
      ],
      hideoptionval: true
    }, {
      coltype: 'datetime',
      prop: 'scsj',
      label: '上传日期',
      headeralign: 'center',
      align: 'center',
      width: 140,
      sortable: true,
    }
  ],
  form: {
    gcdm: '9902',
    scx: '',
    statusno: '',
    gymc: '',
    gyms: '',
    jwdx: '',
    wjfl: '工艺',
    scsj: '',
    wjlj: '',
    scxzx: '',
    scxzxs: [],
    remotelist: [],
    isdb: false,
    isedit: true,
  },
  addapi: {
    url: '/lbj/dzgy/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/dzgy/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/dzgy/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/dzgy/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
