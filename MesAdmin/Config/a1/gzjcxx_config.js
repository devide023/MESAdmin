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
          _this.$request('get', '/a1/fault/readxls', {
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
          _this.$request('get', '/a1/fault/readxls_by_replace', {
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
          _this.$request('get', '/a1/fault/readxls_by_zh', {
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
      window.open('http://172.16.201.216:7002/template/A1/故障基础信息.xlsx?r=' + Math.random());
    },
	select_scx: function (collist, val, row) {
        row.gwhs=[];
		row.gwh='';
		row.gwmc = '';
		row.gwhbz='';
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
	  change_fn_name: 'select_scx',
	  options:[]
  },{
      coltype: 'string',
      label: '故障代码',
      prop: 'faultno',
      dbprop: 'fault_no',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
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
      width: 150
    }, {
      coltype: 'string',
      label: '故障名称',
      prop: 'faultname',
      dbprop: 'fault_name',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      label: '故障分类',
      prop: 'faultfl',
      dbprop: 'fault_fl',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '通用故障',
          value: '通用故障'
        }, {
          label: '系统故障',
          value: '系统故障'
        }, {
          label: '装配问题',
          value: '装配问题'
        }, {
          label: '设备故障',
          value: '设备故障'
        }, {
          label: '零部件问题',
          value: '零部件问题'
        }, {
          label: '测试故障',
          value: '测试故障'
        },
      ],
      width: 150,
      allowcreate: true,
      hideoptionval: true,
    }, {
      coltype: 'string',
      suggest: true,
      label: '机型',
      prop: 'jxno',
      dbprop: 'jx_no',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      suggest_fn_name: 'suggest_fn',
      select_fn_name: 'select_fn',
      width: 150
    }, {
      coltype: 'list',
      label: '状态码',
      prop: 'statusno',
      dbprop: 'status_no',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      hideoptionval: true,
      relation: 'statusno_list',
      width: 150
    }, {
      coltype: 'list',
      label: '默认处理岗位',
      prop: 'gwhbz',
      dbprop: 'gwh_bz',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [],
      width: 150,
	  relation: 'gwhs',
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
      width: 100
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      overflowtooltip: true,
      searchable: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
	  scx:'',
    gwh: '',
    faultno: '',
    faultname: '',
    faultfl: '',
    jxno: '',
    statusno: '',
    gwhbz: '',
    bz: '',
    lrr: '',
    lrsj: '',
    statusno_list: [],
	gwhs:[],
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/fault/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/fault/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/fault/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/fault/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
