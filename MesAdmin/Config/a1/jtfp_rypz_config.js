{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'usercode') {
        row.username = '';
        this.$request('get', '/a1/jtfpsysz/get_sysuser', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          }
        });
      }
    },
    select_fn: function (vm, item, row, col) {
      if (col.prop === 'usercode') {
        row.username = item.label;
      }
    }
  },
  fields: [{
      coltype: 'string',
      label: '用户编码',
      prop: 'usercode',
      suggest: 'true',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn',
      width: 150
    }, {
      coltype: 'string',
      label: '用户名称',
      prop: 'username',
      overflowtooltip: true,
      searchable: false,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'list',
      label: '组别',
      prop: 'zbid',
      dbprop: 'zbid',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/a1/jtfp/group/all_zblist'
      },
      hideoptionval: true,
      options: [],
      width: 150
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 200
    },
  ],
  form: {
    usercode: '',
	username:'',
    scx: '',
    lrr: '',
    lrsj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/jtfpsysz/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/jtfpsysz/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/jtfpsysz/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/jtfpsysz/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
