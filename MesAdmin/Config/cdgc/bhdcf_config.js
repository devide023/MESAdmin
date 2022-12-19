{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  operate_fnlist: [],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    },
    deal_handle: function () {
      var l = this.selectlist;
      if (l.length > 0) {
        this.$request('post', '/cdgc/bhdcf/bhdbh', l).then(function (res) {
			
		});
      } else {
        this.$message.warning('请选择项目');
      }
    }
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      width: 150,
      allowcreate: true,
      options: [{
          label: '缸体',
          value: 'SCX01'
        }, {
          label: '电机壳',
          value: 'SCX02'
        }
      ],
      hideoptionval: true
    }, {
      coltype: 'list',
      prop: 'rwzt',
      label: '状态',
      headeralign: 'center',
      align: 'center',
      width: 100,
      options: [{
          label: '创建',
          value: 0
        }, {
          label: '闭环',
          value: 1
        }
      ]
    }, {
      coltype: 'list',
      prop: 'jt',
      label: '机台',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
      allowcreate: true,
      options: [{
          label: '1-1',
          value: '1-1'
        }, {
          label: '1-2',
          value: '1-2'
        }, {
          label: '1-3',
          value: '1-3'
        }, {
          label: '1-4',
          value: '1-4'
        }, {
          label: '2-1',
          value: '2-1'
        }, {
          label: '2-2',
          value: '2-2'
        }, {
          label: '2-3',
          value: '2-3'
        }, {
          label: '2-4',
          value: '2-4'
        }, {
          label: '3-1',
          value: '3-1'
        }, {
          label: '3-2',
          value: '3-2'
        }, {
          label: '3-3',
          value: '3-3'
        }, {
          label: '3-4',
          value: '3-4'
        }
      ],
      hideoptionval: true
    }, {
      coltype: 'list',
      prop: 'cpxh',
      label: '产品型号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
      allowcreate: true,
      options: [{
          label: '1.2T',
          value: '1.2T'
        }, {
          label: '1.4T',
          value: '1.4T',
        }, {
          label: '1.5T',
          value: '1.5T'
        }, {
          label: '1.5L',
          value: '1.5L'
        }, {
          label: '1.6L',
          value: '1.6L'
        }
      ],
	  hideoptionval: true
    }, {
      coltype: 'string',
      prop: 'cpmc',
      label: '产品名称',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'string',
      prop: 'bhbw',
      label: '变化部位',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'string',
      prop: 'fsddbh',
      label: '发生断点编号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }, {
      coltype: 'string',
      prop: 'fxddbh',
      label: '发现断点编号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }, {
      coltype: 'string',
      prop: 'gzxx',
      label: '故障现象',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }, {
      coltype: 'string',
      prop: 'zssl',
      label: '追溯数量',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'list',
      prop: 'yzpcl',
      label: '已制品处理',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120,
      allowcreate: true,
      options: [{
          label: '正常流转',
          value: '1',
        }, {
          label: '隔离待评审',
          value: '2',
        }, {
          label: '报废',
          value: '3',
        }
      ],
	  hideoptionval: true
    }, {
      coltype: 'string',
      prop: 'czygsdd',
      label: '操作员改善断点',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }, {
      coltype: 'string',
      prop: 'czyscsj',
      label: '操作员实测数据',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }, {
      coltype: 'string',
      prop: 'czypdjg',
      label: '操作员判定结果',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }, {
      coltype: 'string',
      prop: 'xcxjgsdd',
      label: '巡检改善断点',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }, {
      coltype: 'string',
      prop: 'xcxjscsj',
      label: '巡检实测数据',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }, {
      coltype: 'string',
      prop: 'xcxjpdjg',
      label: '巡检判定结果',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
    }
  ],
  form: {
    rwzt: '创建',
    scx: 'SCX01',
    cpxh: '',
    cpmc: '',
    bhbw: '',
    fsddbh: '',
    fxddbh: '',
    gzxx: '',
    jt: '',
    zssl: '',
    yzpcl: '',
    czygsdd: '',
    czyscsj: '',
    czypdjg: '',
    scbzgsdd: '',
    scbzscsj: '',
    scbzpdjg: '',
    xcxjgsdd: '',
    xcxjscsj: '',
    xcxjpdjg: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/cdgc/bhdcf/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/cdgc/bhdcf/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/cdgc/bhdcf/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/cdgc/bhdcf/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
