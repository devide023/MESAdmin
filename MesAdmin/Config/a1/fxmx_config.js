{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: false,
  isselect: false,
  activated: function (_this) {
    var q = _this.$route.query;
    var keys = Object.keys(q);
    _this.queryform.search_condition = [];
    for (var i = 0; i < keys.length; i++) {
      if (keys[i] === 'kssj') {
        _this.queryform.search_condition.push({
          colname: 'jcsj',
          coltype: 'date',
          oper: '>=',
          value: q[keys[i]]
        });
      } else if (keys[i] === 'jssj') {
        _this.queryform.search_condition.push({
          colname: 'jcsj',
          coltype: 'date',
          oper: '<=',
          value: q[keys[i]]
        });
      } else {
        _this.queryform.search_condition.push({
          colname: keys[i],
          coltype: 'string',
          oper: '=',
          value: q[keys[i]]
        });
      }
    }
	for(var i=0;i<_this.queryform.search_condition.length;i++){
		if(i!==_this.queryform.search_condition.length-1){
			_this.queryform.search_condition[i].logic = 'and';
		}
	}
    _this.getlist(_this.queryform);
  },
  pagefuns: {},
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
      hideoptionval: true,
      options: []
    }, {
      coltype: 'string',
      label: '订单号',
      prop: 'orderno',
      dbprop: 'order_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '机号',
      prop: 'engineno',
      dbprop: 'engine_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '机型',
      prop: 'jxno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '状态码',
      prop: 'statusno',
      dbprop: 'status_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '故障现象',
      prop: 'gzxx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '检测部件',
      prop: 'jcbj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '处理岗位',
      prop: 'jyclgw',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'jcry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'jcsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '故障代码',
      prop: 'faultno',
      dbprop: 'fault_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '故障名称',
      prop: 'faultname',
      dbprop: 'faultname',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      searchable: false,
      width: 100
    }, {
      coltype: 'string',
      label: '处理方式',
      prop: 'clfs',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '实际处理岗位',
      prop: 'sjclgw',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '涉及部件',
      prop: 'sjbjbm',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '涉及厂商',
      prop: 'sjbjcs',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '原因分析',
      prop: 'yyfx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 120
    }, {
      coltype: 'string',
      label: '处理结果',
      prop: 'cljg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '返修机标志',
      prop: 'fxjflg',
      dbprop: 'fxj_flg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '处理录入人',
      prop: 'fxr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'datetime',
      label: '处理录入时间',
      prop: 'fxsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '闭环标志',
      prop: 'bhbz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '闭环人',
      prop: 'bhr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 120
    }, {
      coltype: 'datetime',
      label: '闭环时间',
      prop: 'bhsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    scx: '',
    gwh: '',
    engineno: '',
    statusno: '',
    orderno: '',
    gzxx: '',
    jcbj: '',
    jyclgw: '',
    jcry: '',
    jcsj: '',
    clfs: '',
    sjclgw: '',
    sjbjbm: '',
    sjbjcs: '',
    yyfx: '',
    cljg: '',
    fxjflg: '',
    fxr: '',
    fxsj: '',
    bhbz: '',
    bhr: '',
    bhsj: '',
    faultno: '',
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
    url: '/a1/fxmx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
