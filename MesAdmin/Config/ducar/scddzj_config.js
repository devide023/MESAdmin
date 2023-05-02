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
      prop: 'scddh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
      width: 150
    }, {
      coltype: 'string',
      label: '物料编码',
      prop: 'zjwl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
      width: 150
    }, {
      coltype: 'string',
      label: '物料名称',
      prop: 'wlmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'string',
      label: '数量',
      prop: 'zjsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
      width: 100
    }, {
      coltype: 'string',
      label: '单位',
      prop: 'zjdw',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
      width: 100
    }, {
      coltype: 'string',
      label: '装配类型',
      prop: 'zplx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
      width: 150
    }, {
      coltype: 'string',
      label: '供应商代码',
      prop: 'gys',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
      width: 150
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
    url: '/ducar/scddzj/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
