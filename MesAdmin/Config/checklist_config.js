{
  isgradequery: true,
  isbatoperate: true,
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
      label: '明细',
      fnname: 'view_check_mx',
      btntype: 'text'
    },
	{
      label: '编辑',
      fnname: 'edit_check_bill',
      btntype: 'text'
    }
  ],
  pagefuns: {
    view_check_mx: function (row) {
      this.$router.push({
        path: '/smjgl/checkbill?edit=0&billid=' + row.id
      });
    },
	edit_check_bill:function(row){
		this.$router.push({
        path: '/smjgl/checkbill?edit=1&billid=' + row.id
      });
	}
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: [],
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      width: 100
    }, {
      coltype: 'date',
      label: '检测日期',
      prop: 'rq',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '班次',
      prop: 'bc',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
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
      coltype: 'list',
      label: '首末件',
      prop: 'smjbs',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  options:[
	  {label:'首件',value:'S'},
	  {label:'末件',value:'M'},
	  ],
	  hideoptionval:true,
	  width:100
    },	{
      coltype: 'string',
      label: '产品型号',
      prop: 'cpxh',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  width:150
    }, {
      coltype: 'string',
      label: '检测结果',
      prop: 'jcjg',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '部门名称',
      prop: 'bmmc',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'list',
      label: '互检结果',
      prop: 'shjg',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  options:[{
		  label:'合格',
		  value:'合格'
	  },
	  {
		  label:'不合格',
		  value:'不合格'
	  }],
	  hideoptionval:true,
      width: 100
    },{
      coltype: 'string',
      label: '互检人',
      prop: 'shr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '互检时间',
      prop: 'shsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'list',
      label: '巡检结果',
      prop: 'xjjg',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  options:[{
		  label:'合格',
		  value:'合格'
	  },
	  {
		  label:'不合格',
		  value:'不合格'
	  }],
	  hideoptionval:true,
      width: 100
    },
	{
      coltype: 'string',
      label: '巡检人',
      prop: 'xjr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '巡检时间',
      prop: 'xjsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    },{
      coltype: 'string',
      label: '修改人',
      prop: 'xgr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '修改时间',
      prop: 'xgsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    scx: '',
    bmmc: '',
    rq: '',
    bc: '',
    cpxh: '',
    cpmc: '',
    gxmc: '',
    khmc: '',
    jcjg: '',
    bz: '',
    lrr: '',
    lrsj: '',
    shr: '',
    shsj: '',
    vin: '',
    jjh: '',
    xgr: '',
    xgsj: '',
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
    url: '/lbj/checkbill/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/checkbill/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
