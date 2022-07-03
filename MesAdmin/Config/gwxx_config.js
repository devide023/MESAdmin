{
  isgradequery: true,
  isoperate: false,
  isselect: true,
  pagefuns: {},
  fields: [{
      coltype: 'list',
      prop: 'gcdm',
      label: '工厂',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx'
      },
      options: []
    }, {
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
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
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'gwmc',
      label: '岗位名称',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      prop: 'gwlx',
      label: '岗位类型',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '机加',
          value: '机加'
        }, {
          label: '检测',
          value: '检测'
        }, {
          label: '打包',
          value: '打包'
        }
      ]
    }, {
      coltype: 'list',
      prop: 'gwfl',
      label: '岗位分类',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '人工',
          value: '人工'
        }, {
          label: '自动',
          value: '自动'
        }
      ]
    }, {
      coltype: 'list',
      prop: 'glgwh',
      label: '管理岗位号',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: []
    }, {
      coltype: 'bool',
      prop: 'gzty',
      label: '故障停用',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      prop: 'pcsip',
      label: 'pcsIP',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'bz',
      label: '备注',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'cjqxdl',
      label: '超级权限登录',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'usercode',
      label: '最后登录工号',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'dlsj',
      label: '登录时间',
      headeralign: 'center',
      align: 'center',
    }
  ],
  queryapi: {
    url: '/lbj/gwzd/list',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/gwzd/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
}
