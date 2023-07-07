{
  isgradequery: true,
  isoperate: false,
  isselect: false,
  pagefuns: {},
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
	  width:150,
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'statusno',
      dbprop: 'status_no',
      label: '状态编码',
	  width:150,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位号',
	  width:100,
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'zpsx',
      label: '装配顺序',
      headeralign: 'center',
	  width:80,
      align: 'center',
    }, {
      coltype: 'bool',
      prop: 'mj',
      label: '免检',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
	  width:100,
    }, {
      coltype: 'bool',
      prop: 'fsbz',
      label: '互锁标志',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'bool',
      prop: 'shbz',
      label: '审核标志',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
	  width:100,
    }, {
      coltype: 'bool',
      prop: 'sfzp',
      label: '是否装配',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      prop: 'fjbh',
      label: '复检编号',
      headeralign: 'center',
      align: 'center',
	  width:100,
    }, {
      coltype: 'string',
      prop: 'bz',
      label: '备注',
      headeralign: 'center',
      align: 'center',
	  width:100,
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'shr',
      label: '审核人',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'shsj',
      label: '审核时间',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'fsr',
      label: '互锁人',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'fssj',
      label: '互锁时间',
      headeralign: 'center',
      align: 'center',
    },
  ],
  queryapi: {
    url: '/lbj/gylx/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
