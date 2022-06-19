{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isselect:false,
  pagefuns: {},
  batoperate: {
    export_excel(_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields, expdatalist);
        } else if(res.code === 0) {
          this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {},
    import_by_zh(_this,res) {},
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
      align: 'left',
      width: 180,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
	  change_fn_name: function (_this, collist, val,row) {
		  row.gwh = '';
        var gwcol = collist.filter(i => i.prop === 'gwh');
        _this.$request('get', '/lbj/baseinfo/scx_gwh?scx=' + val).then(function (res) {
          if (res.code === 1) {
            if (gwcol) {
              gwcol[0].choose_options = res.list;
            }
          }
        });
      },
	  clear_fn_name:function(_this,row){
		  row.gwh = '';
	  },
      options: []
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
	  choose_options:[],
    }, {
      coltype: 'string',
      prop: 'engineno',
      dbprop: 'engine_no',
      label: '机型',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
      prop: 'statusno',
      dbprop: 'status_no',
      label: '状态码',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
      prop: 'djno',
      label: '点检编号',
      headeralign: 'center',
      align: 'left',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'djxx',
      label: '点检内容',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'djjg',
      label: '点检结果',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'bz',
      label: '备注',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'left',
      width: 80,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    },
  ],
  form: {
    gcdm: '',
    scx: '',
    gwh: '',
    engineno: '',
    statusno: '',
    djno: '',
    djxx: '',
    djjg: '',
    bz: '',
    lrr: '',
    lrsj: ''
  },
  addapi: {
    url: '/lbj/djxx/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/djxx/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/djxx/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/djxx/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
