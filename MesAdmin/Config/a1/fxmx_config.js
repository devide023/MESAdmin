{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: false,
  isselect: false,
  pagefuns: {},
  fields: [
  {
      coltype: 'string',
      label: '订单号',
      prop: 'orderno',
      dbprop: 'order_no',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
	 {
      coltype: 'string',
      label: '机号',
      prop: 'engineno',
      dbprop: 'engine_no',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
  {
      coltype: 'string',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '状态码',
      prop: 'statusno',
      dbprop: 'status_no',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },  {
      coltype: 'string',
      label: '故障现象',
      prop: 'gzxx',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '检测部件',
      prop: 'jcbj',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '处理岗位',
      prop: 'jyclgw',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'jcry',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'jcsj',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '处理方式',
      prop: 'clfs',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '实际处理岗位',
      prop: 'sjclgw',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '涉及部件',
      prop: 'sjbjbm',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '涉及厂商',
      prop: 'sjbjcs',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '原因分析',
      prop: 'yyfx',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:120
    }, {
      coltype: 'string',
      label: '处理结果',
      prop: 'cljg',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '返修机标志',
      prop: 'fxjflg',
      dbprop: 'fxj_flg',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '处理录入人',
      prop: 'fxr',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'datetime',
      label: '处理录入时间',
      prop: 'fxsj',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '闭环标志',
      prop: 'bhbz',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '闭环人',
      prop: 'bhr',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:120
    }, {
      coltype: 'datetime',
      label: '闭环时间',
      prop: 'bhsj',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '故障代码',
      prop: 'faultno',
      dbprop: 'fault_no',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }
  ],
  form: {
    gwh: '',
    engineno: '',
    statusno: '',
    orderno: '',
    gzxx: '',
    jcbj: '',
    jyclgw: '',
    jcry: '',
    jcsj: '',
    clfs: '',
    sjclgw: '',
    sjbjbm: '',
    sjbjcs: '',
    yyfx: '',
    cljg: '',
    fxjflg: '',
    fxr: '',
    fxsj: '',
    bhbz: '',
    bhr: '',
    bhsj: '',
    faultno: '',
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
    url: '/a1/fxmx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
