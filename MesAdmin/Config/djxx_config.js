{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isselect: false,
  pagefuns: {},
  batoperate: {
    export_excel(_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields, expdatalist);
        } else if (res.code === 0) {
          this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {},
    import_by_zh(_this, res) {},
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'left',
      width: 130,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      change_fn_name: function (_this, collist, val, row) {
        row.gwh = '';
        var gwcol = collist.filter(i => i.prop === 'gwh');
        _this.$request('get', '/lbj/baseinfo/scx_gwh?scx=' + val).then(function (res) {
          if (res.code === 1) {
            if (gwcol) {
              gwcol[0].choose_options = res.list;
            }
          }
        });
      },
      clear_fn_name: function (_this, row) {
        row.gwh = '';
      },
      options: []
    }, {
      coltype: 'list',
      label: '子线',
      prop: 'scxzx',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      width: 120,
      optionconfig: {
        method: 'get',
        url: '/lbj/baseinfo/scxzx',
        querycnf: [{
            scx: 'scx'
          }
        ]
      },
      relation: 'scxzxs',
      hideoptionval: true,
      sortable: true
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      options: [],
      choose_options: [],
      relation: 'gwhs',
	  width:120
    }, {
      coltype: 'string',
      prop: 'engineno',
      dbprop: 'engine_no',
      label: '件号',
      headeralign: 'center',
      align: 'center',
      width: 180,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'statusno',
      dbprop: 'status_no',
      label: '状态码',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
      prop: 'djno',
      label: '点检编号',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'djxx',
      label: '点检内容',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'list',
      prop: 'djjg',
      label: '点检结果',
      headeralign: 'center',
      align: 'center',
      width: 100,
      options: [{
          label: '合格',
          value: 'Y'
        }, {
          label: '不合格',
          value: 'N'
        }
      ],
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'bz',
      label: '备注',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'left',
      width: 80,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }
  ],
  form: {
    gcdm: '',
    scx: '',
    gwh: '',
    engineno: '',
    statusno: '',
    djno: '',
    djxx: '',
    djjg: '',
    bz: '',
    lrr: '',
    lrsj: '',
    scxzx: '',
    scxzxs: [],
    gwhoptions: [],
    isdb: false,
    isedit: true,
  },
  addapi: {
    url: '/lbj/djxx/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/djxx/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/djxx/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/djxx/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
