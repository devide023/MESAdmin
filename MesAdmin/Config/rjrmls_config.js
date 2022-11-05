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
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
	  options:[]
    },{
      coltype: 'string',
      label: '刀柄号',
      prop: 'dbh',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '刀柄类型',
      prop: 'dblx',
	  searchable: false,
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '设备编号',
      prop: 'sbbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, 
    {
      coltype: 'string',
      label: '刃具类型',
      prop: 'rjlx',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刃具标准寿命',
      prop: 'bzsm',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刃磨时寿命',
      prop: 'dqsm',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刃磨人',
      prop: 'rmr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    },
    {
      coltype: 'datetime',
      label: '刃磨时间',
      prop: 'rmsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }
  ],
  form: {},
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
    url: '/lbj/rjrmls/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
