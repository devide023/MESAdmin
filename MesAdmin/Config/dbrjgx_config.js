{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  operate_fnlist: [],
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    },
    suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'cpzt') {
        row.wlmc = '';
        this.$request('get', '/lbj/baseinfo/wlbm_by_key', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          }
        });
      } else if (col.prop === 'dbh') {
        row.dblx = '';
        this.$request('get', '/lbj/baseinfo/dbxxbykey', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list.map(function (i) {
                return {
                  label: i.dblx,
                  value: i.dbh,
                  dblx: i.dblx,
                  dbmc: i.dbmc
                };
              }));
          }
        });
      } else if (col.prop === 'djlx') {
        row.jgwz = '';
        this.$request('get', '/lbj/baseinfo/rjlxbykey', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list.map(function (i) {
                return {
                  label: i.jgwz,
                  value: i.rjlx,
                  rjid: i.id,
                  rjmc: i.rjmc
                };
              }));
          }
        })
      }
    },
    select_fn: function (vm, item, row, col) {
      console.log(item);
      if (col.prop === 'cpzt') {
        row.wlmc = item.label;
      } else if (col.prop === 'dbh') {
        row.dblx = item.label;
      } else if (col.prop === 'djlx') {
        row.rjmc = item.rjmc;
        row.jgwz = item.label;
		row.rjid = item.rjid;
      }
    },
    download_template_file() {
      window.open('http://172.16.201.125:7002/template/lbj/刀柄刃具关系.xlsx');
    }
  },
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/dbrjgx/readxls', {
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
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/dbrjgx/readxls_by_zh', {
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
    import_by_zh(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/dbrjgx/readxls_by_zh', {
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
  },
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
      headeralign: 'center',
      align: 'center',
      width: 150,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx',
      },
      options: [],
    }, {
      coltype: 'string',
      label: '产品编号',
      suggest: true,
      prop: 'cpzt',
      headeralign: 'center',
      width: 300,
      align: 'center',
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn'
    }, {
      coltype: 'string',
      label: '产品名称',
      prop: 'wlmc',
      headeralign: 'center',
      width: 300,
      align: 'center',
      overflowtooltip: true,
      searchable: false,
    }, {
      coltype: 'string',
      suggest: true,
      label: '刀柄号',
      prop: 'dbh',
      headeralign: 'center',
      width: 100,
      align: 'center',
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn'
    }, {
      coltype: 'string',
      label: '刀柄类型',
      prop: 'dblx',
      headeralign: 'center',
      width: 200,
      align: 'left'
    }, {
      coltype: 'string',
      suggest: true,
      label: '刃具类型',
      prop: 'djlx',
      headeralign: 'center',
      width: 150,
      align: 'left',
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn'
    }, {
      coltype: 'string',
      label: '刃具名称',
      prop: 'rjmc',
      headeralign: 'center',
      width: 200,
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '加工位置',
      prop: 'jgwz',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }
  ],
  form: {
    gcdm: '9902',
    dbh: '',
    cpzt: '',
    djlx: '',
    rjmc: '',
    jgwz: '',
	rjid:0,
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/dbrjgx/add',
    method: 'post',
    callback: function (_this, res) {},
  },
  editapi: {
    url: '/lbj/dbrjgx/edit',
    method: 'post',
    callback: function (_this, res) {},
  },
  delapi: {
    url: '/lbj/dbrjgx/del',
    method: 'post',
    callback: function (_this, res) {},
  },
  queryapi: {
    url: '/lbj/dbrjgx/list',
    method: 'post',
    callback: function (_this, res) {},
  },
}
