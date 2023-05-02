{
  isgradequery: true,
  isbatoperate: false,
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
      if (row.jtly === 1) {
        window.open("http://jsgltj.zsdl.cn/tjjstz/file/" + row.wjlj);
      } else if (row.jtly === 0) {
        window.open("http://172.16.201.216:81/技术通知/" + row.wjlj);
      }
    },
  },
  fields: [{
      coltype: 'list',
      label: '分配组别',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      inioptionapi: {
        method: 'get',
        url: '/a1/jtfp/group/all_zblist'
      },
      width: 100
    }, {
      coltype: 'string',
      label: '技通编号',
      prop: 'jcbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '技通名称',
      prop: 'jcmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      label: '技通描述',
      prop: 'jcms',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      label: '文件名称',
      prop: 'wjlj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'date',
      label: '有效期限结束',
      prop: 'yxqx2',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 130
    }, {
      coltype: 'string',
      label: '阅读人',
      prop: 'ydrs',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100,
      searchable: false,
      overflowtooltip: true
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
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    id: '',
    scx: '',
    jcbh: '',
    lrr: '',
    lrsj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/jtgl/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/jtgl/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/jtgl/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/jtgl/pdmfped_list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
