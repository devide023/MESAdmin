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
      label: '工厂',
      prop: 'gcdm',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx',
      },
      options: [],
    }, {
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
    }, {
      coltype: 'list',
      label: '岗位号',
      prop: 'gwh',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd',
      },
      options: [],
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
      coltype: 'date',
      label: '计划时间',
      prop: 'wbjhsj',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '维保状态',
      prop: 'wbzt',
      headeralign: 'center',
      align: 'center',
      options: [],
    }, {
      coltype: 'string',
      label: '完成人',
      prop: 'wbwcr',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '完成时间',
      prop: 'wbwcsj',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      headeralign: 'center',
      align: 'center',
    }, ],
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
