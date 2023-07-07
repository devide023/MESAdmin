{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  querybarname: 'ReportGsjComponent',
  componentlist: ['DataDetailComponent'],
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
        url: '/a1/baseinfo/scx'
      },
	  hideoptionval:true,
	  options:[]
  }, 
	{
      coltype: 'string',
      label: '机型 ',
      prop: 'jxno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, 
	{
      coltype: 'string',
      label: '机号 ',
      prop: 'vin',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '干检参数',
      prop: 'gjcs',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
{
      coltype: 'string',
      label: '干检结果',
      prop: 'gjjg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },	
	{
      coltype: 'datetime',
      label: '干检时间',
      prop: 'gjsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },{
      coltype: 'string',
      label: '干检员',
      prop: 'gjry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },  
	{
      coltype: 'string',
      label: '水检结果',
      prop: 'sjjg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '水检员',
      prop: 'sjry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
	{
      coltype: 'datetime',
      label: '水检时间',
      prop: 'sjsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '原因',
      prop: 'yy',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '处理意见',
      prop: 'clyj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
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
    url: '/a1/report/gsjdbsy',
    method: 'post',
    callback: function (vm, res) {},
  },
}
