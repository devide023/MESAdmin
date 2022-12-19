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
          _this.$request('get', '/a1/jcgl/readxls', {
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
    download_template_file: function () {
      window.open('http://172.16.201.216:7002/template/A1/奖惩信息.xlsx?r='+Math.random());
    },
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'usercode') {
        row.username = '';
        this.$request('get', '/a1/baseinfo/ryxx_by_code', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          }
        });
      }
    },
    select_fn: function (vm, item, row, col) {
      if (col.prop === 'usercode') {
        row.username = item.label;
      }
    }
  },
  fields: [{
      coltype: 'string',
      suggest: true,
      label: '人员帐号',
      prop: 'usercode',
      dbprop: 'user_code',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn'
    }, {
      coltype: 'string',
      label: '姓名',
      searchable: false,
      prop: 'username',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      width: 80
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
	  hideoptionval:true,
    }, {
      coltype: 'list',
      label: '类型',
      prop: 'lx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '奖励',
          value: '奖励'
        }, {
          label: '惩罚',
          value: '惩罚'
        }
      ],
	  hideoptionval:true,
    }, {
      coltype: 'int',
      label: '数量',
      prop: 'sl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'int',
      label: '金额',
      prop: 'jcje',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'date',
      label: '发生日期',
      prop: 'fsrq',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '奖惩描述',
      prop: 'jcxx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '奖惩来源',
      prop: 'jclr',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '考核人',
      prop: 'khr',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '考核部门',
      prop: 'khbm',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }
  ],
  form: {
    usercode: '',
    bzxx: '白班',
    gwh: '',
    jcxx: '',
    jclr: '',
    sl: '1',
    jcje: '',
    fsrq: '',
    khr: '',
    khbm: '',
    lx: '',
    bz: '',
    lrr: '',
    lrsj: '',
    scbz: 'N',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/jcgl/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/jcgl/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/jcgl/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/jcgl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
