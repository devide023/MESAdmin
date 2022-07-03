{
  isgradequery: false,
  isfresh: true,
  isoperate: true,
  fields: [{
      coltype: 'string',
      prop: 'code',
      label: '编码',
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'bool',
      prop: 'status',
      label: '状态',
      headeralign: 'center',
      align: 'center',
      width: 80,
      activevalue: 1,
      inactivevalue: 0,
      istag: true,
      tagtypes: [{
          label: 'success',
          value: 1
        }, {
          label: 'danger',
          value: 0
        }
      ],
      options: [{
          label: '启用',
          value: 1
        }, {
          label: '禁用',
          value: 0
        }
      ]
    }, {
      coltype: 'string',
      prop: 'name',
      label: '名称',
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'list',
      prop: 'menutype',
      label: '类型',
      headeralign: 'center',
      align: 'left',
      width: 80,
      align: 'center',
      istag: true,
      tagtypes: [{
          label: 'primary',
          value: '01'
        }, {
          label: 'warning',
          value: '02'
        }, {
          label: 'info',
          value: '03'
        }, {
          label: 'danger',
          value: '04'
        },
		{
          label: 'success',
          value: '05'
        }
      ],
      options: [{
          label: '菜单',
          value: '01'
        }, {
          label: '页面',
          value: '02'
        }, {
          label: '功能',
          value: '03'
        }, {
          label: '字段',
          value: '04'
        },
		{
          label: '批量',
          value: '05'
        }
      ]
    }, {
      coltype: 'string',
      prop: 'icon',
      label: '图标',
      headeralign: 'center',
      align: 'center',
      width: 50,
      isicon: true,
    }, {
      coltype: 'string',
      prop: 'routepath',
      label: '路由',
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      prop: 'viewpath',
      label: '视图',
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'int',
      prop: 'seq',
      label: '排序',
      headeralign: 'center',
      align: 'center',
      width: 80,
    }, {
      coltype: 'string',
      prop: 'configpath',
      label: '配置文件',
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      prop: 'componentname',
      label: '组件名称',
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      prop: 'btntxt',
      label: '按钮文本',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'fnname',
      label: '函数名称',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'btntype',
      label: '按钮类型',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'adduser',
      label: '录入人',
      headeralign: 'center',
      align: 'center',
      width: 80,
    }, {
      coltype: 'datetime',
      prop: 'addtime',
      label: '录入时间',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }
  ],
  form: {
    status: 1,
    code: '',
    name: '',
    menutype: '',
    icon: '',
    routepath: '',
    viewpath: '',
    seq: 10,
    configpath: '',
    componentname: '',
    adduser: '',
    addtime: '',
    isdb: false,
    isedit: true
  },
  addhandle: function (vm) {},
  addapi: {
    url: '/menu/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/menu/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/menu/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/menu/list',
    method: 'post',
    callback: function (vm, res) {}
  },
}
