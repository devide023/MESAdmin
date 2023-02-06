{
  isgradequery: true,
  isbatoperate: true,
  isoperate: true,
  isfresh: true,
  isselect: true,
  operate_fnlist: [{
      label: '下载',
      fnname: 'download_dzgyMp4',
      btntype: 'text'
    },
	{
      label: '查看',
      fnname: 'view_dzgyMp4',
      btntype: 'text'
    }
  ],
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/dzgy/readxls', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
              _this.getlist(_this.queryform);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else {
              _this.$message.error(result.msg);
            }
          });
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
    },
    import_by_replace(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/dzgy/readxls_by_replace', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
            _this.getlist(_this.queryform);
          });
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
    },
    import_by_zh(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/dzgy/readxls_by_zh', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
            _this.getlist(_this.queryform);
          });
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
    },
    export_excel: function (_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields, expdatalist);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
  },
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_template_file: function () {
      window.open('http://172.16.201.216:7002/template/A1/电子工艺.xlsx?r=' + Math.random());
    },
    download_dzgypdf: function (row) {
      var _this = this;
      this.$request('get', "download/ftp2web", {
        wjlx: '电子工艺',
        wjlj: row.wjlj+"/"+row.gymc
      }).then(function (res) {
        if (res.code === 1) {
          window.open("http://172.16.201.216:7002/api/download/downloadpdf?wjlj=" + row.wjlj+'/'+row.gymc);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
	view_dzgyMp4:function(row){
		window.open("http://172.16.201.216:81/视频/" + row.wjlj+'/'+row.gymc);
	},
    download_dzgyMp4: function (row) {
      var _this = this;
      this.$request('get', "download/ftp2web", {
        wjlx: '视频',
        wjlj: row.wjlj+"/"+row.gymc
      }).then(function (res) {
        if (res.code === 1) {
          window.open("http://172.16.201.216:7002/api/download/downloadpdf?wjlj=" + row.wjlj+'/'+row.gymc);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
    suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'jxno') {
        row.username = '';
        this.$request('get', '/a1/baseinfo/jxno_by_code', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          }
        });
      }
    },
    select_fn: function (vm, item, row, col) {
      if (col.prop === 'jxno') {
        row.statusno = '';
        this.$request('get', '/a1/baseinfo/ztbm_by_jxno', {
          jxno: item.value
        }).then(function (res) {
          if (res.code === 1) {
            row.statusno_list = res.list.map(function (i) {
              return {
                label: i,
                value: i
              };
            });
          }
        });
      }
    }
  },
  fields: [{
      coltype: 'string',
      label: '工艺编号',
      prop: 'gybh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'list',
      label: '文件分类',
      prop: 'gylx',
      searchable: false,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '视频',
          value: '视频'
        }
      ],
      width: 100,
      hideoptionval: true
    }, {
      coltype: 'string',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      suggest: true,
      label: '机型',
      prop: 'jxno',
      dbprop: 'jx_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn',
      width: 150
    }, {
      coltype: 'list',
      label: '状态码',
      prop: 'statusno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150,
      options: [],
      hideoptionval: true,
      relation: 'statusno_list'
    }, {
      coltype: 'string',
      label: '文件名称',
      prop: 'gymc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 300
    }, {
      coltype: 'string',
      label: '工艺描述',
      prop: 'gyms',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, 
	{
      coltype: 'string',
      label: '文件路径',
      prop: 'wjlj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, 
	{
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    gcdm: '9100',
    scx: '1',
    gybh: '',
    gymc: '',
    gyms: '',
    gwh: '',
    jxno: '',
    statusno: '',
    wjlj: '',
    jwdx: '',
    scry: '',
    scpc: '',
    scsj: '',
    gylx: '视频',
    bz: '',
    statusno_list: [],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/gysp/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/gysp/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/gysp/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/gysp/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
