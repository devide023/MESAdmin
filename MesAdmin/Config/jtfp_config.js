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
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      width: 100,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: []
    }, {
      coltype: 'string',
      label: '技通编号',
      prop: 'jtid',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
      label: '技通名称',
      prop: 'jtmc',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '机型',
      prop: 'jx',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'string',
      label: '状态码',
      prop: 'statusno',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr1',
      headeralign: 'center',
      align: 'center',
      width: 80,
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj1',
      headeralign: 'center',
      align: 'center',
      width: 150,
      overflowtooltip: true,
    }, ],
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
