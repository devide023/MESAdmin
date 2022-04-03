{
  isgradequery: false,
  isfresh: false,
  isoperate: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.status = 1;
      row.adduser = this.$store.getters.userinfo.id;
      row.addusername = this.$store.getters.name;
      row.addtime = this.$parseTime(new Date());
      this.list.unshift(row);
    },
  },
  fields: [{
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
      prop: 'code',
      label: '角色编码',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'name',
      label: '名称',
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      prop: 'addusername',
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
      with : 150,
      overflowtooltip: true,
    }
  ],
  form: {
    status: 1,
    code: '',
    name: '',
    adduser: '',
    addusername: '',
    addtime: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/role/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/role/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/role/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/role/list',
    method: 'post',
    callback: function (vm, res) {}
  },
  treeapi: {
    url: '/menu/tree',
    method: 'get'
  },
  userapi: {
    url: '/role/user',
    method: 'get'
  },
  roleuserapi: {
    url: '/role/adduser',
    method: 'post'
  },
  searchuserapi: {
    url: '/role/queryuser',
    method: 'get'
  },
  infoapi: {
    url: '/role/info',
    method: 'get',
  }
}
