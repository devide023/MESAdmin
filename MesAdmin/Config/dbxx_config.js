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
    }
  },
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx',
      },
      options: [],
    }, {
      coltype: 'string',
      label: '刀柄名称',
      prop: 'dbmc',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '刀柄类型',
      prop: 'dblx',
      headeralign: 'center',
      align: 'center',
      options: [],
    }, {
      coltype: 'string',
      label: '刀柄号',
      prop: 'dbh',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'date',
      label: '采购时间',
      prop: 'cgsj',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '刀柄状态',
      prop: 'dbzt',
      headeralign: 'center',
      align: 'center',
      options: [],
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入时间',
      prop: 'lrsj',
      headeralign: 'center',
      align: 'center',
    }, ],
  form: {
    gcdm: '9902',
    dbmc: '',
    dblx: '',
    dbh: '',
    cgsj: '',
    dbzt: '',
    lrr: '',
    lrsj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/dbxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/dbxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/dbxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/dbxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
