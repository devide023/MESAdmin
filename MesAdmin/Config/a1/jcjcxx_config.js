{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  activated: function (_this) {
    var q = _this.$route.query;
    var keys = Object.keys(q);
    _this.queryform.search_condition = [];
    for (var i = 0; i < keys.length; i++) {
      _this.queryform.search_condition.push({
        colname: keys[i],
        coltype: 'string',
        oper: '=',
        value: q[keys[i]]
      });
    }
    for (var i = 0; i < _this.queryform.search_condition.length; i++) {
      if (i !== _this.queryform.search_condition.length - 1) {
        _this.queryform.search_condition[i].logic = 'and';
      }
    }
    _this.getlist(_this.queryform);
  },
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/jcjcxx/readxls', {
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
          _this.$request('get', '/a1/jcjcxx/readxls_by_replace', {
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
          _this.$request('get', '/a1/jcjcxx/readxls_by_zh', {
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
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    },
  ],
  pagefuns: {
    add_handle: function () {
      let _this = this;
      var row = _this.$deepClone(_this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      _this.list.unshift(row);
    },
    download_template_file() {
      window.open('http://172.16.201.216:7002/template/A1/检测基础信息.xlsx?r=' + Math.random());
    },
    select_scx: function (collist, val, row) {
      row.gwhs = [];
      row.gwh = '';
      this.$request('get', '/a1/baseinfo/gwzdbyscx', {
        scx: val
      }).then(function (res) {
        if (res.code === 1) {
          row.gwhs = res.list.map(function (i) {
            return {
              label: i.label,
              value: i.value
            };
          });
        }
      });
    },
    suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'jclx') {
        this.$request('get', '/a1/baseinfo/jclxbykey', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          }
        });
      }
    },
  },
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100,
      inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/scx'
      },
      hideoptionval: true,
      options: [],
      change_fn_name: 'select_scx',
    }, {
      coltype: 'list',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      relation: 'gwhs',
      width: 100
    }, {
      coltype: 'string',
      label: '检测类型',
      suggest: true,
      prop: 'jclx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn',
      width: 150
    }, {
      coltype: 'string',
      label: '检测类别',
      prop: 'jclb',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '序号',
      prop: 'xh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '检测要求',
      prop: 'jcyq',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
      width: 200
    },{
      coltype: 'list',
      label: '检查点',
      prop: 'jcd',
      overflowtooltip: true,
      sortable: true,
      options: [{
          label: '班前',
          value: '班前'
        }, {
          label: '班中',
          value: '班中'
        }, {
          label: '班后',
          value: '班后'
        }
      ],
      hideoptionval: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    },{
      coltype: 'string',
      label: '标准值',
      prop: 'bzz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '标准上限',
      prop: 'bzsx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '标准下限',
      prop: 'bzxx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    scx: '',
    gwh: '',
    jclx: '',
    jclb: '',
    xh: '',
    jcyq: '',
    bzz: '',
    bzsx: '',
    bzxx: '',
    lrr: '',
    lrsj: '',
    scbz: 'N',
    gwhs: [],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/jcjcxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/jcjcxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/jcjcxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/jcjcxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
