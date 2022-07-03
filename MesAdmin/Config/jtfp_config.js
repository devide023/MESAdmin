{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: true,
  pagefuns: {
    download_pfd: function (row) {
      console.log(row);
    },
  },
  operate_fnlist: [{
      label: '下载Pdf',
      fnname: 'download_pfd',
      btntype: 'text'
    }, ],
  fields: [{
      coltype: 'list',
      prop: 'gcdm',
      label: '工厂',
      headeralign: 'center',
      align: 'center',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx'
      },
      options: []
    }, {
      coltype: 'list',
      prop: 'scx',
	  dbprop:'t.scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      width: 150,
	  sortable:true,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    }, {
      coltype: 'list',
      prop: 'gwh',
	  dbprop:'t.gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      width: 100,
	  sortable:true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: []
    }, {
      coltype: 'string',
      label: '技通编号',
      prop: 'jcbh',
	  dbprop:'t2.jcbh',
      headeralign: 'center',
      align: 'center',
      width: 130,
	  overflowtooltip: true,
	  sortable:true,
    }, {
      coltype: 'string',
      label: '技通名称',
      prop: 'jtmc',
	  dbprop:'t2.jtmc',
      headeralign: 'center',
      align: 'left',
	  sortable:true,
      overflowtooltip: true,
    },  {
      coltype: 'string',
      label: '状态码',
      prop: 'statusno',
	  dbprop:'t.status_no',
      headeralign: 'center',
      align: 'center',
	  sortable:true,
      width: 150,
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
	  dbprop:'t.bz',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr1',
	  dbprop:'t.lrr1',
      headeralign: 'center',
      align: 'center',
      width: 80,
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj1',
	  dbprop:'t.lrsj1',
      headeralign: 'center',
      align: 'center',
      width: 150,
	  sortable:true,
      overflowtooltip: true,
    }],
  addapi: {
    url: '/lbj/jtfp/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/jtfp/del',
    method: 'post',
    callback: function (_this, res) {
      _this.get_wfp_list();
    }
  },
  editapi: {
    url: '/lbj/jtfp/edit',
    method: 'post',
    callback: function (_this, res) {}
  },
  queryapi: {
    url: '/lbj/jtfp/list',
    method: 'post',
    callback: function (_this, res) {
      _this.recordcount = res.resultcount;
      _this.get_wfp_list();
    }
  }
}
