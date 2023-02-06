{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  pagefuns: {
    tf_handle: function () {
      var _this = this;
      if (_this.selectlist.length > 0) {
        _this.$request('post', '/cdgc/gttf/edit', _this.selectlist).then(function (res) {
          if (res.code === 1) {
            _this.$message.success(res.msg);
            _this.getlist(_this.queryform);
          } else {
            _this.$message.error(res.msg);
          }
        });
      } else {
        _this.$message.warning('请选择要退废的项目');
      }
    }
  },
  fields: [{
      coltype: 'string',
      label: '机台号',
      prop: 'jth',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '产品名称',
      prop: 'cpmc',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '件号',
      prop: 'vin',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '工废原因',
      prop: 'yx',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '退废人',
      prop: 'tfr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '退废时间',
      prop: 'tfsj',
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
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      searchable: false,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }
  ],
  form: {
    vin: '',
    yx: '',
    jth: '',
    lrr: '',
    tfr: '',
    tfsj: '',
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
    url: '/cdgc/gttf/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/cdgc/gttf/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
