{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  operate_fnlist: [],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.pid = 0;
      row.adduser = this.$store.getters.name;
      row.addtime = this.$parseTime(new Date());
      this.list.unshift(row);
    },
  },
  fields: [{
      coltype: 'string',
      prop: 'id',
      label: '菜单ID',
      headeralign: 'center',
      align: 'left',
      width: 100,
    }, {
      coltype: 'bool',
      prop: 'status',
      label: '状态',
      headeralign: 'center',
      align: 'center',
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
      ],
      width: 100,
    }, {
      coltype: 'string',
      prop: 'code',
      label: '菜单编码',
      headeralign: 'center',
      align: 'left',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'name',
      label: '菜单名称',
      headeralign: 'center',
      align: 'left',
      width: 200,
    }, {
      coltype: 'string',
      prop: 'icon',
      label: '图标',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'path',
      label: '菜单路由',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true
    }, {
      coltype: 'int',
      prop: 'seq',
      label: '菜单权重',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'datetime',
      prop: 'addtime',
      label: '创建时间',
      headeralign: 'center',
      align: 'center',
      width: 200,
    }
  ],
  form: {
    id: '',
    pid: 0,
    status: 1,
    name: '',
    icon: '',
    seq: 10,
    adduser: '',
    addtime: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/appmenu/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/appmenu/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/appmenu/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/appmenu/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
