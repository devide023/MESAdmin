{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: false,
  isselect: false,
  pagefuns: {},
  fields: [{
	  coltype: 'list',
      label: '生产线',
      prop: 'scx',
      dbprop: 'scx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      width: 80,
	  inioptionapi: {
        method: 'get',
        url: '/ducar/baseinfo/scx'
      },
	  hideoptionval:true,
	  options:[]
  },{
      coltype: 'string',
      label: '设备编号',
      prop: 'sbbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '设备名称',
      prop: 'sbmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '岗位号',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width:100
    }, {
      coltype: 'list',
      label: '设备类型',
      prop: 'sblx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '拧紧枪',
          value: '拧紧枪'
        }, {
          label: '拧紧轴',
          value: '拧紧轴'
        }
      ],
      hideoptionval: true
    }, {
      coltype: 'list',
      label: '连接类型',
      prop: 'ljlx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: 'PCS直连',
          value: 'PCS直连'
        }, {
          label: 'SCADA',
          value: 'SCADA'
        }, {
          label: 'PLC',
          value: 'PLC'
        }
      ],
      hideoptionval: true
    }, {
      coltype: 'string',
      label: 'IP',
      prop: 'ip',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '端口号',
      prop: 'port',
      width: '100',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'int',
      label: 'plc序号',
      prop: 'plcxh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'bool',
      label: '是否连接',
      prop: 'sflj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }
  ],
  form: {
	  scx:'',
    sbbh: '',
    sbmc: '',
    gwh: '',
    sblx: '',
    ljlx: '',
    ip: '',
    port: '',
    plcxh: '',
    sflj: '',
    bz: '',
    lrr: '',
    lrsj: '',
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
    url: '/ducar/sbxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
