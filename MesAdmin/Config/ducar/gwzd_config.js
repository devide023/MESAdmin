{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/ducar/gwzd/readxls', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else {
              _this.$message.error(result.msg);
            }
            _this.getlist(_this.queryform);
          });
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
    },
    import_by_replace(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/ducar/gwzd/readxls_by_replace', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
            _this.getlist(_this.queryform);
          });
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
    },
    import_by_zh(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/ducar/gwzd/readxls_by_zh', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
            _this.getlist(_this.queryform);
          });
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
    },
    export_excel: function (_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields.filter(function (i) {
              return ['gzty'].indexOf(i.prop) === -1;
            }), expdatalist);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
  },
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_template_file: function () {
      window.open('http://192.168.1.111:7002/template/Ducar/岗位站点.xlsx?r=' + Math.random());
    },
	view_khgw_info(vm,row,col){
		var _this = this;
      _this.dialog_title = '['+row.gwmc + ']看护岗位';
      _this.dialog_width = '60%';
      _this.dialogVisible = true;
      _this.dialog_hidefooter = true;
      _this.dialog_viewpath = 'ducar/gwzd/khgw';
      _this.dialog_props = {
        row: row
      };
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
      coltype: 'list',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  optionconfig:{
		  method: 'get',
		  url: '/ducar/baseinfo/gwzdbyscx',
		  querycnf:[{scx:'scx'}]
	  },
	  options:[],
      width: 150,
    }, {
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    },
	{
      coltype: 'string',
      label: '是否看护岗位',
      prop: 'iskhgw',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  link_fn_name:'view_khgw_info',
	  width:120
    },
	{
      coltype: 'list',
      label: '岗位类型',
      prop: 'gwlx',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '装配',
          value: '装配'
        }, {
          label: '测试',
          value: '测试'
        }, {
          label: '返工',
          value: '返工'
        }, {
          label: '部装',
          value: '部装'
        }
      ],
      hideoptionval: true,
    }, {
      coltype: 'list',
      label: '岗位分类',
      prop: 'gwfl',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
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
      hideoptionval: true,
    }, {
      coltype: 'list',
      label: '工序号',
      prop: 'workflow',
      dbprop: 'work_flow',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      options: [{
          label: '托盘绑定',
          value: '1'
        }, {
          label: '扭力',
          value: '2'
        }, {
          label: '耐压仪',
          value: '3'
        }, {
          label: '辅料绑定',
          value: '4'
        }
      ],
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '首末岗位',
      prop: 'smgw',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      options: [{
          label: '首岗位',
          value: 'S'
        }, {
          label: '末岗位',
          value: 'M'
        }, {
          label: '下线岗位',
          value: 'X'
        }
      ],
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'bool',
      label: '换产校验',
      prop: 'ishcjy',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    },{
      coltype: 'bool',
      label: '解绑产品',
      prop: 'jbfdj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'list',
      label: '自动合格',
      prop: 'iszdhg',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '自动合格',
          value: 1
        }, {
          label: '进站即合格',
          value: 2
        }, {
          label: '全部螺栓拧紧即合格',
          value: '全部螺栓拧紧即合格'
        }, {
          label: '测试合格即合格',
          value: '全部螺栓拧紧即合格'
        }
      ]
    }, {
      coltype: 'bool',
      label: '故障停用',
      prop: 'gzty',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      label: 'PCSIP',
      prop: 'ip',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }
  ],
  form: {
    gcdm: '101',
    scx: '',
    gwh: '',
    gwmc: '',
    gwlx: '装配',
    gwfl: '人工',
    workflow: '',
    shbz: 'Y',
    gzty: 'N',
    pcsip: '',
    bz: '',
    lrr: '',
    lrsj: '',
    shr: '',
    shsj: '',
    khgw: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/ducar/gwzd/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/ducar/gwzd/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/ducar/gwzd/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/gwzd/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
