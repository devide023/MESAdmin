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
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
  },
  fields: [{
      coltype: 'string',
      label: '件号',
      prop: 'vin',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, 
	{
      coltype: 'string',
      label: '机台号',
      prop: 'jth',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
	  overflowtooltip: true,
    },
	{
      coltype: 'list',
      label: '检测结果',
      prop: 'lx',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '修复后报废',
          value: '报废'
        }, {
          label: '修复后不合格',
          value: '不合格'
        }
      ],
      hideoptionval: true
    }, {
      coltype: 'string',
      label: '故障信息',
      prop: 'gzxx',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '原因',
      prop: 'yxxx',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '操作人',
      prop: 'czr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
    },
	{
      coltype: 'datetime',
      label: '操作时间',
      prop: 'czsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:180,
    },
	{
      coltype: 'string',
      label: '退废人',
      prop: 'tfr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:100,
    },
	{
      coltype: 'datetime',
      label: '退废时间',
      prop: 'tfsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:180,
    },
	{
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
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
    vin: '',
    lx: '',
    gzxx: '',
    yxxx: '',
    lrr: '',
    lrsj: '',
	jth:'',
	czr:'',
	czsj:'',
	tfr:'',
	tfsj:'',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/cdgc/nokmark/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/cdgc/nokmark/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/cdgc/nokmark/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/cdgc/nokmark/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
