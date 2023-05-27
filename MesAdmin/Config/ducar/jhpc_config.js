{
  isgradequery: true,
  isbatoperate: false,
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
  pagefuns: {
    add_handle: function () {
      let _this = this;
      var row = _this.$deepClone(_this.pageconfig.form);
      row.sjscsj = this.$parseTime(new Date());
      row.lrsj = this.$parseTime(new Date());
      _this.list.unshift(row);
    },
    suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'orderno') {
        this.$request('get', '/ducar/jhpc/orderno_by_code', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          }
        });
      }
    },
    select_fn: function (vm, item, row, col) {
      if (col.prop === 'orderno') {
        this.$request('get', '/ducar/jhpc/orderinfo_by_orderno', {
          orderno: item.value
        }).then(function (res) {
          if (res.code === 1) {
            row.orderinfo.order_no = res.orderinfo.order_no;
            row.scx = res.orderinfo.scx;
            row.orderinfo.jx = res.orderinfo.jx;
            row.orderinfo.ztbm = res.orderinfo.ztbm;
            row.pcsl = res.orderinfo.scsl;
            row.orderinfo.scddlx = res.orderinfo.scddlx;
            row.orderinfo.xsbz = res.orderinfo.xsbz;
          }
        });
      }
    },
  },
  fields: [{
      coltype: 'date',
      label: '实际生成日期',
      prop: 'sjscsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 200
    }, {
      coltype: 'string',
      suggest: true,
      label: '订单号',
      prop: 'orderno',
      dbprop: 'order_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150,
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn'
    }, {
      coltype: 'string',
      label: '序号',
      prop: 'xh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    },{
      coltype: 'string',
      label: '排产数量',
      prop: 'pcsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '机型',
      prop: 'orderinfo',
      subprop: 'jx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '状态编码',
      prop: 'orderinfo',
      subprop: 'ztbm',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '生产数量',
      prop: 'orderinfo',
      subprop: 'scsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '订单类型',
      prop: 'orderinfo',
      subprop: 'scddlx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'orderinfo',
      subprop: 'xsbz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    orderno: '',
    xh: '',
    sjscsj: '',
    lrsj: '',
    scx: '',
	pcsl:'',
    orderinfo: {
      order_no: '',
      jx: '',
      ztbm: '',
      scsl: '',
      xsbz: '',
      scddlx: ''
    },
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/ducar/jhpc/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/ducar/jhpc/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/ducar/jhpc/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/jhpc/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
