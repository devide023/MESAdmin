{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: false,
  querybarname: 'ReportGztj',
  pagefuns: {},
  fields: [{
      coltype: 'string',
      label: '机型',
      prop: 'jx_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    },
	{
      coltype: 'string',
      label: '状态编码',
      prop: 'status_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    },
	{
      coltype: 'string',
      label: '检测数量',
      prop: 'jcsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, 
	{
      coltype: 'string',
      label: '一次合格数量',
      prop: 'firsthgsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    },
	{
      coltype: 'string',
      label: '合格数量',
      prop: 'hgsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '一次合格率',
      prop: 'hgl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
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
    url: '/ducar/report/gztj',
    method: 'post',
    callback: function (vm, res) {},
  },
}
