{
  isgradequery: true,
  isbatoperate: true,
  isoperate: true,
  isfresh: true,
  isselect: true,
  operate_fnlist: [{
      label: '查看Pdf',
      fnname: 'download_jstcpdf',
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
      var _this = this;
      this.$request('get', '/a1/jtfpscx/read_pdm_jstz', {
        jcbh: row.jcbh
      }).then(function (res) {});
      if (row.jtly === 1) {
        window.open("http://jsgltj.zsdl.cn/tjjstz/file/" + row.wjlj);
      } else if (row.jtly === 0) {
        window.open("http://172.16.201.216:81/技术通知/" + row.wjlj);
      }
    }
  },
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      dbprop: 'scx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/ducar/baseinfo/scx'
      },
      hideoptionval: true,
      options: []
    }, {
      coltype: 'string',
      label: '岗位编号',
      prop: 'gwh',
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    },
{
      coltype: 'string',
      label: '机型',
      prop: 'jxno',
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    },
	{
      coltype: 'string',
      label: '状态编号',
      prop: 'statusno',
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    },
	{
      coltype: 'string',
      label: '技通编号',
      prop: 'jstc',
      subprop: 'jcbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'list',
      label: '文件分类',
      prop: 'jstc',
      subprop: 'wjfl',
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
      prop: 'jstc',
      subprop: 'jcmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'string',
      label: '文件名称',
      prop: 'jstc',
      subprop: 'wjlj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'date',
      label: '有效期限开始',
      subprop: 'yxqx1',
      prop: 'jstc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'date',
      label: '有效期限结束',
      prop: 'jstc',
      subprop: 'yxqx2',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '分配标志',
      prop: 'jstc',
      subprop: 'fpflg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '分配人',
      prop: 'jstc',
      subprop: 'fpr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '分配时间',
      prop: 'jstc',
      subprop: 'fpsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '创建者',
      prop: 'jstc',
      subprop: 'scry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '创建时间',
      prop: 'jstc',
      subprop: 'scsj',
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
    url: '/ducar/jtfp/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/ducar/jtfp/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/jtfp/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
