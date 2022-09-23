{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: false,
  operate_fnlist: [{
      label: '明细',
      fnname: 'view_bill',
      btntype: 'text'
    }
  ],
  pagefuns: {
    view_bill: function (row) {
      var _this = this;
      _this.dialog_title = '';
      _this.dialog_width = '40%';
      _this.dialogVisible = true;
      _this.dialog_viewpath = 'cdgc/jcgl/gtjc/component/gtjc';
      _this.dialog_props = {
        billid: row.id,
        lx: row.cplx
      };
    },
    dialog_save_handle: function () {
      this.dialogVisible = false;
    }
  },
  fields: [{
      coltype: 'date',
      prop: 'rq',
      label: '日期',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'cplx',
      label: '产品类型',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'string',
      prop: 'jth',
      label: '机台号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'jyry',
      label: '检验人员',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'jylb',
      label: '检验类别',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'th',
      label: '图号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'vin',
      label: '二维吗',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'mh',
      label: '模号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'pdjg',
      label: '评定结果',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'cljl',
      label: '处理结论',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'datetime',
      prop: 'clsj',
      label: '处理时间',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }, {
      coltype: 'string',
      prop: 'clr',
      label: '处理人',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
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
    url: '/cdgc/gtjchis/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
