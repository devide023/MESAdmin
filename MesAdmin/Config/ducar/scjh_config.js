{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
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
  operate_fnlist: [{
      label: '订单组件',
      fnname: 'view_scddzj_mx',
      btntype: 'text'
    }
  ],
  pagefuns: {
    view_scddzj_mx: function (row) {
      this.$router.push({
        path: '/jhgl/scddzj?scddh=' + row.order_no
      });
    },
    yqt_handle: function () {
      var _this = this;
      if (_this.selectlist.length > 0) {
        _this.$request('post', '/ducar/zpjh/yqt', _this.selectlist).then(function (res) {
          _this.$alert(res.msg,'校验结果',{dangerouslyUseHTMLString:true}).then(function(){
			  _this.getlist(_this.queryform);
		  }).catch(function(){
			  _this.getlist(_this.queryform);
		  });
        });
      } else {
        _this.$message.warning('请选项目');
      }
    },
    wqt_handle: function () {
      var _this = this;
      if (_this.selectlist.length > 0) {
		  _this.$request('post', '/ducar/zpjh/wqt', _this.selectlist).then(function (res) {
          _this.$alert(res.msg,'校验结果',{dangerouslyUseHTMLString:true}).then(function(){
			  _this.getlist(_this.queryform);
		  }).catch(function(){
			  _this.getlist(_this.queryform);
		  });
        });
	  }
      else {
        _this.$message.warning('请选项目');
      }
    },
    gylx_jy_handle: function () {
      var _this = this;
      if (_this.selectlist.length > 0) {
		  _this.$request('post', '/ducar/zpjh/gylx', _this.selectlist).then(function (res) {
		  _this.$alert(res.msg,'校验结果',{dangerouslyUseHTMLString:true}).then(function(){
			  _this.getlist(_this.queryform);
		  }).catch(function(){
			  _this.getlist(_this.queryform);
		  });
        });
	  }
      else {
        _this.$message.warning('请选项目');
      }
    },
    bom_jy_handle: function () {
      var _this = this;
      if (_this.selectlist.length > 0) {
		  _this.$request('post', '/ducar/zpjh/bom', _this.selectlist).then(function (res) {
          _this.$alert(res.msg,'校验结果',{dangerouslyUseHTMLString:true}).then(function(){
			  _this.getlist(_this.queryform);
		  }).catch(function(){
			  _this.getlist(_this.queryform);
		  });
        });
	  }
      else {
        _this.$message.warning('请选项目');
      }
    },
	all_jy_handle:function(){
		var _this = this;
		if (_this.selectlist.length > 0){
			_this.$request('post', '/ducar/zpjh/alljy', _this.selectlist).then(function (res) {
          _this.$alert(res.msg,'校验结果',{dangerouslyUseHTMLString:true}).then(function(){
			  _this.getlist(_this.queryform);
		  }).catch(function(){
			  _this.getlist(_this.queryform);
		  });
        });
		}else {
        _this.$message.warning('请选项目');
      }
	}
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
        url: '/ducar/baseinfo/scx'
      },
      hideoptionval: true,
      options: [],
    }, {
      coltype: 'string',
      label: '订单号',
      prop: 'order_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
      width: 150
    },
	{
      coltype: 'string',
      label: '状态',
	  prop: 'orderjy',
      subprop: 'status',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    },
	 {
      coltype: 'string',
      label: '齐套校验',
	  prop: 'orderjy',
      subprop: 'qdjy',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  cellbg:'#ff4949',
	  cellbgval:'N',
    },	
	{
      coltype: 'string',
      label: '工艺路线校验',
	  prop: 'orderjy',
      subprop: 'gylxjy',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  cellbg:'#ff4949',
	  cellbgval:'N',
    },{
      coltype: 'string',
      label: 'BOM校验',
	  prop: 'orderjy',
      subprop: 'gdbomjy',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  cellbg:'#ff4949',
	  cellbgval:'N',
    },
	{
      coltype: 'string',
      label: '订单类型',
      prop: 'scddlx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '生产数量',
      prop: 'scsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'date',
      label: '排产日期',
      prop: 'jhsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '机型',
      prop: 'jx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'string',
      label: '状态编码',
      prop: 'ztbm',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'string',
      label: '客户代码',
      prop: 'khdm',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '销售计划号',
      prop: 'jhh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '销售备注',
      prop: 'xsbz',
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
    url: '/ducar/zpjh/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
