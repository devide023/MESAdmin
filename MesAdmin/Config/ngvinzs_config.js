{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: false,
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
  pagefuns: {},
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'left',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: [],
    }, {
      coltype: 'list',
      label: '子线',
      prop: 'scxzx',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
	  optionconfig:{
		  method: 'get',
		  url: '/lbj/baseinfo/scxzx',
		  querycnf:[{scx:'scx'}]
	  },
	  options:[],
	  relation: 'scxzxs',
    }, {
      coltype: 'string',
      label: '件号',
      prop: 'vin',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '产品编码',
      prop: 'ztbm',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '产品名称',
      prop: 'cpmc',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '工序',
      prop: 'work_flow',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '检测',
          value: '01'
        }, {
          label: '捡漏',
          value: '03'
        }, {
          label: '扭力',
          value: '07'
        }, {
          label: '对比仪',
          value: '08'
        }
      ]
    }, {
      coltype: 'string',
      label: '库存状态',
      prop: 'kczt',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '故障现象',
      prop: 'gzxx',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '断钻头',
          value: 'G01'
        }, {
          label: '孔大',
          value: 'G02'
        }, {
          label: '断丝锥',
          value: 'G03'
        }, {
          label: '加工歪',
          value: 'G04'
        }, {
          label: '碰摔伤',
          value: 'G05'
        }, {
          label: '深度超差',
          value: 'G06'
        }, {
          label: '平面啃刀',
          value: 'G07'
        }, {
          label: '平面铣斜',
          value: 'G08'
        }, {
          label: '平面铣短',
          value: 'G09'
        }, {
          label: '坐标超差',
          value: 'G10'
        }, {
          label: '状态加工错',
          value: 'G11'
        }, {
          label: '螺纹牙平烂牙',
          value: 'G12'
        }, {
          label: '压伤',
          value: 'G13'
        }, {
          label: '粗糙度超差',
          value: 'G14'
        }, {
          label: '撞刀',
          value: 'G15'
        }, {
          label: '失圆',
          value: 'G16'
        }, {
          label: '加工漏气',
          value: 'G17'
        }, {
          label: '其他',
          value: 'G18'
        }, {
          label: '刀痕',
          value: 'G19'
        }, {
          label: '调试',
          value: 'G20'
        }, {
          label: '打码不合格',
          value: 'G21'
        }, {
          value: 'L01',
          label: '坯件漏气'
        }, {
          value: 'L02',
          label: '气孔'
        }, {
          value: 'L03',
          label: '缺料'
        }, {
          value: 'L04',
          label: '变形'
        }, {
          value: 'L05',
          label: '碰伤'
        }, {
          value: 'L06',
          label: '霉斑'
        }, {
          value: 'L07',
          label: '报告清退'
        }, {
          value: 'L08',
          label: '毛坯尺寸超差'
        }, {
          value: 'L09',
          label: '客户原因退回'
        }, {
          value: 'L10',
          label: '其他'
        }, {
          value: 'L11',
          label: '锯伤'
        }, {
          value: 'L12',
          label: '色差'
        }, {
          value: 'L13',
          label: '黄斑'
        }, {
          value: 'L14',
          label: '杂质'
        }, {
          value: 'L15',
          label: '黑皮'
        }, {
          value: 'L16',
          label: '裂纹'
        }, {
          value: 'L17',
          label: '多料'
        }, {
          value: 'L18',
          label: '单边'
        }, {
          value: 'L19',
          label: '毛坯孔偏'
        }, {
          value: 'L20',
          label: '毛坯孔深'
        }, {
          value: 'L21',
          label: '夹渣'
        }
      ]
    }, {
      coltype: 'datetime',
      label: '扫描时间',
      prop: 'smsj',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '入库人员',
      prop: 'smry',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '出库时间',
      prop: 'cksj',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '出库人员',
      prop: 'ckry',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '处理方式',
      prop: 'clfs',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '处理结果',
      prop: 'cljg',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '处理时间',
      prop: 'clsj',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '处理人',
      prop: 'clr',
      sortable: true,
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
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
    url: '/lbj/scbhg/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
