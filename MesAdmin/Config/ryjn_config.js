{
  isgradequery: true,
  isoperate: false,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    select_usercode_handle: function () {
      console.log(this.$basepage);
    }
  },
  isbatoperate: true,
  batoperate: {
    import_by_add: function (vm, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          vm.$request('get', '/lbj/ryjn/readxls', {
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
          this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {},
    import_by_zh(_this, res) {},
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
      options: []
    }, {
      coltype: 'string',
      prop: 'jnbh',
      label: '技能编号',
      headeralign: 'center',
      align: 'center',
      width: 80,
    }, {
      coltype: 'string',
      prop: 'usercode',
	  dbprop:'user_code',
      label: '账号',
	  width:80,
      headeralign: 'center',
      align: 'center',
      suggest: function (key, cb) {
        this.$request('get', '/lbj/baseinfo/usercode', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          } else {
            this.$message.error(res.msg);
          }
        });
      },
      select_handlename: 'select_usercode_handle'
    }, {
      coltype: 'string',
      prop: 'username',
      label: '姓名',
	  width:80,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'jnxx',
      label: '技能',
      headeralign: 'center',
      align: 'center',
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
      options: []
    }, {
      coltype: 'list',
      prop: 'jnfl',
      label: '技能分类',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '打码',
          value: '打码'
        }, {
          label: '机加',
          value: '机加'
        }, {
          label: '检漏',
          value: '检漏'
        }, {
          label: '清洗',
          value: '清洗'
        }, {
          label: '检测',
          value: '检测'
        }
      ]
    }, {
      coltype: 'date',
      prop: 'jnsj',
      label: '技能时间',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'rate',
      prop: 'jnsld',
      label: '熟练度',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'bool',
      prop: 'sfhg',
      label: '是否合格',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }
  ],
  form: {
    gcdm: '9902',
    scx: '',
    usercode: '',
    jnbh: '',
    jnxx: '',
    gwh: '',
    sfhg: 'Y',
    lrr: '',
    lrsj: '',
    jnfl: '',
    jnsj: '',
    jnsld: 0,
    isdb: false,
    isedit: true,
  },
  addapi: {
    url: '/lbj/ryjn/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/ryjn/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/ryjn/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/ryjn/list',
    method: 'post',
    callback: function (vm, res) {}
  },
}
