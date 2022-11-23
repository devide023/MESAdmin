{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  batoperate: {
    export_excel: function (_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields, expdatalist);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
  },
  pagefuns: {},
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/scx'
      },
      width: 100
    }, {
      coltype: 'string',
      label: '技通编号',
      prop: 'jcbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '技通名称',
      prop: 'jcmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      label: '技通描述',
      prop: 'jcms',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      label: '文件名称',
      prop: 'wjlj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'date',
      label: '有效期限结束',
      prop: 'yxqx2',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 130
    }, {
      coltype: 'string',
      label: '分配标志',
      prop: 'fpflg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '分配人',
      prop: 'fpr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '分配时间',
      prop: 'fpsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    id: '',
    scx: '',
    jcbh: '',
    lrr: '',
    lrsj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/jtgl/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/jtgl/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/jtgl/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/jtgl/scxfpjllist',
    method: 'post',
    callback: function (vm, res) {},
  },
}
