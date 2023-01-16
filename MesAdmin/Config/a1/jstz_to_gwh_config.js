{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: false,
  operate_fnlist: [{
      label: '查看Pdf',
      fnname: 'download_jstcpdf',
      btntype: 'text'
    }, {
      label: '技通分配',
      fnname: 'jstz_fp',
      btntype: 'text',
      callback: 'dialog_save_handle'
    },
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
    download_jstcpdf: function (row) {
      var _this = this;
	  if(row.jtly===1){
      window.open("http://jsgltj.zsdl.cn/tjjstz/file/" + row.wjlj);
	  }else if(row.jtly===0){
		window.open("http://172.16.201.216:81/技术通知/" + row.wjlj);  
	  }
    },
    jstz_fp: function (row, item) {
      var _this = this;
      _this.dialog_title = row.jcbh + '分配';
      _this.dialog_width = '30%';
      _this.dialogVisible = true;
      _this.dialog_hidefooter = true;
      _this.dialog_viewpath = 'tja1/jstz/jtfp';
      _this.dialog_fnitem = item;
      _this.dialog_props = {
        row: row
      };
    },
    dialog_save_handle: function (vm) {
      console.log(vm)
    }
  },
  fields: [{
      coltype: 'string',
      label: '技通编号',
      prop: 'jcbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'list',
      label: '文件分类',
      prop: 'wjfl',
      searchable: false,
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '技通',
          value: '技术通知'
        }, {
          label: '变更',
          value: '变更通知'
        }, {
          label: '质量',
          value: '质量通知'
        }, ],
      hideoptionval: true,
      width: 100
    }, {
      coltype: 'string',
      label: '技通名称',
      prop: 'jcmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, 
	{
      coltype: 'string',
      label: '文件名称',
      prop: 'wjlj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    },
	{
      coltype: 'date',
      label: '有效期限开始',
      prop: 'yxqx1',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'date',
      label: '有效期限结束',
      prop: 'yxqx2',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '分配标志',
      prop: 'fpflg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '分配人',
      prop: 'fpr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '分配时间',
      prop: 'fpsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '创建者',
      prop: 'scry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '创建时间',
      prop: 'scsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    jcbh: '',
    jcmc: '',
    jcms: '',
    wjlj: '',
    jwdx: '',
    scry: '',
    scpc: '',
    scsj: '',
    yxqx1: '',
    yxqx2: '',
    fpflg: '',
    fpsj: '',
    fpr: '',
    wjfl: '',
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
    url: '/a1/jtgl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
