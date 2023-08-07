{
  isgradequery: false,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  pagefuns: {
    select_scx: function (collist, val, row) {
      var _this = this;
      this.$request('get', '/lbj/baseinfo/getscxzx', {
        scx: val
      }).then(function (res) {
        if (res.code === 1) {
          row.scxzxs = res.list.map(function(i){return {label:i.scxzxmc,value:i.scxzx};});
        } else {
          _this.$message.error(res.msg);
        }
      });
    }
  },
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: [],
      change_fn_name: 'select_scx'
    }, {
      coltype: 'list',
      label: '子线',
      prop: 'scxzx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
	  optionconfig:{
		  method: 'get',
		  url: '/lbj/baseinfo/scxzx',
		  querycnf:[{scx:'scx'}]
	  },
      relation: 'scxzxs',
      hideoptionval: true,
    },{
      coltype: 'string',
      label: '参数名',
      prop: 'paramkey',
      dbprop: 'param_key',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '参数值',
      prop: 'paramvalue',
      dbprop: 'param_value',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }
  ],
  form: {
    gcdm: '9902',
    scx: '',
    scxzx: '',
    paramkey: '',
    paramvalue: '',
    bz: '',
    scxzxs: [],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/setting/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/setting/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
