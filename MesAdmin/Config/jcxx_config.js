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
            if (result.code === 1) {
              vm.$message.success(result.msg);
            } else if (result.code === 2) {
              vm.$message.warning(result.msg);
            } else if (result.code === 0) {
              vm.$message.error(result.msg);
            }
            vm.getlist(vm.queryform);
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
    bzxx: '白班',
    gwh: '',
    jcxx: '',
    jcly: '奖惩通报',
    sl: 1,
    jcje: 0,
    fsrq: '',
    khr: '',
    khbm: '',
    lx: '',
    bz: '',
    lrr: '',
    lrsj: '',
	useroptions:[],
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
		row.usercode='';
        _this.$request('get', '/lbj/baseinfo/scx_gwh?scx=' + val).then(function (res) {
          if (res.code === 1) {
            row.gwhoptions = res.list;
          }
        });
		_this.$request('get', '/lbj/baseinfo/scx_ryxx?scx=' + val).then(function (res) {
          if (res.code === 1) {
            row.useroptions = res.list;
          }
        });
      },
      clear_fn_name: function (_this, row) {
        row.gwh = '';
		row.usercode='';
      },
      options: []
    }, {
      coltype: 'list',
      prop: 'usercode',
	  dbprop: 'user_code',
      label: '账号',
      headeralign: 'center',
      align: 'center',
      options: [],
	  change_fn_name: function (_this, collist, val, row){
		  var f = row.useroptions.filter(function(i){
			 return i.value === val;
		  });
		  if(f.length>0){
		  row.username = f[0].label;
		  }
	  },
	  clear_fn_name: function (_this, row) {
		row.username='';
      },
	  relation:'useroptions'
    }, {
      coltype: 'string',
      prop: 'username',
      label: '姓名',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
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
      label: '岗位',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      relation: 'gwhoptions',
      options: [],
    }, {
      coltype: 'list',
      prop: 'lx',
      label: '类型',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '奖励',
          value: '奖励'
        }, {
          label: '惩罚',
          value: '惩罚'
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
	  width:150,
	  overflowtooltip:true,
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
