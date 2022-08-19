{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: true,
  operate_fnlist: [{
      label: '关联菜单',
      fnname: 'relate_menus',
      btntype: 'text'
    }, {
      label: '关联用户',
      fnname: 'relate_users',
      btntype: 'text'
    }
  ],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.adduser = this.$store.getters.name;
      row.addtime = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    relate_menus: function (row) {
      var _this = this;
      this.roleid = row.id;
      this.$request('get', '/lbj/approle/role_menu_list', {
        roleid: row.id
      }).then(function (res) {
        if (res.code === 1) {
          _this.menuids = res.list.map(function(i){
			  return i.id;
		  });
          _this.role_menu_dialogVisible = true;
        }
      });
    },
    relate_users: function (row) {
      var _this =this;
      this.roleid = row.id;
      this.$request('get', '/lbj/approle/role_user_list', {
        roleid: row.id
      }).then(function (res) {
        if (res.code === 1) {
          _this.user_tel = res.list;
          _this.role_user_dialogVisible = true;
        }
      });

    },
  },
  fields: [{
      coltype: 'bool',
      prop: 'status',
      label: '状态',
      headeralign: 'center',
      align: 'left',
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
      width: 150
    }, {
      coltype: 'string',
      prop: 'name',
      label: '名称',
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'datetime',
      prop: 'addtime',
      label: '创建时间',
      headeralign: 'center',
      align: 'left',
      width: 200,
    }, {
      coltype: 'string',
      prop: 'adduser',
      label: '创建人',
      headeralign: 'center',
      align: 'left',
    }
  ],
  form: {
    status: 1,
    name: '',
    addtime: '',
    adduser: '',
    rolemenus: [],
    roleuser: [],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/approle/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/approle/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/approle/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/approle/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
