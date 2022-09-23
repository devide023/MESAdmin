{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  operate_fnlist: [],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    },
  },
  fields: [{
      coltype: 'string',
      prop: 'bhdbh',
      label: '变化点编号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'bhdnr',
      label: '变化点内容',
      headeralign: 'center',
      align: 'center',
      sortable: true,
    }, {
      coltype: 'string',
      prop: 'bhddj',
      label: '变化等级',
      headeralign: 'center',
      align: 'center',
      sortable: true,
    }, {
      coltype: 'string',
      prop: 'sbfs',
      label: '识别方式',
      headeralign: 'center',
      align: 'center',
      sortable: true,
    }, {
      coltype: 'list',
      prop: 'sbtj',
      label: '识别条件',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      options: [{
          label: '自动',
          value: '自动'
        }, {
          label: '手动',
          value: '手动'
        }
      ]
    }
  ],
  form: {
    bhdbh: '',
    bhdnr: '',
    bhddj: '',
    sbfs: '',
    sbtj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/cdgc/bhdgl/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/cdgc/bhdgl/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/cdgc/bhdgl/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/cdgc/bhdgl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
