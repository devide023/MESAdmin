{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  operate_fnlist: [],
  pagefuns: {},
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
      width: 150,
      sortable: true,
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
      width: 130,
      fixed: 'left',
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'rwzt',
      label: '任务状态',
      headeralign: 'center',
      align: 'center',
      width: 150,
      fixed: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'list',
      prop: 'trigtype',
      dbprop: 'trig_type',
      label: '触发类型',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
      options: [{
          label: '自动',
          value: '1'
        }, {
          label: '人工',
          value: '2'
        }
      ]
    }, {
      coltype: 'list',
      prop: 'changetype',
      dbprop: 'change_type',
      label: '变化点类型',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
      options: [{
          label: '人员',
          value: '1'
        }, {
          label: '刀具',
          value: '2'
        }, {
          label: '换产',
          value: '3'
        }, {
          label: '程序号变更',
          value: '4'
        }, {
          label: 'API数据',
          value: '5'
        }
      ]
    }, {
      coltype: 'string',
      prop: 'cpxh',
      label: '产品型号',
      headeralign: 'center',
      align: 'center',
      width: 130,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'jt',
      label: '设备编号',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'bhbw',
      label: '变化部位',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'gzxx',
      label: '故障信息',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'fsddbh',
      label: '发生断点编号',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'fxddbh',
      label: '发现断点编号',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'scbzpdjg',
      label: '判定结果',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'xcxjczrmc',
      label: '确认人',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'xcxjczsj',
      label: '确认时间',
      headeralign: 'center',
      align: 'center',
      width: 100,
      sortable: true,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'cjrmc',
      label: '记录人',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'cjsj',
      label: '记录时间',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
      sortable: true,
    },
  ],
  form: {
    isdb: false,
    isedit: true
  },
  queryapi: {
    url: '/lbj/bhdjl/list',
    method: 'post',
    callback: function (_this, res) {},
  },
}
