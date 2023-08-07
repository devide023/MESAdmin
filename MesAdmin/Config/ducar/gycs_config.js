{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/ducar/gycs/readxls', {
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
          _this.$request('get', '/ducar/gycs/readxls_by_replace', {
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
          _this.$request('get', '/ducar/gycs/readxls_by_zh', {
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
      window.open('http://192.168.1.111:7002/template/Ducar/工艺参数.xlsx');
    },
    gwh_change_handle: function (collist, val, row) {
      row.sbbh_list = [];
      row.sbbh = '';
      this.$request('get', '/a1/baseinfo/sbxx_by_gwh', {
        gwh: val
      }).then(function (res) {
        row.sbbh_list = res.list;
      });
    },
    gwh_clear_handle(row, col) {
      row.sbbh = '';
    },
    select_scx: function (collist, val, row) {
      row.gwhs = [];
      row.gwh = '';
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
    select_gwh: function (collist, val, row) {
      row.sbbh_list = [];
      row.sbbh = '';
      var pos = row.gwhs.findIndex(t => t.value === val);
      if (pos !== -1) {
        row.gwmc = row.gwhs[pos].label;
      } else {
        row.gwmc = '';
      }
      this.$request('post', '/ducar/baseinfo/sbbhbygwh', {
        scx: row.scx,
        gwh: val
      }).then(function (res) {
        if (res.code === 1) {
          row.sbbh_list = res.list.map(function (i) {
            return {
              label: i.label,
              value: i.value
            };
          });
        }
      });
    },
    select_sbbh: function (collist, val, row) {
      var pos = row.sbbh_list.findIndex(t => t.value === val);
      if (pos !== -1) {
        row.sbmc = row.sbbh_list[pos].label;
      } else {
        row.sbmc = '';
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
      hideoptionval: true,
      options: [],
      change_fn_name: 'select_scx',
    }, {
      coltype: 'list',
      label: '岗位编码',
      prop: 'gwh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      relation: 'gwhs',
	  optionconfig:{
		  method: 'get',
		  url: '/ducar/baseinfo/gwzdbyscx',
		  querycnf:[{scx:'scx'}]
	  },
      change_fn_name: 'select_gwh',
    }, {
      coltype: 'string',
      label: '岗位名称',
      prop: 'gwmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '机型',
      prop: 'jxno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '状态码',
      prop: 'statusno',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '设备编号',
      prop: 'sbbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      relation: 'sbbh_list',
      change_fn_name: 'select_sbbh',
    }, {
      coltype: 'string',
      label: '设备名称',
      prop: 'sbmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      searchable: false,
      align: 'center',
    }, {
      coltype: 'string',
      label: '设备类型',
      prop: 'sblx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      searchable: false,
      align: 'center',
    }, {
      coltype: 'string',
      label: '程序号',
      prop: 'sbcxh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'bool',
      label: '是否下发程序号',
      prop: 'iscxh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'bool',
      label: '是否核心数据',
      prop: 'ishxsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      label: '最小值',
      prop: 'gymin',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '最大值',
      prop: 'gymax',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '标准值',
      prop: 'gybz',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, 
	{
      coltype: 'string',
      label: '螺钉数',
      prop: 'parm1',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    },	
	{
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
    gcdm: '101',
    scx: '',
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
    statusno_list: [],
    sbbh_list: [],
    gwhs: [],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/ducar/gycs/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/ducar/gycs/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/ducar/gycs/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/gycs/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
