{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: true,
  operate_fnlist: [{
      label: '明细',
      fnname: 'view_bill',
      btntype: 'text',
      callback: 'dialog_close_handle'
    }, {
      label: '编辑',
      fnname: 'edit_bill',
      btntype: 'text',
      callback: 'dialog_save_handle',
      condition: [{
          field: 'const_userid',
          oper: 'in',
          val: [1,25]
        }
      ]
    }
  ],
  batoperate: {},
  pagefuns: {
    edit_bill: function (row) {
      var _this = this;
      _this.dialog_title = '';
      _this.dialog_width = '60%';
      _this.dialogVisible = true;
      _this.dialog_hidefooter = true;
      _this.dialog_viewpath = 'cdgc/jjbgl/gtjjb/component/gtjjb';
      _this.dialog_props = {
        isread: false,
        calcrq: row.rq,
        calcbc: row.bc,
      };
    },
    view_bill: function (row) {
      var _this = this;
      _this.dialog_title = '';
      _this.dialog_width = '50%';
      _this.dialogVisible = true;
      _this.dialog_hidefooter = false;
      _this.dialog_viewpath = 'cdgc/jjbgl/gtjjb/component/gtjjb';
      _this.dialog_props = {
        isread: true,
        calcrq: row.rq,
        calcbc: row.bc
      };
    },
    dialog_close_handle: function () {
      this.dialogVisible = false;
    },
    dialog_save_handle: function () {}
  },
  fields: [{
      coltype: 'date',
      prop: 'rq',
      label: '日期',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 130,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 200,
    },{
      coltype: 'string',
      prop: 'bc',
      label: '班次',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'string',
      prop: 'jbr',
      label: '交班人',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'string',
      prop: 'dbzz',
      label: '当班组长',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'string',
      prop: 'slry',
      label: '上料岗位',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'string',
      prop: 'mcry',
      label: '毛刺岗位',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'string',
      prop: 'jyry',
      label: '检验岗位',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, 
	{
      coltype: 'string',
      prop: 'unit1ok',
      label: '一单元OK',
      headeralign: 'center',
      align: 'center',
      sortable: true,
	  width:100,
      overflowtooltip: true,
    },
	{
      coltype: 'string',
      prop: 'unit1ng',
      label: '一单元NG',
      headeralign: 'center',
      align: 'center',
	  width:100,
      sortable: true,
      overflowtooltip: true,
    },
	{
      coltype: 'string',
      prop: 'unit2ok',
      label: '二单元OK',
      headeralign: 'center',
      align: 'center',
	  width:100,
      sortable: true,
      overflowtooltip: true,
    },
	{
      coltype: 'string',
      prop: 'unit2ng',
      label: '二单元NG',
      headeralign: 'center',
      align: 'center',
	  width:100,
      sortable: true,
      overflowtooltip: true,
    },
	{
      coltype: 'string',
      prop: 'unit3ok',
      label: '三单元OK',
      headeralign: 'center',
      align: 'center',
	  width:100,
      sortable: true,
      overflowtooltip: true,
    },
	{
      coltype: 'string',
      prop: 'unit3ng',
      label: '三单元NG',
      headeralign: 'center',
      align: 'center',
	  width:100,
      sortable: true,
      overflowtooltip: true,
    },
	{
      coltype: 'string',
      prop: 'zlqk',
      label: '质量情况',
      headeralign: 'center',
      align: 'center',
      sortable: true,
	  width:130,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'sbqk',
      label: '设备情况',
      headeralign: 'center',
      align: 'center',
	  width:130,
      sortable: true,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'qtqk',
      label: '其他情况',
      headeralign: 'center',
      align: 'center',
      sortable: true,
	  width:130,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '制单人',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100
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
    url: '/cdgc/gtjjb/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/cdgc/gtjjb/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
