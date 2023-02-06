{
  isgradequery: true,
  isbatoperate: true,
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
  fields: [{
      coltype: 'string',
      prop: 'gwh',
	  label: '岗位编号',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150,
    }, {
      coltype: 'string',
      prop: 'wlbm',
	  label: '物料编码',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
	  width:150
    }, 
	{
      coltype: 'list',
      prop: 'wlsx',
	  label: '物料属性',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
	  options:[{label:'大件',value:'A'},{label:'小件',value:'B'}]
    },
	{
      coltype: 'string',
      prop: 'bjmc',
	  label: '物料名称',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'string',
      prop: 'jx_no',
	  label: '机型',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      prop: 'ztbm',
	  label: '状态编码',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, 
	{
      coltype: 'int',
      prop: 'sl',
	  label: '数量',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
	{
      coltype: 'string',
      prop: 'gwpb',
	  label: '岗位配比',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
	{
      coltype: 'list',
      prop: 'zplx',
	  label: '装配类型',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
	  inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/zplx'
      },
      options: [],
    },
	{
      coltype: 'string',
      prop: 'lrr',
	  label: '录入人',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
	  label: '录入时间',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }
  ],
  form: {
    bjbm: '',
    bjmc: '',
    xt: '',
    gw1: '',
    ztbm: '',
    lrr: '',
    lrsj: '',
    sl: '',
    gwpb: '',
    zplx: '',
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
    url: '/a1/gwwl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
