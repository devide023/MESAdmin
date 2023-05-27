{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  querybarname: 'ZxjcComponent',
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
  fields: [{
      coltype: 'string',
      label: '编号',
      prop: 'rowno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },{
      coltype: 'string',
      label: '类别',
      prop: 'jclb',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
	{
      coltype: 'string',
      label: '检查项目',
      prop: 'jcyq',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    },{
      coltype: 'string',
      label: '序号',
      prop: 'xh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
	{
      coltype: 'string',
      label: '首台机号',
      prop: 'firstvin',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '首台实测试记录',
      prop: 'firstjcjg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '末台机号',
      prop: 'lastvin',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '末台实测试记录',
      prop: 'lastjcjg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '单项判定',
      prop: 'dxpd',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
	{
      coltype: 'string',
      label: '机型编码',
      prop: 'jxno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:120
    },
	{
      coltype: 'string',
      label: '状态编码',
      prop: 'statusno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:120
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
    url: '/a1/report/jcjg',
    method: 'post',
    callback: function (vm, res) {},
  },
}
