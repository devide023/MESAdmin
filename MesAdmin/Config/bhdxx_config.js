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
      coltype: 'list',
      prop: 'gcdm',
      label: '工厂',
      headeralign: 'center',
      align: 'center',
      width: 80,
      fixed: 'left',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx'
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'bhdbh',
      label: '变化点编号',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'bhdnr',
      label: '变化点内容',
      headeralign: 'center',
      align: 'left',
	  width:200,
	  overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'bhddj',
      label: '变化点等级',
      headeralign: 'center',
      width: 100,
      align: 'center',
    }, {
      coltype: 'list',
      prop: 'sbfs',
      label: '识别方式',
      headeralign: 'center',
      align: 'center',
      width: 120,
	  options: [{label:'自动',value:'自动'},{label:'人工',value:'人工'}]
    }, {
      coltype: 'list',
      prop: 'sbtj',
      label: '识别条件',
      headeralign: 'center',
      align: 'center',
      width: 120,
	  options: [{label:'手动',value:'手动'},{label:'自动',value:'自动'}]
    },
  ],
  form: {
    gcdm: '9902',
    bhdbh: '',
    bhdnr: '',
    bhddj: '',
    sbfs: '',
    sbtj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/bhdxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/bhdxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/bhdxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/bhdxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
