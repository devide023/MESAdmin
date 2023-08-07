{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_template_file: function () {
      window.open('http://172.16.201.125:7002/template/lbj/点检基础信息.xlsx');
    },
    select_scx: function (collist, val, row) {
      var _this = this;
      row.scxzx = '';
      row.gwh = '';
      this.$request('get', '/lbj/baseinfo/scx_gwh?scx=' + val).then(function (res) {
        if (res.code === 1) {
          row.gwhoptions = res.list;
        }
      });
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
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/djgw/readxls', {
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
          _this.$request('get', '/lbj/djgw/readxls_by_replace', {
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
          _this.$request('get', '/lbj/djgw/readxls_by_zh', {
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
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'left',
      width: 100,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      change_fn_name: 'select_scx',
      clear_fn_name: function (_this, row) {
        row.gwh = '';
      },
      options: []
    },
	{
      coltype: 'list',
      label: '子线',
      prop: 'scxzx',
      overflowtooltip: true,
      searchable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      width: 100,
	  optionconfig:{
		  method: 'get',
		  url: '/lbj/baseinfo/scxzx',
		  querycnf:[{scx:'scx'}]
	  },
      relation: 'scxzxs',
      hideoptionval: true,
      sortable: true
    },
	{
      coltype: 'string',
      prop: 'djno',
      label: '点检编号',
      headeralign: 'center',
      align: 'left',
      width: 80,
      overflowtooltip: true,
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位名称',
      headeralign: 'center',
      align: 'center',
      width: 150,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: [],
      relation: 'gwhoptions',
    }, {
      coltype: 'string',
      prop: 'djxx',
      label: '点检内容',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'bool',
      prop: 'scbz',
      label: '删除标志',
      headeralign: 'center',
      align: 'center',
      width: 80,
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'left',
      width: 80,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      width: 130,
      overflowtooltip: true,
    }
  ],
  form: {
    gcdm: '9902',
    scx: '',
    scxzx: '',
    gwh: '',
    statusno: '',
    djno: '',
    djxx: '',
    scbz: 'N',
    lrr: '',
    lrsj: '',
    gwhoptions: [],
    scxzxs: [],
    isdb: false,
    isedit: true,
  },
  addapi: {
    url: '/lbj/djgw/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/djgw/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/djgw/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/djgw/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
