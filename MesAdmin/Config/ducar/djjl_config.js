{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: false,
  batoperate: {
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
  pagefuns: {},
  fields: [
  {
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
  },{
      coltype: 'string',
      label: '点检编号',
      prop: 'djno',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
	{
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
	  options:[],
	  relation: 'gwhs',
    },{
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150,
    },  {
      coltype: 'string',
	  suggest:true,
      label: '机型',
      prop: 'jxno',
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
    }, 
	{
      coltype: 'string',
      label: '点检结果',
      prop: 'djjg',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
    },
	{
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
    },
	{
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
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/djjl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
