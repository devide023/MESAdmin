{
  isgradequery: true,
  isbatoperate: true,
  isoperate: true,
  isfresh: true,
  isselect: true,
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  operate_fnlist: [{
      label: '下载',
      fnname: 'download_dzgypdf',
      btntype: 'text'
    },
	{
      label: '查看',
      fnname: 'view_zpzdpdf',
      btntype: 'text'
    }
  ],
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/gyzpzd/readxls', {
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
          _this.$request('get', '/a1/gyzpzd/readxls_by_replace', {
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
          _this.$request('get', '/a1/gyzpzd/readxls_by_zh', {
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
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
	view_zpzdpdf:function(row){
		window.open("http://172.16.201.216:81/电子工艺/" + row.wjlj+'/'+row.gymc);
	},
    download_template_file: function () {
      window.open('http://172.16.201.216:7002/template/A1/装配指导.xlsx?r=' + Math.random());
    },
    download_dzgypdf: function (row) {
      var _this = this;
      this.$request('get', "download/ftp2web", {
        wjlx: '电子工艺',
        wjlj: row.wjlj + "/" + row.gymc
      }).then(function (res) {
        if (res.code === 1) {
          window.open("http://172.16.201.216:7002/api/download/downloadpdf?wjlj=" + row.wjlj + '/' + row.gymc);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
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
        url: '/a1/baseinfo/scx'
      },
	  hideoptionval:true,
	  options:[]
  },{
      coltype: 'string',
      label: '文件编号',
      prop: 'gybh',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '分类',
      prop: 'gylx',
      overflowtooltip: true,
      searchable: false,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '文件名称',
      prop: 'gymc',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '描述',
      prop: 'gyms',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '文件路径',
      prop: 'wjlj',
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
	  scx:'',
    gybh: '',
    gymc: '',
    gyms: '',
    gwh: '',
    wjlj: '',
    scsj: '',
    gylx: '装配指导',
    lrr: '',
    lrsj: '',
    bz: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/gyzpzd/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/gyzpzd/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/gyzpzd/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/gyzpzd/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
