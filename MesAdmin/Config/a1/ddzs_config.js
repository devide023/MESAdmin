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
      label: '订单号',
      prop: 'order_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, , {
      coltype: 'string',
      label: '机号',
      prop: 'vin',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
    {
      coltype: 'string',
      label: '状态编码',
      prop: 'ztbm',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刻字时间',
      prop: 'print_time',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }
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
    url: '/a1/sjzs/ddzs/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
