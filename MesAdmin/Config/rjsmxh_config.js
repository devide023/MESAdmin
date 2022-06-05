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
  operate_fnlist: [],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    },
    download_template_file() {
      window.open('http://172.16.201.125:7002/template/lbj/刃具寿命消耗.xlsx');
    }
  },
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/rjsmxh/readxls', {
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
          this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/rjsmxh/readxls_by_replace', {
            fileid: fid
          }).then(function (result) {
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
    import_by_zh(_this, res) {
		if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/rjsmxh/readxls_by_zh', {
            fileid: fid
          }).then(function (result) {
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
  },
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
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
      label: '刃具类型',
      prop: 'rjlx',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '产品状态',
      prop: 'cpzt',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '设备编号',
      prop: 'sbbh',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'int',
      label: '每件消耗寿命',
      prop: 'mjxhsm',
      headeralign: 'center',
      align: 'center',
    }, ],
  form: {
    gcdm: '9902',
    scx: '',
    rjlx: '',
    cpzt: '',
    sbbh: '',
    mjxhsm: '1',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/rjsmxh/add',
    method: 'post',
    callback: function (_this, res) {},
  },
  editapi: {
    url: '/lbj/rjsmxh/edit',
    method: 'post',
    callback: function (_this, res) {},
  },
  delapi: {
    url: '/lbj/rjsmxh/del',
    method: 'post',
    callback: function (_this, res) {},
  },
  queryapi: {
    url: '/lbj/rjsmxh/list',
    method: 'post',
    callback: function (_this, res) {},
  },
}
