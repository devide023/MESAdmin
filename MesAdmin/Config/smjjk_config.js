{
  isgradequery: true,
  isselect: false,
  isoperate: false,
  pagefuns: {},
  fields: [{
      coltype: 'list',
      prop: 'gcdm',
      label: '工厂',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx'
      },
      options: []
    },
	{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
      width: 100,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    }, {
      coltype: 'string',
        prop: 'statusno',
      dbprop:'status_no',
      label: '状态码',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
    }, {
      coltype: 'string',
      prop: 'engineno',
      label: '产品件号',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
    }, {
      coltype: 'string',
      prop: 'orderno',
      label: '订单号',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
      width: 80,
    }, {
      coltype: 'string',
      prop: 'bc',
      label: '班次',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
      width: 80,
    }, {
      coltype: 'string',
      prop: 'smjbs',
      label: '首末件标识',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
      width: 100,
    }, {
      coltype: 'image',
      prop: 'jczpdz',
      label: '首末件检测照片地址',
      headeralign: 'center',
      align: 'center',
      width: 130,
    }, {
      coltype: 'string',
      prop: 'zpjcjg',
      label: '首末件照片检测结果',
      headeralign: 'center',
      align: 'center',
      width: 130,
    }, {
      coltype: 'string',
      prop: 'jcr',
      label: '首末件照片检测人员',
      headeralign: 'center',
      align: 'center',
      width: 130,
    }, {
      coltype: 'datetime',
      prop: 'jcsj',
      label: '检测时间',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'szbjg',
      label: '三坐标结果',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'cpzt',
      label: '三坐标检测人员',
      headeralign: 'center',
      align: 'center',
      width: 120,
    }, {
      coltype: 'datetime',
      prop: 'szbjcsj',
      label: '三坐标结果时间',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'wcsj',
      label: '完成时间',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,    
    }, {
      coltype: 'bool',
      prop: 'scbz',
      label: '删除标识',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'bool',
      prop: 'sfbh',
      label: '是否闭环',
      headeralign: 'center',
      align: 'center',
      fixed: 'right',
    }
  ],
  queryapi: {
    url: '/lbj/smjjk/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
