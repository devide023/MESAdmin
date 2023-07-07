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
    add_dbgx_handle: function () {
      var _this = this;
      _this.dialog_title = '机号夹具绑定';
      _this.dialog_width = '30%';
      _this.dialogVisible = true;
      _this.dialog_hidefooter = true;
      _this.dialog_viewpath = 'ducar/bdgx/jjbdgx';
    },
    del_bdgx_handle: function () {
      var _this = this;
      _this.dialog_title = '机号夹具解绑';
      if (_this.selectlist.length > 0) {
        _this.$request('post', '/ducar/jjgx/unbind', _this.selectlist).then(function (res) {
          if (res.code === 1) {
            _this.getlist(_this.queryform);
          } else {
            _this.$message.error(res.msg);
          }
        });
      } else {
        _this.$message.warning('请选项目');
      }
    }
  },
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      width: 100,
      inioptionapi: {
        method: 'get',
        url: '/ducar/baseinfo/scx'
      },
      hideoptionval: true,
      options: [],
    }, {
      coltype: 'string',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
	  width: 100,
    }, {
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width: 150,
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
      label: '夹具号',
      prop: 'jjh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '订单号',
      prop: 'order_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '机型编号',
      prop: 'jx_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '状态编号',
      prop: 'status_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '绑定时间',
      prop: 'bdsj',
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
    url: '/ducar/jjgx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
