{
  isgradequery: true,
  isbatoperate: true,
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
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'left',
	  inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/gwzd'
      },
      options: [],
	  width:150
    }, {
      coltype: 'string',
      label: '机型',
      prop: 'jxno',
      dbprop: 'jx_no',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '状态码',
      prop: 'statusno',
      dbprop: 'status_no',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '点检编号',
      prop: 'djno',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100
    }, {
      coltype: 'string',
      label: '点检内容',
      prop: 'djxx',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      label: '点检结果',
      prop: 'djjg',
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
    }, {
      coltype: 'string',
      label: '点检人',
      prop: 'lrr',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
    }, {
      coltype: 'datetime',
      label: '点检时间',
      prop: 'lrsj',
      overflowtooltip: true,      
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }
  ],
  form: {
    id: '',
    gcdm: '',
    scx: '',
    gwh: '',
    jxno: '',
    statusno: '',
    djno: '',
    djxx: '',
    djjg: '',
    bz: '',
    lrr: '',
    lrsj: '',
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
    url: '/a1/djjl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
