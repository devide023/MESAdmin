{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/rygl/readxls', {
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
    import_by_replace: function (_this, res) {
      _this.$message.error('暂不支持该操作');
      _this.$loading().close();
    },
    import_by_zh: function (_this, res) {
      _this.$message.error('暂不支持该操作');
      _this.$loading().close();
    },
    export_excel: function (_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields.filter(function (i) {
              return ['password', 'xpmc'].indexOf(i.prop) === -1;
            }), expdatalist);
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
      let _this = this;
      var row = _this.$deepClone(_this.pageconfig.form);
      _this.list.unshift(row);
    },
    download_template_file: function () {
      window.open('http://172.16.201.216:7002/template/A1/员工基础信息.xlsx?r=' + Math.random());
    },
    select_scx: function (collist, val, row) {
      var idx = collist.findIndex(i => i.prop === 'scx');
      if (idx !== -1) {
        var pos = collist[idx].options.findIndex(j => j.value === val);
        if (pos !== -1) {
          row.bzxx = collist[idx].options[pos].label;
        }
      }
    },
    select_bzxx: function (collist, val, row) {
      var idx = collist.findIndex(i => i.prop === 'scx');
      if (idx !== -1) {
        var pos = collist[idx].options.findIndex(j => j.value === val);
        if (pos !== -1) {
          row.bzxx = collist[idx].options[pos].label;
        }
      }
    }
  },
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      dbprop: 'scx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/scx'
      },
      hideoptionval: true,
      options: [],
      change_fn_name: 'select_scx',
    }, {
      coltype: 'string',
      label: '帐号',
      prop: 'usercode',
      dbprop: 'user_code',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 80
    }, {
      coltype: 'string',
      label: '姓名',
      prop: 'username',
      dbprop: 'user_name',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '密码',
      prop: 'password',
      dbprop: 'pass_word',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '岗位号',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '性别',
      prop: 'ryxb',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '男',
          value: '男'
        }, {
          label: '女',
          value: '女'
        }
      ],
      hideoptionval: true,
      width: 80
    }, {
      coltype: 'list',
      label: '类型',
      prop: 'rylx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '装配',
          value: '装配'
        }, {
          label: '机动',
          value: '机动'
        }, {
          label: '线长',
          value: '线长'
        }, {
          label: '巡检',
          value: '巡检'
        }
      ],
      hideoptionval: true,
    }, {
      coltype: 'list',
      label: '学历',
      prop: 'xl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '小学',
          value: '小学'
        }, {
          label: '初中',
          value: '初中'
        }, {
          label: '高中',
          value: '高中'
        }, {
          label: '大专',
          value: '大专'
        }, {
          label: '本科',
          value: '本科'
        }
      ],
      hideoptionval: true,
      width: 100
    }, {
      coltype: 'string',
      label: '手机号',
      prop: 'tel',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '班组',
      prop: 'bzxx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/scx'
      },
      options: [],
      hideoptionval: true,
      change_fn_name: 'select_bzxx'
    }, {
      coltype: 'date',
      label: '入司日期',
      prop: 'rsrq',
      width: 150,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'date',
      label: '出生日期',
      prop: 'csrq',
      width: 150,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'image',
      prop: 'xpmc',
      label: '照片',
      searchable: false,
      headeralign: 'center',
      align: 'center',
      action: 'http://172.16.201.216:7002/api/upload/image',
      accept: '.jpg,.jpeg,.png',
      before_upload: function (file) {
        var isJPG = file.type === "image/jpeg" || file.type === "image/jpg" || file.type === "image/png";
        var isLt2M = file.size / 1024 / 1024 < 5;
        if (!isJPG) {
          this.$message.error("上传头像图片只能是JPG,PNG格式!");
        }
        if (!isLt2M) {
          this.$message.error("上传头像图片大小不能超过 5MB!");
        }
        return isJPG && isLt2M;
      },
      upload_success: function (res, file) {
        if (res.code === 1) {
          if (res.files.length > 0) {
            var rowkey = res.extdata.rowkey;
            var findrow = this.$basepage.list.find((t) => t.rowkey === rowkey);
            if (findrow) {
              findrow.xpmc = res.files[0].fileid;
            }
          }
        } else if (res.code === 0) {
          this.$message.error(res.msg);
        }
      }
    }, {
      coltype: 'bool',
      label: '离职',
      prop: 'scbz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
      width: 100
    }
  ],
  form: {
    scx: '',
    usercode: '',
    username: '',
    password: '123456',
    gcdm: '9100',
    scx: '1',
    gwh: '',
    bzxx: '',
    hgsg: 'Y',
    rsrq: '',
    csrq: '',
    ryxb: '男',
    rylx: '装配',
    tel: '',
    xpmc: '',
    scbz: 'N',
    lzrq: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/rygl/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/rygl/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/rygl/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/rygl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
