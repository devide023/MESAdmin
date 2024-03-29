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
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
  },
  fields: [{
      coltype: 'string',
      label: '订单号',
      prop: 'orderno',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 120
    }, {
      coltype: 'string',
      label: '序号',
      prop: 'xh',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 80
    }, {
      coltype: 'list',
      label: '生产班次',
      prop: 'scbc',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '白班',
          value: '白班'
        }, {
          label: '中班',
          value: '中班'
        }, {
          label: '夜班',
          value: '夜班'
        }
      ],
      hideoptionval: true,
      width: 120
    }, {
      coltype: 'string',
      label: '生产数量',
      prop: 'scsl',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'date',
      label: '生产时间',
      prop: 'scsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '排产数量',
      prop: 'zpsl',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'date',
      label: '排产时间',
      prop: 'zpsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '机型',
      prop: 'jx',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '状态编码',
      prop: 'ztbm',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'date',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }
  ],
  form: {
    orderno: '',
    scx: '',
    xh: '',
    scbc: '',
    scsl: '',
    scsj: '',
    zpsl: '',
    zpsj: '',
    jx: '',
    ztbm: '',
    bz: '',
    scbz: '',
    lrr: '',
    lrsj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/cdgc/scjh/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/cdgc/scjh/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/cdgc/scjh/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/cdgc/scjh/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
