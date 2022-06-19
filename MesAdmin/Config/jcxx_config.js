{
  isgradequery: true,
  isoperate: false,
  isfresh: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_template_file: function () {
      window.open('http://172.16.201.125:7002/template/lbj/奖惩数据.xlsx');
    },
    select_usercode_handle: function () {},
    scx_field_change_handle(collist, item, row) {
      var gwcol = collist.filter(i => i.prop === 'gwh');
      this.$request('get', '/lbj/baseinfo/gwh?scx=' + item).then(function (res) {
        if (res.code === 1) {
          if (gwcol) {
            gwcol[0].options = res.list;
          }
        }
      });
    },
  },
  isbatoperate: true,
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  batoperate: {
    import_by_add: function (vm, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          vm.$request('get', '/lbj/jcgl/readxls', {
            fileid: fid
          }).then(function (result) {
            vm.$loading().close();
            for (var i = 0; i < result.list.length; i++) {
              var item = result.list[i];
              vm.$set(item, 'lrr', vm.$store.getters.name);
              vm.$set(item, 'lrsj', vm.$parseTime(new Date()));
              vm.$set(item, 'isdb', false);
              vm.$set(item, 'isedit', true);
              vm.list.unshift(item);
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
          _this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {
      _this.$loading().close();
      _this.$message.error('暂不支持批量替换导入');
    },
    import_by_zh(_this, res) {
      _this.$loading().close();
      _this.$message.error('暂不支持综合导入');
    },
  },
  form: {
    gcdm: '9902',
    scx: '',
    usercode: '',
    bzxx: '',
    gwh: '',
    jcxx: '',
    jcly: '',
    sl: 0,
    jcje: 0,
    fsrq: '',
    khr: '',
    khbm: '',
    lx: '',
    bz: '',
    lrr: '',
    lrsj: '',
    gwhoptions: [],
    isdb: false,
    isedit: true,
  },
  fields: [{
      coltype: 'list',
      prop: 'gcdm',
      label: '工厂',
      headeralign: 'center',
      align: 'center',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx'
      },
      options: []
    }, {
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      change_fn_name: function (_this, collist, val, row) {
        row.gwh = '';
        _this.$request('get', '/lbj/baseinfo/scx_gwh?scx=' + val).then(function (res) {
          if (res.code === 1) {
            row.gwhoptions = res.list;
          }
        });
      },
      clear_fn_name: function (_this, row) {
        row.gwh = '';
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'usercode',
      label: '账号',
      headeralign: 'center',
      align: 'center',
      suggest: function (key, cb) {
        this.$request('get', '/lbj/baseinfo/usercode', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list)
          } else {
            this.$message.error(res.msg)
          }
        })
      },
      select_handlename: 'select_usercode_handle'
    }, {
      coltype: 'list',
      prop: 'bzxx',
      label: '班组',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '白班',
          value: '白班'
        }, {
          label: '晚班',
          value: '晚班'
        }
      ]
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      relation: 'gwhoptions',
      options: [],
    }, {
      coltype: 'string',
      prop: 'lx',
      label: '考核类型',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '质量异常',
          value: '质量异常'
        }, {
          label: '错装',
          value: '错装'
        }, {
          label: '漏装',
          value: '漏装'
        }
      ]
    }, {
      coltype: 'int',
      prop: 'sl',
      label: '数量',
      headeralign: 'center',
      align: 'center',
      with : 130,
    }, {
      coltype: 'int',
      prop: 'jcje',
      label: '金额',
      headeralign: 'center',
      align: 'center',
      with : 100,
    }, {
      coltype: 'string',
      prop: 'jcxx',
      label: '明细',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'jcly',
      label: '来源',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'date',
      prop: 'fsrq',
      label: '发生日期',
      headeralign: 'center',
      align: 'center',
      with : 180,
    }, {
      coltype: 'string',
      prop: 'khr',
      label: '考核人',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'khbm',
      label: '考核部门',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'bz',
      label: '备注',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }
  ],
  addapi: {
    url: '/lbj/jcgl/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/jcgl/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/jcgl/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/jcgl/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
