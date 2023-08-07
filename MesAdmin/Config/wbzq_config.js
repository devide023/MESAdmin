{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  pagefuns: {
	  
  },
  batoperate:{
	  export_excel(_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields, expdatalist);
        } else if(res.code === 0) {
          this.$message.error(res.msg);
        }
      });
    },
  },
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902',
      },
      options: [],
	  overflowtooltip: true,
    }, 
	{
      coltype: 'list',
      label: '子线',
      prop: 'scxzx',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      width: 100,
	  optionconfig:{
		  method: 'get',
		  url: '/lbj/baseinfo/scxzx',
		  querycnf:[{scx:'scx'}]
	  },
      options: [],
      relation: 'scxzxs',
      hideoptionval: true,
    },
	{
      coltype: 'list',
      label: '设备名称',
      prop: 'sbbh',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/sbxx',
      },
      options: [],
	  overflowtooltip: true,
    }, {
      coltype: 'int',
      label: '顺序',
      prop: 'wbsh',
      headeralign: 'center',
      align: 'center',
	  width:80,
    }, {
      coltype: 'string',
      label: '维保内容',
      prop: 'wbxx',
      headeralign: 'center',
	  width:180,
	  overflowtooltip: true,
      align: 'left',
    }, {
      coltype: 'datetime',
      label: '计划开始时间',
      prop: 'wbjhsj',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }, 
	{
      coltype: 'datetime',
      label: '计划结束时间',
      prop: 'wbjhsjend',
	  dbprop:'wbjhsj_end',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    },
	{
      coltype: 'list',
      label: '维保状态',
      prop: 'wbzt',
      headeralign: 'center',
      align: 'center',
      options: [
	  {
		  label:'已完成',
		  value:'已完成'
	  },
	  {
		  label:'计划中',
		  value:'计划中'
	  },
	  ],
    }, {
      coltype: 'string',
      label: '完成人',
      prop: 'wbwcr',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }, {
      coltype: 'datetime',
      label: '完成时间',
      prop: 'wbwcsj',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }],
  form: {
    gcdm: '',
    scx: '',
    gwh: '',
    wbsh: '',
    wbxx: '',
    wbjhsj: '',
    wbzt: '',
    wbwcr: '',
    wbwcsj: '',
    lrr: '',
    lrsj: '',
	scxzx:'',
	scxzxs:[],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/wbzq/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/wbzq/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/wbzq/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/wbzq/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
