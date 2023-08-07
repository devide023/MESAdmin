{
  isgradequery: true,
  isoperate: false,
  isselect: true,
  pagefuns: {
    select_scx: function (collist, val, row) {
      var _this = this;
      this.$request('get', '/lbj/baseinfo/getscxzx', {
        scx: val
      }).then(function (res) {
        if (res.code === 1) {
          row.scxzxs = res.list.map(function (i) {
            return {
              label: i.scxzxmc,
              value: i.scxzx
            };
          });
        } else {
          _this.$message.error(res.msg);
        }
      });
    }
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: [],
      change_fn_name: 'select_scx',
	  overflowtooltip: true,
	  sortable: true,
    }, {
      coltype: 'list',
      prop: 'scxzx',
      label: '子线',
      headeralign: 'center',
      align: 'center',
      options: [],
      relation: 'scxzxs',
      hideoptionval: true,
	  sortable: true,
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      options: [],
	  optionconfig:{
		  method: 'get',
		  url: '/lbj/baseinfo/gwzdbyscx',
		  querycnf:[{scx:'scx'}]
	  },
	  relation: 'gwhs',
	  overflowtooltip: true,
	  sortable: true,
    }, {
      coltype: 'string',
      prop: 'gwmc',
      label: '岗位名称',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }, {
      coltype: 'list',
      prop: 'gwlx',
      label: '岗位类型',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '机加',
          value: '机加'
        }, {
          label: '检测',
          value: '检测'
        }, {
          label: '打包',
          value: '打包'
        }
      ],
	  overflowtooltip: true,
    }, {
      coltype: 'list',
      prop: 'gwzlx',
      label: '岗位子类型',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '刻字',
          value: '00'
        }, {
          label: '机加',
          value: '01'
        }, {
          label: '清洗烘干',
          value: '02'
        }, {
          label: '检漏',
          value: '03'
        }, {
          label: 'SPC',
          value: '04'
        }, {
          label: '全检',
          value: '05'
        }, {
          label: 'GP12',
          value: '06'
        }, {
          label: '扭力',
          value: '07'
        }, {
          label: '对比仪',
          value: '08'
        }, {
          label: 'MMET',
          value: '09'
        }, {
          label: '检测',
          value: '10'
        }, {
          label: '三坐标',
          value: '11'
        }, {
          label: '二维码码评级',
          value: '12'
        }, {
          label: '检测码打刻',
          value: '13'
        }, {
          label: '螺套检测机',
          value: '14'
        }, {
          label: '打标码',
          value: '15'
        }, {
          label: '压装机-定位销',
          value: '16'
        }
      ],
	  overflowtooltip: true,
    }, {
      coltype: 'list',
      prop: 'gwfl',
      label: '岗位分类',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '人工',
          value: '人工'
        }, {
          label: '自动',
          value: '自动'
        }
      ],
	  overflowtooltip: true,
    }, {
      coltype: 'list',
      prop: 'glgwh',
      label: '管理岗位号',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: [],
	  overflowtooltip: true,
    }, {
      coltype: 'bool',
      prop: 'gzty',
      label: '故障停用',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
	  overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'pcsip',
      label: 'pcsIP',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'bz',
      label: '备注',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'cjqxdl',
      label: '超级权限登录',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'usercode',
      label: '最后登录工号',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'dlsj',
      label: '登录时间',
      headeralign: 'center',
      align: 'center',
	  overflowtooltip: true,
    }
  ],
  queryapi: {
    url: '/lbj/gwzd/list',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/gwzd/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
}
