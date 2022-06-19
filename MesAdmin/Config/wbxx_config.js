{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    },
    download_template_file: function () {
      window.open('http://172.16.201.125:7002/template/lbj/维保基础信息.xlsx');
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
          _this.$request('get', '/lbj/wbxx/readxls', {
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
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/wbxx/readxls_by_replace', {
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
    import_by_zh(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/wbxx/readxls_by_zh', {
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
  },
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
      headeralign: 'center',
      align: 'center',
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
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902',
      },
	  change_fn_name: function (_this, collist, val, row) {
        row.gwh = '';
        _this.$request('get', '/lbj/baseinfo/scx_gwh?scx=' + val).then(function (res) {
          if (res.code === 1) {
            row.gwhoptions = res.list;
          }
        });
      },
	  clear_fn_name:function(_this,row){
		  row.gwh = '';
	  },
      options: [],
    }, {
      coltype: 'string',
      label: '生产线编号',
      prop: 'scx',
      headeralign: 'center',
      align: 'center',
      width: 100,
      searchable: false,
    }, {
      coltype: 'list',
      label: '岗位',
      prop: 'gwh',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd',
      },
      options: [],
	  relation:'gwhoptions',
    }, {
      coltype: 'string',
      label: '岗位编号',
      prop: 'gwh',
      headeralign: 'center',
      align: 'center',
      width: 80,
      searchable: false,
    }, {
      coltype: 'int',
      label: '顺序',
      prop: 'wbsh',
      headeralign: 'center',
      align: 'center',
      width: 80,
      searchable: false,
    }, {
      coltype: 'string',
      label: '维保内容',
      prop: 'wbxx',
      headeralign: 'center',
      align: 'left',

    }, {
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'bool',
      label: '删除标志',
      prop: 'scbz',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, ],
  form: {
	  gcdm:'9902',
	  scx:'',
	  gwh:'',
	  wbsh:'',
	  wbxx:'',
	  bz:'',
	  lrr:'',
	  lrsj:'',
	  scbz:'N',
	  gwhoptions:[],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/wbxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/wbxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/wbxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/wbxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
