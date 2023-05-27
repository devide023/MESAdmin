{
  isgradequery: false,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: false,
  querybarname: 'ReportFaultComponent',
  componentlist: ['DataDetailComponent'],
  operate_fnlist: [{
      label: '明细',
      fnname: 'view_fxmx_handle',
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
    view_fxmx_handle: function (row) {
      var _this = this;
	  var cxcs = _this.$refs.query_bar_component.search_condition;
	  var kssj = cxcs.filter(function(i){return i.colname==='kssj'});
	  var jssj = cxcs.filter(function(i){return i.colname==='jssj'});
	  var rq1='';rq2='';
	  if(kssj){
		  rq1 = kssj[0].value.substring(0,10);
	  }
	  if(jssj){
		  rq2 = jssj[0].value.substring(0,10);
	  }
	  var url = '/zlgl/fxmx?jxno=' + row.jxno+'&kssj='+rq1+'&jssj='+rq2;
      _this.$router.push({
        path: url
      });
    },
  },
  fields: [{
      coltype: 'string',
      label: '机型',
      prop: 'jxno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '检测数',
      prop: 'jcsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '合格数',
      prop: 'hgsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '不合格数',
      prop: 'ngsl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '合格率%',
      prop: 'hgl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }
  ],
  childfields: [{
      coltype: 'string',
      label: '故障代码',
      prop: 'faultno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '故障名称',
      prop: 'faultname',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 200
    }, {
      coltype: 'string',
      label: '发生次数',
      prop: 'sl',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
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
    url: '/a1/report/zj_zxjc_jl',
    method: 'post',
    callback: function (vm, res) {},
  },
  detailapi: {
    url: '/a1/report/zj_zxjc_jl_mx',
    method: 'post',
    callback: function (vm, res) {},
  }
}
