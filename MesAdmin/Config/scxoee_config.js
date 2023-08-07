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
          _this.$request('get', '/lbj/scxoee/readxls', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
              _this.getlist(_this.queryform);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else {
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
    import_by_replace: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/scxoee/readxls_by_replace', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
              _this.getlist(_this.queryform);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
          })
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
    },
    import_by_zh: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/scxoee/readxls_by_zh', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
              _this.getlist(_this.queryform);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
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
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    },
    del_oee_handle: function () {
      var _this = this;
      if (_this.selectlist.length > 0) {
        _this.$confirm("你确定要删除?", "警告", {
          type: "warning",
          cancelButtonClass: "el-button--primary",
          confirmButtonClass: "el-button--danger",
        }).then(function () {
          var cachedata = _this.selectlist.filter(function (i) {
            return !i.isdb;
          });
          if (cachedata.length > 0) {
            cachedata.forEach((t) => {
              let pos = _this.list.findIndex(
                  (i) => i.rowkey === t.rowkey);
              if (pos !== -1) {
                _this.list.splice(pos, 1);
              }
            });
          }

        });
      } else {
        _this.$message.warning('请选项目');
      }
    },
    rq_change_handle: function (vm, val, row, col) {
      this.$request('post', '/lbj/scxoee/oeebyrq', {
        scx: row.scx,
        rq: val
      }).then(function (res) {
        if (res.code === 1) {
          row.hgpsl = res.proinfo.hgpsl;
          row.bhgpsl = res.proinfo.bhgpsl;
        } else {
          vm.$message.error(res.msg);
        }
      });
    },
    select_scx: function (collist, val, row) {
      this.$request('get', '/lbj/scxoee/oeebyscx', {
        scx: val
      }).then(function (res) {
        if (res.code === 1) {
          row.rq = res.oee.rq;
          row.dlsjjam = res.oee.dlsjjam;
          row.dlsjwait = res.oee.dlsjwait;
          row.fjhtjsj = res.oee.fjhtjsj;
          row.gzsj = res.oee.gzsj;
          row.hdsj = res.oee.hdsj;
          row.hgpl = res.oee.hgpl;
          row.hgpsl = res.oee.hgpsl;
          row.bhgpsl = res.oee.bhgpsl;
          row.hxsj = res.oee.hxsj;
          row.jgllsj = res.oee.jgllsj;
          row.jhtjsj = res.oee.jhtjsj;
          row.jhyxsj = res.oee.jhyxsj;
          row.jhzxsj = res.oee.jhzxsj;
          row.jph = res.oee.jph;
          row.lljp = res.oee.lljp;
          row.oeereal = res.oee.oeereal;
          row.oeetarget = res.oee.oeetarget;
          row.px = res.oee.px;
          row.xx = res.oee.xx;
          row.qttjsj = res.oee.qttjsj;
          row.sfjs = res.oee.sfjs;
          row.sjcl = res.oee.sjcl;
          row.sjkdl = res.oee.sjkdl;
          row.sjyxsj = res.oee.sjyxsj;
          row.sjkdl = res.oee.sjkdl;
          row.teep = res.oee.teep;
          row.tjsj = res.oee.tjsj;
          row.xnkdl = res.oee.xnkdl;
          row.zzwbqh = res.oee.zzwbqh;
          row.zzwbzxx = res.oee.zzwbzxx;
          row.zzwcf = res.oee.zzwcf;
          row.zzwsbby = res.oee.zzwsbby;
          row.scxzxs = res.scxzx;
        }
      });
    },
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: [],
      change_fn_name: 'select_scx',
      width: 100,
    }, {
      coltype: 'list',
      label: '线别',
      prop: 'scxzx',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
	  optionconfig:{
		  method: 'get',
		  url: '/lbj/baseinfo/scxzx',
		  querycnf:[{scx:'scx'}]
	  },
      width: 80,
      relation: 'scxzxs',
      hideoptionval: true,
    }, {
      coltype: 'date',
      label: '日期',
      prop: 'rq',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      width: 140,
      change_fn_name: 'rq_change_handle'
    }, {
      coltype: 'string',
      label: '节拍(s)',
      prop: 'lljp',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '合格数量',
      prop: 'hgpsl',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '不合格数量',
      prop: 'bhgpsl',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '实际产量',
      prop: 'sjcl',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '班前会时长',
      prop: 'zzwbqh',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '用餐时长',
      prop: 'zzwcf',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '班中休息时长',
      prop: 'zzwbzxx',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '5s/保养',
      prop: 'zzwsbby',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '培训时长',
      prop: 'px',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '休息时长',
      prop: 'xx',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '计划停机',
      prop: 'jhtjsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '堵料停机',
      prop: 'dlsjjam',
      dbprop: 'dlsj_jam',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '待料停机',
      prop: 'dlsjwait',
      dbprop: 'dlsj_wait',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '换刀停机',
      prop: 'hdsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '换型停机',
      prop: 'hxsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '故障停机',
      prop: 'gzsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '其他停机',
      prop: 'qttjsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '非计划停机',
      prop: 'fjhtjsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '停机时间',
      prop: 'tjsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '计划作息时长',
      prop: 'jhzxsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '计划运行时长',
      prop: 'jhyxsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '实际运行时长',
      prop: 'sjyxsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '加工理论时间',
      prop: 'jgllsj',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '每小时产量',
      prop: 'jph',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '时间开动率(%)',
      prop: 'sjkdl',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      fixed: 'right',
      width: 70
    }, {
      coltype: 'string',
      label: '性能开动率(%)',
      prop: 'xnkdl',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      fixed: 'right',
      width: 70
    }, {
      coltype: 'string',
      label: '合格率(%)',
      prop: 'hgpl',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      fixed: 'right',
      width: 70
    }, {
      coltype: 'string',
      label: '实际OEE(%)',
      prop: 'oeereal',
      dbprop: 'oee_real',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      fixed: 'right',
      width: 70
    }, {
      coltype: 'string',
      label: '目标OEE',
      prop: 'oeetarget',
      dbprop: 'oee_target',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      fixed: 'right',
      width: 50
    }, {
      coltype: 'string',
      label: '设备总生产率',
      prop: 'teep',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      fixed: 'right',
      width: 70
    }
  ],
  form: {
    scx: '',
    scxzx: '',
    rq: '',
    jhzxsj: '',
    zzwbqh: '',
    zzwcf: '',
    zzwbzxx: '',
    zzwsbby: '',
    px: '',
    xx: '',
    dlsjjam: '',
    dlsjwait: '',
    hdsj: '',
    hxsj: '',
    gzsj: '',
    qttjsj: '',
    lljp: '',
    hgpsl: '',
    bhgpsl: '',
    oeetarget: '',
    sfjs: '',
    jhyxsj: '',
    sjyxsj: '',
    jhtjsj: '',
    fjhtjsj: '',
    tjsj: '',
    jgllsj: '',
    sjcl: '',
    jph: '',
    sjkdl: '',
    xnkdl: '',
    hgpl: '',
    oeereal: '',
    teep: '',
    scxzxs: [],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/scxoee/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/scxoee/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/scxoee/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
