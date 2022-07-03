{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  operate_fnlist: [{
      label: '卸载',
      fnname: 'uninstall_rjlx_handle',
      btntype: 'text'
    },
  ],
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/dbrjly/readxls', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
              _this.getlist(_this.queryform);
            } else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
          });
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
    },
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
          _this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {
		_this.$loading().close();
		_this.$message.error('暂不支持替换导入！');
	},
    import_by_zh(_this, res) {
		_this.$loading().close();
		_this.$message.error('暂不支持综合导入！');
	},
  },
  pagefuns: {
	old2new_handle:function(){
		var _this = this;
      if (_this.selectlist.length > 0) {
        this.$request('post', '/lbj/dbrjly/old2new', _this.selectlist).then(function (res) {
          if (res.code === 1) {
            _this.$message.success(res.msg);
          } else if (res.code === 0) {
            _this.$message.error(res.msg);
          }
        });
      } else {
        _this.$message.warning('请选择刃具');
      }
	},
    rjxz_handle: function () {
      var _this = this;
      if (this.selectlist.length > 0) {
        this.$confirm("你确定要卸载刀柄刃具?", "警告", {
          type: "warning",
          cancelButtonClass: "el-button--primary",
          confirmButtonClass: "el-button--danger",
        }).then(function () {
          var ids = _this.selectlist.map((i) => i.id);
          _this.$request('post', '/lbj/dbrjly/uninstall', ids).then(function (res) {
            if (res.code === 1) {
              _this.$message.success(res.msg);
              _this.getlist(_this.queryform);
            } else if (res.code === 0) {
              _this.$message.error(res.msg);
            }
          });
        })
      } else {
        this.$message.warning('请选择要卸载的刀柄刃具');
      }
    },
    uninstall_rjlx_handle: function (row) {
      console.log(row);
      var _this = this;
      if (!row.rjid) {
        this.$message.error('当前刀柄未绑定');
      } else {
        this.$confirm("你确定要卸载该刃具?", "警告", {
          type: "warning",
          cancelButtonClass: "el-button--primary",
          confirmButtonClass: "el-button--danger",
        }).then(function () {
          _this.$request('post', '/lbj/dbrjly/uninstall', [row.id]).then(function (r) {
            if (r.code === 1) {
              _this.$message.success('刃具卸载成功');
              _this.getlist(_this.queryform);
            } else if (r.code === 0) {
              _this.$message.error(r.msg);
            }
          });

        });
      }
    },
    add_handle: function () {
      this.dialogVisible = true;
    },
    change_rj_handle: function () {
      this.installVisible = true;
    },
    rjrm_handle: function () {
      var _this = this;
      if (this.selectlist.length > 0) {
        this.$confirm("刃磨将重置刃具当前寿命确定要刃磨?", "警告", {
          type: "warning",
          cancelButtonClass: "el-button--primary",
          confirmButtonClass: "el-button--danger",
        }).then(function () {
          var postdata = _this.selectlist.map(t => t.id);
          _this.$request('post', '/lbj/dbrjly/zxrjrm', postdata).then(function (res) {
            if (res.code === 1) {
              _this.$message.success(res.msg);
              _this.getlist(_this.queryform);
            } else if (res.code === 0) {
              _this.$message.error(res.msg);
            }
          });
        });
      } else {
        this.$message.warning('请选择刃具');
      }
    },
    download_template_file() {
      window.open('http://172.16.201.125:7002/template/lbj/刀柄刃具在线.xlsx');
    },
  },
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
      dbprop: 'ta.gcdm',
      headeralign: 'center',
      align: 'center',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx',
      },
      options: [],
    }, {
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      dbprop: 'ta.scx',
      headeralign: 'center',
      align: 'center',
      width: 180,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902',
      },
      options: [],
    }, {
      coltype: 'string',
      label: '设备编号',
      prop: 'sbbh',
      dbprop: 'ta.sbbh',
      headeralign: 'center',
      align: 'center',
      sortable: true,
    }, {
      coltype: 'string',
      label: '设备名称',
      prop: 'basesbxx',
      subprop: 'sbmc',
	  dbprop:'td.sbmc',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '刀柄编号',
      prop: 'dbh',
      dbprop: 'ta.dbh',
      headeralign: 'center',
      align: 'center',
      sortable: true,
    }, {
      coltype: 'string',
      label: '刀柄类型',
      prop: 'basedbxx',
      subprop: 'dblx',
	  dbprop:'tb.dblx',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '刀柄名称',
      prop: 'basedbxx',
      subprop: 'dbmc',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刃具类型',
      prop: 'rjlx',
      dbprop: 'ta.rjlx',
      headeralign: 'center',
      align: 'left',
      width: 150,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '刃具名称',
      prop: 'baserjxx',
      subprop: 'rjmc',
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      label: '标准寿命',
      prop: 'rjbzsm',
      dbprop: 'ta.rjbzsm',
      headeralign: 'center',
      align: 'center',
      sortable: true,
    }, {
      coltype: 'string',
      label: '当前寿命',
      prop: 'rjdqsm',
      dbprop: 'ta.rjdqsm',
      headeralign: 'center',
      align: 'center',
      sortable: true,
    }, {
      coltype: 'progress',
      label: '刃具状态',
      prop: 'rjzt',
      headeralign: 'center',
      align: 'center',
      width: 100,
      sortable: true,
	  searchable:false,
    }, {
      coltype: 'string',
      label: '领用人',
      prop: 'rjlyr',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '领用时间',
      prop: 'rjlysj',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }],
  trbginfo: {
    colname: 'rjzt',
    logiclist: [{
        logic: 'between',
        val0: 90,
        val1: 95,
        classname: 'warning-row',
      }, {
        logic: 'between',
        val0: 95,
        val1: 100,
        classname: 'danger-row',
      }, {
        logic: '>',
        val0: 100,
        classname: 'error-row',
      },
    ]
  },
  form: {
    gcdm: '',
    scx: '',
    sbbh: '',
    dbh: '',
    rjlx: '',
    rjbzsm: '',
    rjdqsm: '',
    dblyr: '',
    dblysj: '',
    rjlyr: '',
    rjlysj: '',
    basedbxx: {},
    baserjxx: {},
    basesbxx: {},
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/dbrjly/scly',
    method: 'post',
    callback: function (_this, res) {},
  },
  editapi: {
    url: '/lbj/dbrjly/edit',
    method: 'post',
    callback: function (_this, res) {},
  },
  delapi: {
    url: '/lbj/dbrjly/del',
    method: 'post',
    callback: function (_this, res) {},
  },
  queryapi: {
    url: '/lbj/dbrjly/list',
    method: 'post',
    callback: function (_this, res) {},
  },
}
