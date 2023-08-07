{
  isgradequery: true,
  isoperate: false,
  isselect: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
	select_scx: function (collist, val, row) {
      var _this = this;
      row.gwh = '';
      this.$request('get', '/lbj/baseinfo/scx_gwh?scx=' + val).then(function (res) {
        if (res.code === 1) {
          row.gwhs = res.list;
        }
      });
    }
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: [],
	  change_fn_name: 'select_scx',
      sortable: true
    }, {
      coltype: 'string',
      prop: 'statusno',
      dbprop: 'status_no',
      label: '状态编码',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      sortable: true
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位号',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
      optionconfig: {
        method: 'get',
        url: '/lbj/baseinfo/gwzdbyscx',
        querycnf: [{
            scx: 'scx'
          }
        ]
      },
      options: [],
      relation: 'gwhs',
      sortable: true
    }, {
      coltype: 'string',
      prop: 'zpsx',
      label: '装配顺序',
      headeralign: 'center',
      overflowtooltip: true,
      align: 'center',
      sortable: true
    }, {
      coltype: 'list',
      prop: 'mj',
      label: '免检',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '每台检',
          value: 'N'
        }, {
          label: '首检',
          value: 'S'
        }, {
          label: '免检',
          value: 'Y'
        },
      ],
      overflowtooltip: true,
      sortable: true
    }, {
      coltype: 'bool',
      prop: 'fsbz',
      label: '互锁标志',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
      overflowtooltip: true,
      sortable: true
    }, {
      coltype: 'bool',
      prop: 'shbz',
      label: '审核标志',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
      overflowtooltip: true,
      sortable: true
    }, {
      coltype: 'bool',
      prop: 'sfzp',
      label: '是否装配',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
      overflowtooltip: true,
      sortable: true
    }, {
      coltype: 'string',
      prop: 'bz',
      label: '备注',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
      sortable: true
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
      sortable: true
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
      sortable: true
    }
  ],
  form: {
    gcdm: '9902',
    scx: '',
    statusno: '',
    gwh: '',
    zpsx: '',
    mj: 'N',
    fsbz: 'Y',
    shbz: 'Y',
    sfzp: 'Y',
    bz: '',
	lrr:'',
	lrsj:'',
    gwhs: [],
    isdb: false,
    isedit: true
  },
  
  addapi: {
    url: '/lbj/gylx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/gylx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/gylx/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
