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
          _this.$request('get', '/ducar/djxx/readxls', {
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
          _this.$request('get', '/ducar/djxx/readxls_by_replace', {
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
          _this.$request('get', '/ducar/djxx/readxls_by_zh', {
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
      window.open('http://192.168.1.111:7002/template/Ducar/点检基础信息.xlsx?r='+Math.random());
    },
	select_scx: function (collist, val, row) {
        row.gwhs=[];
		row.gwh='';
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
	select_gwh: function (collist, val, row){
		var pos = row.gwhs.findIndex(t => t.value === val);
      if (pos !== -1) {
        row.gwmc = row.gwhs[pos].label;
      } else {
        row.gwmc = '';
      }
	},
	  suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'jxno') {
        row.username = '';
        this.$request('get', '/ducar/baseinfo/jxno_by_code', {
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
		  row.statusno_list=[];
        this.$request('get', '/ducar/baseinfo/ztbm_by_jxno', {
          jxno: item.value
        }).then(function (res) {
          if (res.code === 1) {
            row.statusno_list = res.list.map(function(i){
				return {label:i,value:i};
			});
          }
        });
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
        url: '/ducar/baseinfo/scx'
      },
	  hideoptionval:true,
	  options:[],
	  change_fn_name: 'select_scx',
  },{
      coltype: 'string',
      label: '点检编号',
      prop: 'djno',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'list',
      label: '点检类型',
      prop: 'djlx',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  options:[{label:'点检',value:'点检'},{label:'互检',value:'互检'}],
	  hideoptionval:true,
	  width:100
    }, {
      coltype: 'list',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
	  optionconfig:{
		  method: 'get',
		  url: '/ducar/baseinfo/gwzdbyscx',
		  querycnf:[{scx:'scx'}]
	  },
	  relation: 'gwhs',
	  change_fn_name: 'select_gwh',
    },{
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
	  suggest:true,
      label: '机型',
      prop: 'jxno',
      dbprop: 'jx_no',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn',
	  width:100,
    }, {
      coltype: 'list',
      label: '状态码',
      prop: 'statusno',
      dbprop: 'status_no',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  hideoptionval:true,
	  options:[],
	  relation:'statusno_list',
	  width:100
    }, {
      coltype: 'string',
      label: '点检内容',
      prop: 'djxx',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'bool',
      label: '删除标志',
      prop: 'scbz',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  activevalue: 'Y',
      inactivevalue: 'N',
	  width:100
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }
  ],
  form: {
	  scx:'',
    gwh: '',
    jxno: '',
    statusno: '',
    djno: '',
    djxx: '',
    scbz: 'N',
    lrr: '',
    lrsj: '',
    djlx: '点检',
	statusno_list:[],
	gwhs:[],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/ducar/djxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/ducar/djxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/ducar/djxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/djxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
