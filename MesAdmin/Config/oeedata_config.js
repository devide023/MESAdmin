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
          _this.$request('get', '/lbj/oee/readxls', {
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
          _this.$request('get', '/lbj/oee/readxls_by_replace', {
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
          _this.$request('get', '/lbj/oee/readxls_by_zh', {
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

    }, {
      coltype: 'string',
      label: '计划作息时长',
      prop: 'jhzxsj',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center'
    }, {
      coltype: 'string',
      label: '班前会时长',
      prop: 'zzwbqh',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '用餐时长',
      prop: 'zzwcf',
      overflowtooltip: true,

      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '班中休息时长',
      prop: 'zzwbzxx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '5S/保养',
      prop: 'zzwsbby',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '培训时长',
      prop: 'px',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '休息时长',
      prop: 'xx',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '堵料停机',
      prop: 'dlsjjam',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '待料停机',
      prop: 'dlsjwait',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '换刀停机',
      prop: 'hdsj',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '换型停机',
      prop: 'hxsj',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '故障停机',
      prop: 'gzsj',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '其它停机',
      prop: 'qttjsj',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '理论节拍(s)',
      prop: 'lljp',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '目标OEE',
      prop: 'oeetarget',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }
  ],
  form: {
    scx: '',
    jhzxsj: 1440,
    zzwbqh: 15,
    zzwcf: 30,
    zzwbzxx: 60,
    zzwsbby: 15,
    px: 0,
    xx: 0,
    dlsjjam: 0,
    dlsjwait: 0,
    hdsj: 0,
    hxsj: 0,
    gzsj: 0,
    qttjsj: 0,
    lljp: 5,
    oeetarget: 99,
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/oee/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/oee/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/oee/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/oee/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
