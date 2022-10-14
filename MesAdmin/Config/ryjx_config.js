{
  isgradequery: false,
  isoperate: false,
  isselect: false,
  pagefuns: {},
  fields: [{
      coltype: 'list',
      prop: 'SCX',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      width: 150,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'JCRY',
      label: '员工编号',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'list',
      prop: 'GWH',
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
      prop: 'JGS',
      label: '加工数',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'HGS',
      label: '合格数',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'HGL',
      label: '合格率',
      headeralign: 'center',
      align: 'center',
    },
  ],
}
