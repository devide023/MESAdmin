{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    }
  },
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
      headeralign: 'center',
      align: 'center',
      width: 150,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx'
      },
      options: [],
    }, {
      coltype: 'string',
      label: '刃具类型',
      width: 200,
      prop: 'rjlx',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刃具名称',
      prop: 'rjmc',
      headeralign: 'center',
      width: 200,
      align: 'center',
    }, {
      coltype: 'string',
      label: '标准寿命',
      prop: 'rjbzsm',
      width: 150,
      headeralign: 'center',
      align: 'center',
    }, ],
  form: {
    gcdm: '9902',
    rjlx: '',
    rjmc: '',
    rjbzsm: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/rjxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/rjxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/rjxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/rjxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
