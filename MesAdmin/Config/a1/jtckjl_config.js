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
      label: '设置未读',
      fnname: 'set_jstc_unread',
      btntype: 'text'
    }
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
      if (row.jstcinfo) {
        if (row.jstcinfo.jtly === 1) {
          window.open("http://jsgltj.zsdl.cn/tjjstz/file/" + row.jstcinfo.wjlj);
        } else if (row.jstcinfo.jtly === 0) {
          window.open("http://172.16.201.216:81/技术通知/" + row.jstcinfo.wjlj);
        }
      }
    },
	set_jstc_unread:function(row){
		console.log(row);
		var _this = this;
		if(row.jstcinfo){
			_this.$request("post",'/a1/jtck/del',[row]).then(function(res){
				if(res.code ===1){
					_this.$message.success('设置成功');
				}else{
					_this.$message.error(res.msg);
				}
			});
		}
	}
  },
  fields: [{
      coltype: 'list',
      label: '技通组别',
      prop: 'zbid',
      dbprop: 'zbid',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/a1/jtfp/group/all_zblist'
      },
      hideoptionval: true,
      options: []
    }, {
      coltype: 'string',
      label: '技通编号',
      prop: 'jcbh',
      dbprop: 'ta.jcbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '阅读人',
      prop: 'ydr',
      dbprop: 'ta.ydr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'datetime',
      label: '阅读时间',
      prop: 'ydsj',
      dbprop: 'ta.ydsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'list',
      label: '文件分类',
      prop: 'jstcinfo',
      subprop: 'wjfl',
      dbprop: 'tb.jcbh',
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
      label: '文件名称',
      prop: 'jstcinfo',
      subprop: 'wjlj',
      dbprop: 'tb.jcbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'string',
      label: '技通描述',
      prop: 'jstcinfo',
      subprop: 'jcms',
      dbprop: 'tb.jcbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'date',
      label: '有效期限开始',
      prop: 'jstcinfo',
      subprop: 'yxqx1',
      dbprop: 'tb.yxqx1',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'date',
      label: '有效期限结束',
      prop: 'jstcinfo',
      subprop: 'yxqx2',
      dbprop: 'tb.yxqx2',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '分配标志',
      prop: 'jstcinfo',
      subprop: 'fpflg',
      dbprop: 'tb.fpflg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '分配人',
      prop: 'jstcinfo',
      subprop: 'fpr',
      dbprop: 'tb.fpr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '分配时间',
      prop: 'jstcinfo',
      subprop: 'fpsj',
      dbprop: 'tb.fpsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '创建者',
      prop: 'jstcinfo',
      subprop: 'scry',
      dbprop: 'tb.scry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '创建时间',
      prop: 'jstcinfo',
      subprop: 'scsj',
      dbprop: 'tb.scsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
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
    url: '/a1/jtck/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
