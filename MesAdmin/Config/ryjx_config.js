{
  isgradequery: false,
  isoperate: false,
  isselect: false,
  pagefuns: {},
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
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
      options: []
    }, {
      coltype: 'string',
      prop: 'zjgs',
      label: '总加工数',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'glf',
      label: '工料废',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'dd',
      label: '待定',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'dbs',
      label: '打包数',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'hgl',
      label: '合格率',
      headeralign: 'center',
      align: 'center',
    },
  ],
  queryapi: {
    url: '/lbj/ryjx/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
