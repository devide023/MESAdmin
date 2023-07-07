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
      window.open('http://172.16.201.125:7002/template/lbj/员工技能信息.xlsx');
    },
    select_usercode_handle: function () {
      console.log(this.$basepage);
    }
  },
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
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
            if (result.code === 1) {
              for (var i = 0; i < result.list.length; i++) {
                var item = result.list[i];
                vm.$set(item, 'lrr', vm.$store.getters.name);
                vm.$set(item, 'lrsj', vm.$parseTime(new Date()));
                vm.$set(item, 'isdb', false);
                vm.$set(item, 'isedit', true);
                vm.list.unshift(item);
              }
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
              _this.$message.error(result.msg);
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
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/ryjn/readxls_by_replace', {
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
          _this.$request('get', '/lbj/ryjn/readxls_by_zh', {
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
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      width: 110,
      sortable: true,
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
      coltype: 'string',
      prop: 'jnbh',
      label: '技能编号',
      headeralign: 'center',
      align: 'center',
      width: 120,
      sortable: true,
    }, {
      coltype: 'list',
	  searchtype:'string',
      prop: 'usercode',
      dbprop: 'user_code',
      label: '账号',
      sortable: true,
      width: 110,
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
      width: 80,
      headeralign: 'center',
      align: 'center',
	  searchable:false,
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
      options: [],
      relation: 'gwhoptions',
	  multiple:true,
	  selectedvals:'selectedgwh'
    }, {
      coltype: 'list',
      prop: 'jnfl',
      label: '技能分类',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '刻字',
          value: '刻字'
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
      ],
	  hideoptionval:true
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
    gwhoptions: [],
	useroptions:[],
	selectedgwh:[],
    isdb: false,
    isedit: true,
  },
  addapi: {
    url: '/lbj/ryjn/add',
    method: 'post',
    callback: function (_this, res) {
	}
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
