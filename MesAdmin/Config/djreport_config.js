{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: false,
  operate_fnlist: [],
  pagefuns: {},
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
      headeralign: 'center',
      align: 'center',
      width: 150,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx'
      },
      options: [],
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
      options: []
    }, {
      coltype: 'string',
      prop: 'sbbh',
      label: '设备编号',
      headeralign: 'center',
      align: 'left',
      width: 180,
	  sortable:true,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'dbh',
      label: '刀柄号',
      headeralign: 'center',
      align: 'left',
      width: 180,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'dblx',
      label: '刀柄类型',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'rjlx',
      label: '刃具类型',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'rjbzsm',
      label: '标准寿命',
      headeralign: 'center',
      align: 'left',
      width: 180,
	  sortable:true,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'rjdqsm',
      label: '当前寿命',
      headeralign: 'center',
      align: 'left',
      width: 180,
	  sortable:true,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'dqjgs',
      label: '当前加工数',
      headeralign: 'center',
      align: 'left',
      width: 180,
	  sortable:true,
      overflowtooltip: true,
    }, {
      coltype: 'progress',
      prop: 'rjzt',
      label: '刃具状态',
      headeralign: 'center',
      align: 'left',
      width: 100,
	  sortable:true,
    },
  ],
  trbginfo: [{
    colname: 'rjzt',
    logiclist: [{
        logic: 'between',
        val0: 90,
        val1: 95,
        classname: 'warning-row',
      }, {
        logic: 'between',
        val0: 95,
        val1: 100,
        classname: 'danger-row',
      }, {
        logic: '>=',
        val0: 100,
        classname: 'error-row',
      },
    ]
  }],
  form: {
    isdb: false,
    isedit: true
  },
  queryapi: {
    url: '/lbj/djreport/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
