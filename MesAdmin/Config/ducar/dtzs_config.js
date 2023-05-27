{
  isgradequery: false,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: false,
  querybarname:'ReportDtzs',
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
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    },{
      coltype: 'string',
      label: '机号',
      prop: 'engine_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '岗位编号',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    },{
      coltype: 'string',
      label: '夹具号',
      prop: 'jjh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    },{
      coltype: 'string',
      label: '机型',
      prop: 'jx_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '状态编码',
      prop: 'status_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '结果',
      prop: 'jcjg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '检测员',
      prop: 'jcry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'datetime',
      label: '检测时间',
      prop: 'jcsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
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
    url: '/ducar/report/dtzs',
    method: 'post',
    callback: function (vm, res) {},
  },
}
