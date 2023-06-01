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
          _this.$request('get', '/ducar/gwbj/readxls', {
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
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/ducar/gwbj/readxls_by_replace', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
              _this.getlist(_this.queryform);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
          })
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
    },
    import_by_zh: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/ducar/gwbj/readxls_by_zh', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
              _this.getlist(_this.queryform);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
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
    },
  ],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
	download_template_file: function () {
      window.open('http://192.168.1.111:7002/template/Ducar/岗位物料.xlsx?r='+Math.random());
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
	select_gwh:function(collist, val, row){
		var pos = row.gwhs.findIndex(t=>t.value === val);
		if(pos!==-1){
			row.gwmc = row.gwhs[pos].label;
		}else{
			row.gwmc='';
		}	
	},
	suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'wlbm') {
        row.username = '';
        this.$request('get', '/ducar/baseinfo/wlbm_by_key', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          }
        });
      }
    },
    select_fn: function (vm, item, row, col) {
      if (col.prop === 'wlbm') {
		  row.wlmc=item.label;
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
      label: '机型',
      prop: 'jxno',
      dbprop: 'jx_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 130,
    }, {
      coltype: 'list',
      label: '岗位号',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      relation: 'gwhs',
	  width: 110,
	  change_fn_name: 'select_gwh'
    },
	{
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
	  searchable:false,
	  width: 150,
    },
	{
      coltype: 'string',
      label: '物料编码',
      prop: 'wlbm',
	  suggest: true,
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width: 180,
	  suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn'
    },{
      coltype: 'string',
      label: '物料名称',
      prop: 'wlmc',
      overflowtooltip: true,
      sortable: true,
	  searchable:false,
      headeralign: 'center',
      align: 'left',
	  width: 250,
    },{
      coltype: 'list',
      label: '料箱判断',
      prop: 'lxpd',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  options:[{label:'不校验',value:'N'},{label:'首台',value:'Y'},{label:'每台',value:'A'}],
	  width: 100,
    },{
      coltype: 'string',
      label: '单箱数量',
      prop: 'dxsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width: 100,
    }, {
      coltype: 'string',
      label: '岗位用量',
      prop: 'gwpb',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width: 100,
    },{
      coltype: 'string',
      label: '物料属性',
      prop: 'wlsx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width: 100,
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    },{
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width: 100,
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width: 140,
    }
  ],
  form: {
    scx: '',
    gwh: '',
    wlbm: '',
    dxsl: 0,
    gwpb: 1,
    qwwbm: '',
    wlsx: 'A',
    bz: '',
    lrr: '',
    lrsj: '',
    jxno: '',
    gwhs: [],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/ducar/gwbj/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/ducar/gwbj/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/ducar/gwbj/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/gwbj/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
