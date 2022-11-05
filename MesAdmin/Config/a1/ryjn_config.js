{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  batoperate: {
    import_by_add: function (vm, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          vm.$request('get', '/a1/ryjn/readxls', {
            fileid: fid
          }).then(function (result) {
            vm.$loading().close();
            if (result.code === 1) {
              vm.$message.success(result.msg);
			  vm.getlist(vm.queryform);
            } else if (result.code === 2) {
              vm.$message.warning(result.msg);
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
    import_by_replace(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/ryjn/readxls_by_replace', {
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
          _this.$request('get', '/a1/ryjn/readxls_by_zh', {
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
      window.open('http://localhost:52655/template/A1/人员技能.xlsx');
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
      label: '技能编号',
      prop: 'jnbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 120,
    }, {
      coltype: 'string',
      label: '账号',
      suggest: true,
      prop: 'usercode',
      dbprop: 'user_code',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150,
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn'
    }, {
      coltype: 'string',
      label: '姓名',
      prop: 'username',
      headeralign: 'center',
      align: 'center',
      width: 130,
      overflowtooltip: true,
    }, {
      coltype: 'list',
      label: '适应岗位',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/gwzd'
      },
      width: 150,
    }, {
      coltype: 'list',
      label: '技能分类',
      prop: 'jnfl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '装配',
          value: '装配'
        }, {
          label: '部装',
          value: '部装'
        }, {
          label: '测试',
          value: '测试'
        }
      ],
      width: 150,
	  hideoptionval:true,
    }, {
      coltype: 'rate',
      label: '熟练度',
      prop: 'jnsld',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 180,
    }, {
      coltype: 'string',
      label: '技能信息',
      prop: 'jnxx',
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
      width: 130,
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
    usercode: '',
    username: '',
    jnbh: '',
    jnxx: '',
    gwh: '',
    sfhg: 'Y',
    lrr: '',
    lrsj: '',
    jnfl: '装配',
    jnsld: 3,
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/ryjn/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/ryjn/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/ryjn/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/ryjn/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
