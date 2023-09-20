{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: false,
  operate_fnlist: [{
      label: '处理',
      fnname: 'deal_4mbhd_xcxj_handle',
      btntype: 'text',
      condition: [{
          field: 'rwzt',
          oper: '=',
          val: '01'
        }
      ]
    },
  ],
  pagefuns: {
    deal_4mbhd_xcxj_handle: function (row, item) {
      var _this = this;
      row.dealtype = "bhzx_deal";
      var _this = this;
      _this.dialog_title = row.id + '变化点';
      _this.dialog_width = '60%';
      _this.dialogVisible = true;
      _this.dialog_hidefooter = true;
      _this.dialog_viewpath = 'lbj/4mbhd/deal_bhd';
      _this.dialog_props = {
        row: row
      };
    }
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      width: 150,
      sortable: true,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    }, {
      coltype: 'string',
      label: '子线',
      prop: 'scxzxmc',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      width: 100,
      hideoptionval: true,
      sortable: true
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: []
    }, {
      coltype: 'list',
      prop: 'rwzt',
      label: '任务状态',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
      options: [{
          label: '暂存',
          value: '00'
        }, {
          label: '待操作员确认',
          value: '03'
        }, {
          label: '待班组确认',
          value: '05'
        }, {
          label: '待巡检确认',
          value: '07'
        }, {
          label: '闭环',
          value: '49'
        },
      ]
    }, {
      coltype: 'list',
      prop: 'trigtype',
      dbprop: 'trig_type',
      label: '触发类型',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
      options: [{
          label: '自动',
          value: '1'
        }, {
          label: '人工',
          value: '2'
        }
      ]
    }, {
      coltype: 'list',
      prop: 'changetype',
      dbprop: 'change_type',
      label: '变化点类型',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
      options: [{
          label: '人员',
          value: '1'
        }, {
          label: '刀具',
          value: '2'
        }, {
          label: '换产',
          value: '3'
        }, {
          label: '程序号变更',
          value: '4'
        }, {
          label: 'API数据',
          value: '5'
        }
      ]
    }, {
      coltype: 'string',
      prop: 'cpxh',
      label: '产品型号',
      headeralign: 'center',
      align: 'center',
      width: 130,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'jt',
      label: '设备编号',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'bhbw',
      label: '变化部位',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'gzxx',
      label: '故障信息',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'fsddbh',
      label: '发生断点编号',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'fxddbh',
      label: '发现断点编号',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'scbzpdjg',
      label: '判定结果',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'xcxjczrmc',
      label: '确认人',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'xcxjczsj',
      label: '确认时间',
      headeralign: 'center',
      align: 'center',
      width: 100,
      sortable: true,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'cjrmc',
      label: '记录人',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'cjsj',
      label: '记录时间',
      headeralign: 'center',
      align: 'center',
      width: 130,
      overflowtooltip: true,
      sortable: true,
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
    url: '/lbj/bhdjl/bhzx_dealbhd_list',
    method: 'post',
    callback: function (_this, res) {},
  },
}
