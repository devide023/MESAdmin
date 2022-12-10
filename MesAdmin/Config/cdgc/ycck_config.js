{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: false,
  operate_fnlist: [],
  pagefuns: {},
  fields: [{
      coltype: 'string',
      prop: 'bjzt',
      label: '报警主体',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 130,
    }, {
      coltype: 'list',
      prop: 'bjlx',
      label: '报警类型',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      options: [{
          label: '质量异常',
          value: 1
        }, {
          label: '物料异常',
          value: 2
        }, {
          label: '设备异常',
          value: 3
        }, {
          label: '其它异常',
          value: 4
        }, {
          label: '刀具异常',
          value: 5
        }, {
          label: '夹具异常',
          value: 6
        }
      ],
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'bjdm',
      label: '报警代码',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 130,
    }, {
      coltype: 'string',
      prop: 'bjxx',
      label: '报警信息',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      overflowtooltip: true
    }, {
      coltype: 'string',
      prop: 'gzxx',
      label: '故障信息',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 130,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'status',
      label: '报警状态',
      headeralign: 'center',
      align: 'center',
      sortable: true,
	  overflowtooltip: true
    }, {
      coltype: 'datetime',
      prop: 'kssj',
      label: '报警开始时间',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      overflowtooltip: true
    }, {
      coltype: 'string',
      prop: 'bjdj',
      label: '报警等级',
      headeralign: 'center',
      align: 'center',
      sortable: true,
	  overflowtooltip: true
    }, {
      coltype: 'string',
      prop: 'bjlxdesc',
      label: '报警类型描述',
      headeralign: 'center',
      align: 'center',
      sortable: true,
	  overflowtooltip: true
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '记录人',
      headeralign: 'center',
      align: 'center',
      sortable: true,
	  overflowtooltip: true
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '记录时间',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 170,
	  overflowtooltip: true
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
    url: '/cdgc/cdycgl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
