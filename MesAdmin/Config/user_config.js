{
  isgradequery: true,
  isfresh: true,
  isoperate: true,
  pagefuns: {
    
  },
  fields: [{
      coltype: 'bool',
      prop: 'status',
	  dbprop:'ta.status',
      label: '状态',
      headeralign: 'center',
      align: 'center',
      width: 100,
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
      prop: 'code',
	  dbprop:'ta.code',
      label: '用户编码',
      headeralign: 'center',
      align: 'left',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'name',
	  dbprop:'ta.name',
      label: '用户名',
      headeralign: 'center',
      align: 'left',
	  width:200,
    },
	{
      coltype: 'string',
      prop: 'tel',
      label: '手机号码',
      headeralign: 'center',
      align: 'center',
	  width:150,
    },
	{
      coltype: 'list',
      prop: 'role',
      label: '角色',
      headeralign: 'center',
      align: 'left',
	  multiple:true,
	  inioptionapi: {
        method: 'get',
        url: '/role/all'
      },
      options: [],
	  searchable:false
    },
	{
      coltype: 'string',
      prop: 'addusername',
      label: '录入人',
      headeralign: 'center',
      width: 80,
      align: 'left',
    }, {
      coltype: 'datetime',
      prop: 'addtime',
      label: '录入时间',
      headeralign: 'center',
      width: 150,
      align: 'left',
      overflowtooltip: true,
    }
  ],
  form: {
    status: 1,
    code: '',
    name: '',
	tel:'',
    adduser: '',
    addtime: '',
	role:[],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/user/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/user/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/user/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/user/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
