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
          _this.$request('get', '/ducar/ryxx/readxls', {
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
      window.open('http://192.168.1.111:7002/template/Ducar/员工基础信息.xlsx?r=' + Math.random());
    },
    select_scx: function (collist, val, row) {
      row.gwhs = [];
      row.gwh = '';
      row.gwmc = '';
      this.$request('get', '/ducar/baseinfo/gwzdbyscx', {
        scx: val
      }).then(function (res) {
        if (res.code === 1) {
          row.gwhs = res.list.map(function (i) {
            return {
              label: i.label,
              value: i.value
            };
          });
        }
      });
    },
    select_gwh: function (collist, val, row) {
      var pos = row.gwhs.findIndex(t => t.value === val);
      if (pos !== -1) {
        row.gwmc = row.gwhs[pos].label;
      } else {
        row.gwmc = '';
      }
    },
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
        url: '/ducar/baseinfo/scx'
      },
      hideoptionval: true,
      options: [],
      change_fn_name: 'select_scx',
    }, {
      coltype: 'string',
      label: '账号',
      prop: 'usercode',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 80
    }, {
      coltype: 'string',
      label: '姓名',
      prop: 'username',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '密码',
      prop: 'password',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'list',
      label: '岗位号',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  optionconfig:{
		  method: 'get',
		  url: '/ducar/baseinfo/gwzdbyscx',
		  querycnf:[{scx:'scx'}]
	  },
      relation: 'gwhs',
	  change_fn_name: 'select_gwh'
    }, {
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      sortable: true,
      searchable: false,
      headeralign: 'center',
      align: 'center',
      relation: 'gwhs',
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
      options: [{
          label: '白班',
          value: '白班'
        }, {
          label: '晚班',
          value: '晚班'
        }
      ],
      hideoptionval: true,
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
      action: 'http://192.168.1.111:7002/api/upload/image',
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
    usercode: '',
    username: '',
    password: '123456',
    gcdm: '101',
    scx: '',
    gwh: '',
    gwmc: '',
    bzxx: '白班',
    hgsg: 'Y',
    rsrq: '',
    csrq: '',
    ryxb: '男',
    rylx: '装配',
    tel: '',
    xpmc: '',
    scbz: 'N',
    lzrq: '',
    gwhs: [],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/ducar/ryxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/ducar/ryxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/ducar/ryxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/ryxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
