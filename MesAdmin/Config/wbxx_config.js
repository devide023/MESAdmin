{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  pagefuns: {},
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx',
      },
      options: [],
    }, {
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902',
      },
      options: [],
    }, {
      coltype: 'list',
      label: '岗位',
      prop: 'gwh',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd',
      },
      options: [],
    }, {
      coltype: 'int',
      label: '顺序',
      prop: 'wbsx',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '维保内容',
      prop: 'wbxx',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'bool',
      label: '删除标志',
      prop: 'scbz',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, ],
  form: {
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/wbxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/wbxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/wbxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/wbxx/query',
    method: 'post',
    callback: function (vm, res) {},
  },
}
