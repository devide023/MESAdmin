{
  isgradequery: true,
  isselect: true,
  isoperate: false,
  pagefuns: {},
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    },
	{
      coltype: 'list',
      prop: 'scxzx',
      label: '子线',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
	  options: [],
      width: 80,
	  optionconfig:{
		  method: 'get',
		  url: '/lbj/baseinfo/scxzx',
		  querycnf:[{scx:'scx'}]
	  },
      relation: 'scxzxs',
	  hideoptionval: true
    },
	{
      coltype: 'string',
      prop: 'sbbh',
      label: '设备编号',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'sbmc',
      label: '设备名称',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      overflowtooltip: true,
      options: []
    }, {
      coltype: 'string',
      prop: 'sblx',
      label: '设备类型',
      headeralign: 'center',
      overflowtooltip: true,
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'sbczl',
      label: '设备残值率',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'sbyz',
      label: '设备原值',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'sbzj',
      label: '设备折旧',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'ljlx',
      label: '连接类型',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'ip',
      label: 'IP',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'port',
      label: '端口号',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'sfky',
      label: '是否可用',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'sflj',
      label: '是否连接',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'bz',
      label: '备注',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }
  ],
  editapi: {
    url: '/lbj/sbxx/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/sbxx/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
