{
  isgradequery: true,
  isbatoperate: true,
  isoperate: true,
  isfresh: true,
  isselect: true,
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/jclx/readxls', {
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
          _this.$request('get', '/a1/jclx/readxls_by_replace', {
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
          _this.$request('get', '/a1/jclx/readxls_by_zh', {
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
  operate_fnlist:[
  {
		label: '检测项目',
      fnname: 'view_jcxm',
      btntype: 'text'
	}
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
      window.open('http://172.16.201.216:7002/template/A1/检测类型.xlsx?r=' + Math.random());
    },
	view_jcxm(row){
		var url = '/check/jcxx?jclx='+row.jclx;
		this.$router.push({
			path:url
		});
	}
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
      options: []
    }, {
      coltype: 'string',
      label: '检测类型',
      prop: 'jclx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    },
{
      coltype: 'bool',
      label: '是否首末件',
      prop: 'issm',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  activevalue: 'Y',
      inactivevalue: 'N',
	  width:120
    },
	{
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
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
    jclx: '',
    lrr: '',
    lrsj: '',
    scbz: 'N',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/jclx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/jclx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/jclx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/jclx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
