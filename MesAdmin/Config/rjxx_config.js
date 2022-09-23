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
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    },
	download_template_file() {
      window.open('http://172.16.201.125:7002/template/lbj/刃具基础数据.xlsx');
    }
  },
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/rjxx/readxls', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
              _this.getlist(_this.queryform);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            }else{
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
        } else if(res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {
		if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/rjxx/readxls_by_replace', {
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
    import_by_zh(_this,res) {
		if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/rjxx/readxls_by_zh', {
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
        url: '/lbj/baseinfo/gcxx'
      },
      options: [],
    }, 
	{
      coltype: 'string',
      label: '刃具类型编号',
      width: 100,
      prop: 'id',
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '刃具类型',
      width: 200,
      prop: 'rjlx',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刃具名称',
      prop: 'rjmc',
      headeralign: 'center',
      width: 200,
      align: 'center',
    }, 
	{
      coltype: 'string',
      label: '标准寿命',
      prop: 'rjbzsm',
      width: 150,
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '加工位置',
      prop: 'jgwz',
      headeralign: 'center',
      align: 'left',
	  overflowtooltip: true,
	  width:180
    },
	{
      coltype: 'string',
      label: '备注',
      prop: 'rjxxbz',
      headeralign: 'center',
      align: 'left',
	  overflowtooltip: true,
	  width:200
    }
	],
  form: {
    gcdm: '9902',
    rjlx: '',
    rjmc: '',
    rjbzsm: '',
	jgwz:'',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/rjxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/rjxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/rjxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/rjxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
