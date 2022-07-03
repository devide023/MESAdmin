{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  operate_fnlist: [],
  pagefuns: {
    add_handle: function () {
		var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
	},
  },
  batoperate:{
	  import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/tj/gygl/jrgy/readxls', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            }
			else if(result.code === 2){
				_this.$message.warning(result.msg);
			}
			else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
            _this.getlist(_this.queryform);
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
          _this.$request('get', '/tj/gygl/jrgy/readxls_by_replace', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            }
			else if(result.code === 2){
				_this.$message.warning(result.msg);
			}
			else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
            _this.getlist(_this.queryform);
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
          _this.$request('get', '/tj/gygl/jrgy/readxls_by_zh', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            }
			else if(result.code === 2){
				_this.$message.warning(result.msg);
			}
			else if (result.code === 0) {
              _this.$message.error(result.msg);
            }
            _this.getlist(_this.queryform);
          });
        } catch (error) {
          _this.$message.error(error);
        }
      } else {
        _this.$loading().close();
      }
	}
  },
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      headeralign: 'center',
      align: 'center',
	  inioptionapi: {
        method: 'get',
        url: '/tj/jcxx/scx'
      },
      options: [],
	  fixed:'left'
    }, {
      coltype: 'list',
      label: '岗位号',
      prop: 'workno',
	  dbprop:'work_no',
      headeralign: 'center',
      align: 'center',
	  inioptionapi: {
        method: 'get',
        url: '/tj/jcxx/gwzd'
      },
	  options: [],
	  width:100,
	  fixed:'left'
    },
	{
      coltype: 'string',
      label: '状态编码',
      prop: 'ztbm',
      headeralign: 'center',
      align: 'center',
	  width:100,
	  fixed:'left'
    },
	{
      coltype: 'string',
      label: '程序号',
      prop: 'jqrcs',
      headeralign: 'center',
      align: 'center',
	  width:100,
    }, {
      coltype: 'string',
      label: '最小值',
      prop: 'min',
      headeralign: 'center',
      align: 'center',
	  width:100,
    }, {
      coltype: 'string',
      label: '最大值',
      prop: 'max',
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '标准值',
      prop: 'bz',
      headeralign: 'center',
      align: 'center',
	  width:100,
    },
	{
      coltype: 'string',
      label: '箱体物料编码',
      prop: 'xtwlbm',
      headeralign: 'center',
      align: 'center',
	  width:150,
    },
	{
      coltype: 'string',
      label: '打印模板',
      prop: 'dymb',
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '油封工装',
      prop: 'gzlx2',
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '是否涂油',
      prop: 'sfty',
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '是否注油',
      prop: 'sfzy',
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'string',
      label: '是否油封压装',
      prop: 'sfyfyz',
      headeralign: 'center',
      align: 'center',
	  width:150,
    },
	{
      coltype: 'string',
      label: '是否轴承压装',
      prop: 'sfzcyz',
      headeralign: 'center',
      align: 'center',
	  width:150,
    },
	{
      coltype: 'string',
      label: '轴承工装',
      prop: 'gzlx1',
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'bool',
      label: '审核标志',
      prop: 'shbz',
      headeralign: 'center',
      align: 'center',
	  activevalue: 'Y',
      inactivevalue: 'N',
    },
	{
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      headeralign: 'center',
      align: 'center',
	  width:140,
	  overflowtooltip: true,
    }
	],
  form: {
    gcdm: '9100',
    scx: '',
    workno: '',
	workname:'',
	ztbm:'',
	jqrcs:'',	
    min: '',
    max: '',
	bz:'',
	xtwlbm:'',
	dymb:'',
	gzlx2:'',
	sfty:'',
	sfzy:'',
	sfyfyz:'',
	sfzcyz:'',
	gzlx1:'',
	shbz:'Y',
	lrr:'',
	lrsj:'',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/tj/gygl/jrgy/add',
    method: 'post',
    callback: function (_this, res) {},
  },
  editapi: {
    url: '/tj/gygl/jrgy/edit',
    method: 'post',
    callback: function (_this, res) {},
  },
  delapi: {
    url: '/tj/gygl/jrgy/del',
    method: 'post',
    callback: function (_this, res) {},
  },
  queryapi: {
    url: '/tj/gygl/jrgy/list',
    method: 'post',
    callback: function (_this, res) {},
  },
}
