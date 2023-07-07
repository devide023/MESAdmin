{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
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
  pagefuns: {},
  fields: [{
      coltype: 'string',
      label: '序号',
      prop: 'rowno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:80
    }, {
      coltype: 'string',
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'datetime',
      label: '日期',
      prop: 'rq',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '点检项目',
      prop: 'jclb',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '点检内容',
      prop: 'jcyq',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      label: '点检时间',
      prop: 'jcd',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '点检结果',
      prop: 'jcjg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
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
    url: '/a1/report/jcdjjl',
    method: 'post',
    callback: function (vm, res) {},
  },
}
