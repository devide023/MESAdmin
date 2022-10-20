{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: false,
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
      coltype: 'string',
      label: '生产订单号',
      prop: 'order_no',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150
    }, {
      coltype: 'string',
      label: '物料编码',
      prop: 'bjbm',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'left',
      sortable: true,
      width: 150
    }, {
      coltype: 'string',
      label: '物料名称',
      prop: 'wlmc',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'left',
      sortable: true,
    }, {
      coltype: 'list',
      label: '物料属性',
      prop: 'bjsx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
      options: [{
          label: '大件',
          value: 'A'
        }, {
          label: '小件',
          value: 'B'
        }
      ]
    }, {
      coltype: 'list',
      label: '装配类型',
      prop: 'zplx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
      options: [],
      inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/zplx'
      },
    }, {
      coltype: 'int',
      label: '采购数量',
      prop: 'cgsl',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100
    }, {
      coltype: 'int',
      label: '到货数量',
      prop: 'dhsl',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100
    }, {
      coltype: 'string',
      label: '厂商编码',
      prop: 'csbm',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100
    }, {
      coltype: 'string',
      label: '厂商名称',
      prop: 'csmc',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'left',
      sortable: true,
    }, {
      coltype: 'datetime',
      label: '到货时间',
      prop: 'sjdhsj',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150
    }, {
      coltype: 'datetime',
      label: '创建时间',
      prop: 'cjrq',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150
    },
  ],
  form: {
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/cgjh/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
