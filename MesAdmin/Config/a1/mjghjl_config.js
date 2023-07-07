{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: false,
  isselect: false,
    querybarname: 'ReportGsjComponent',
  componentlist: ['DataDetailComponent'],
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
  pagefuns: {
  },
  fields: [{
      coltype: 'string',
      label: '序号',
      prop: 'rowno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, 
	{
      coltype: 'datetime',
      label: '日期',
      prop: 'rq',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    },
	{
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '开始机号',
      prop: 'ksjh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '结束机号',
      prop: 'jsjh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '岗位人员',
      prop: 'gwry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '巡检确认',
      prop: 'xjqr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
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
    url: '/a1/report/mjghjl',
    method: 'post',
    callback: function (vm, res) {},
  },
}
