{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: false,
  pagefuns: {},
  fields: [{
      coltype: 'string',
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '数据类型',
      prop: 'commcationdatatype',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: 'PLCIP',
      prop: 'plcip',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '通讯状态',
      prop: 'commcationstatus',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '通讯结果',
      prop: 'commcationrresult',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '交互数据',
      prop: 'plcdata',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '夹具号',
      prop: 'jjh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '数据区',
      prop: 'dbnum',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '交互时间',
      prop: 'time',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }],
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
    url: '/ducar/plclog/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
