{
  isgradequery: true,
  isbatoperate: true,
  isoperate: true,
  isfresh: true,
  isselect: true,
  operate_fnlist: [{
      label: '上传',
      btntype: 'uploadvideo',
      action: 'http://192.168.1.111:7002/api/ducar/upload/gysp',
      callback: function (response, file) {
        if (response.code === 1) {
          this.$loading().close();
          this.$message.success(response.msg);
          let rowid = response.extdata.rowkey;
          let finditem = this.$basepage.list.find(function (i) {
            return i.rowkey === rowid;
          });
          if (finditem) {
            finditem.jwdx = file.size;
            finditem.gymc = file.name;
            finditem.wjlj = response.files[0].fileid;
            finditem.scry = this.$store.getters.name;
            finditem.scsj = this.$parseTime(new Date());
          }
        } else {
          this.$message.error(response.msg);
        }
      }
    }, {
      label: '下载',
      fnname: 'download_dzgyMp4',
      btntype: 'text'
    },
	{
		label: '查看',
      fnname: 'view_gysp',
      btntype: 'text'
	}
  ],
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/ducar/gysp/readxls', {
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
          _this.$request('get', '/ducar/gysp/readxls_by_replace', {
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
          _this.$request('get', '/ducar/gysp/readxls_by_zh', {
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
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_template_file: function () {
      window.open('http://192.168.1.111:7002/template/Ducar/工艺视频.xlsx?r=' + Math.random());
    },
	view_gysp:function(row){
		window.open("http://192.168.1.114:8100/储能/视频/"+row.gymc);
	},
    download_dzgypdf: function (row) {
      var _this = this;
      this.$request('get', "download/ftp2web", {
        wjlx: '视频',
        wjlj: row.gymc
      }).then(function (res) {
        if (res.code === 1) {
          window.open("http://192.168.1.111:7002/api/download/downloadpdf?wjlj=" +row.gymc);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
    download_dzgyMp4: function (row) {
      var _this = this;
      this.$request('get', "download/ftp2web", {
        wjlx: '视频',
        wjlj: row.gymc
      }).then(function (res) {
        if (res.code === 1) {
          window.open("http://192.168.1.111:7002/api/download/downloadpdf?wjlj="+row.gymc);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
	select_scx: function (collist, val, row) {
        row.gwhs=[];
		row.gwh='';
		row.gwmc = '';
        this.$request('get', '/ducar/baseinfo/gwzdbyscx', {
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
	select_gwh: function (collist, val, row){
		var pos = row.gwhs.findIndex(t => t.value === val);
      if (pos !== -1) {
        row.gwmc = row.gwhs[pos].label;
      } else {
        row.gwmc = '';
      }
	},
    suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'jxno') {
        row.username = '';
        this.$request('get', '/ducar/baseinfo/jxno_by_code', {
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
        row.statusno = '';
        this.$request('get', '/ducar/baseinfo/ztbm_by_jxno', {
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
        url: '/ducar/baseinfo/scx'
      },
	  hideoptionval:true,
	  options:[],
	  change_fn_name: 'select_scx',
  },{
      coltype: 'string',
      label: '工艺编号',
      prop: 'gybh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'list',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150,
	  options: [],
	  optionconfig:{
		  method: 'get',
		  url: '/ducar/baseinfo/gwzdbyscx',
		  querycnf:[{scx:'scx'}]
	  },
	  relation: 'gwhs',
	  change_fn_name: 'select_gwh',
    },{
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
      label: '机型',
      prop: 'jxno',
      dbprop: 'jx_no',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      label: '状态码',
      prop: 'statusno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150,
      hideoptionval: true
    }, {
      coltype: 'string',
      label: '文件名称',
      prop: 'gymc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 300
    }, {
      coltype: 'string',
      label: '工艺描述',
      prop: 'gyms',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, 
	{
      coltype: 'string',
      label: '文件路径',
      prop: 'wjlj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, 
	{
      coltype: 'string',
      label: '备注',
      prop: 'bz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'scry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'scsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    scx: '',
    gybh: '',
    gymc: '',
    gyms: '',
    gwh: '',
    jxno: '',
    statusno: '',
    wjlj: '',
    jwdx: '',
    scry: '',
    scpc: '',
    scsj: '',
    bz: '',
    statusno_list: [],
	gwhs:[],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/ducar/gysp/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/ducar/gysp/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/ducar/gysp/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/gysp/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
