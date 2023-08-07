{
  isgradequery: true,
  isbatoperate: true,
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
      label: '件号',
      prop: 'engine_no',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
	  width:350
    }, {
      coltype: 'string',
      label: '批次号',
      prop: 'batch_no',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '产品编码',
      prop: 'status_no',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '订单号',
      prop: 'order_no',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '刻字时间',
      prop: 'print_time',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '刻字状态',
      prop: 'status_flag',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '已生成',
          value: 'E'
        }, {
          label: '打印完成',
          value: 'F'
        }, {
          label: '打印中',
          value: 'P'
        }, {
          label: '打印取消',
          value: 'C'
        }, {
          label: '计调撤销',
          value: 'H'
        }
      ]
    },
	{
      coltype: 'string',
      label: '总检下线',
      prop: 'in_flag',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '扫描入库',
      prop: 'sjin_flag',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '包装下线',
      prop: 'finish_flag',
      overflowtooltip: true,
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
    url: '/lbj/batgl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
