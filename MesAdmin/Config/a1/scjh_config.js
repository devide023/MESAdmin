{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: false,
  isselect: false,
  operate_fnlist: [{
      label: '采购计划',
      fnname: 'view_cgjh',
      btntype: 'text'
    }
  ],
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
    view_cgjh: function (row) {
      var _this = this;
      this.$router.push({
        path: "/jhgl/cgjh",
        query: {
          order_no: row.order_no
        },
      });
    },
  },
  fields: [{
	  coltype: 'list',
      label: '生产线',
      prop: 'scx',
      dbprop: 'scx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      width: 80,
	  inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/scx'
      },
	  hideoptionval:true,
	  options:[]
  },{
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
      label: '机型',
      prop: 'jx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'left',
      sortable: true,
      width: 100
    }, {
      coltype: 'string',
      label: '状态编码',
      prop: 'ztbm',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'left',
      sortable: true,
      width: 100
    }, {
      coltype: 'string',
      label: '生产班次',
      prop: 'scbc',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '生产数量',
      prop: 'scsl',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'date',
      label: '生产时间',
      prop: 'scsj',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100
    }, {
      coltype: 'string',
      label: '装配计划数量',
      prop: 'zpsl',
      overflowtooltip: true,
      headeralign: 'center',
      sortable: true,
      align: 'center',
      width: 130
    }, {
      coltype: 'date',
      label: '装配时间',
      prop: 'zpsj',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'string',
      label: '工艺数量',
      prop: 'gylx_jx_qty',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      searchable: false,
      width: 80
    }
  ],
  trbginfo: [{
      colname: 'gylx_jx_qty',
      logiclist: [{
          logic: '=',
          val0: 0,
          classname: 'error-row',
        }
      ]
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
    url: '/a1/scjh/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
