{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: false,
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/gycs/readxls', {
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
    import_by_replace(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/a1/gycs/readxls_by_replace', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
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
          _this.$request('get', '/a1/gycs/readxls_by_zh', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
            } else if (result.code === 2) {
              _this.$message.warning(result.msg);
            } else if (result.code === 0) {
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
  bat_btnlist: [
  ],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_template_file: function () {
      window.open('http://172.16.201.216:7002/template/A1/工艺参数.xlsx');
    },
	gwh_change_handle:function(collist,val, row){
		row.sbbh_list=[];
		row.sbbh='';
		this.$request('get','/a1/baseinfo/sbxx_by_gwh',{gwh:val}).then(function(res){
			row.sbbh_list = res.list;
		});
	},
	gwh_clear_handle(row,col){
		row.sbbh='';
	},
    suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'jxno') {
        row.username = '';
        this.$request('get', '/a1/baseinfo/jxno_by_code', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          }
        });
      }
    },
    select_fn: function (vm, item, row, col) {
      if (col.prop === 'jxno') {
        this.$request('get', '/a1/baseinfo/ztbm_by_jxno', {
          jxno: item.value
        }).then(function (res) {
          if (res.code === 1) {
            row.statusno_list = res.list.map(function (i) {
              return {
                label: i,
                value: i
              };
            });
          }
        });
      }
    }
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
      coltype: 'list',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
	  change_fn_name:'gwh_change_handle',
	  clear_fn_name:'gwh_clear_handle',
    }, {
      coltype: 'string',
      suggest: true,
      label: '机型',
      prop: 'jxno',
      dbprop: 'jx_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn'
    }, {
      coltype: 'list',
      label: '状态码',
      prop: 'statusno',
      dbprop: 'status_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      hideoptionval: true,
      relation: 'statusno_list'
    }, {
      coltype: 'list',
      label: '设备编号',
      prop: 'sbbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  options:[],
	  relation:'sbbh_list'
    }, {
      coltype: 'string',
      label: '程序号',
      prop: 'sbcxh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '最小值',
      prop: 'gymin',
      dbprop: 'gy_min',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '最大值',
      prop: 'gymax',
      dbprop: 'gy_max',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '标准值',
      prop: 'gybz',
      dbprop: 'gy_bz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '螺钉数',
      prop: 'parm1',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }
  ],
  form: {
    gcdm: '9100',
    scx: '1',
    gwh: '',
    jxno: '',
    statusno: '',
    sbbh: '',
    sbcxh: '',
    gymin: '',
    gymax: '',
    gybz: '',
    shbz: 'Y',
    lrr: '',
    lrsj: '',
    parm1: '1',
    statusno_list: [],
	sbbh_list:[],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/gycs/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/gycs/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/gycs/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/gycs/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
