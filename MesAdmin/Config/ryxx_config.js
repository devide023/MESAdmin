{
  isgradequery: true,
  isoperate: false,
  isfresh: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.adduser = this.$store.getters.name;
      row.addtime = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_template_file: function () {
      window.open('http://172.16.201.125:7002/template/lbj/员工基础数据.xlsx');
    }
  },
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  isbatoperate: true,
  batoperate: {
    import_by_add: function (vm, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          vm.$request('get', '/lbj/ryxx/readxls', {
            fileid: fid
          }).then(function (result) {
            vm.$loading().close();
            if (result.code === 1) {
              vm.$message.success(result.msg);
            } else if (result.code === 2) {
              vm.$message.warning(result.msg);
            } else if (result.code === 0) {
              vm.$message.error(result.msg);
            }
            vm.getlist(vm.queryform);
          });
        } catch (error) {
          vm.$message.error(error);
        }
      } else {
        vm.$loading().close();
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
          _this.export_handle(_this.pageconfig.fields.filter(function (i) {
              return ['password'].indexOf(i.prop) === -1;
            }), expdatalist);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/ryxx/readxls_by_replace', {
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
          _this.$request('get', '/lbj/ryxx/readxls_by_zh', {
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
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      change_fn_name: function (_this, collist, val, row) {
        row.gwh = '';
        _this.$request('get', '/lbj/baseinfo/scx_gwh?scx=' + val).then(function (res) {
          if (res.code === 1) {
            row.gwhoptions = res.list;
          }
        });
      },
      clear_fn_name: function (_this, row) {
        row.gwh = '';
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'usercode',
      dbprop: 'user_code',
      label: '账号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'string',
      prop: 'username',
      width: 80,
      dbprop: 'user_name',
      label: '姓名',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'password',
      label: '密码',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'list',
      prop: 'ryxb',
      label: '性别',
      headeralign: 'center',
      align: 'center',
      width: 80,
      options: [{
          label: '男',
          value: '男'
        }, {
          label: '女',
          value: '女'
        }
      ],
	  hideoptionval:true
    }, {
      coltype: 'string',
      prop: 'sfz',
      label: '身份证号',
      headeralign: 'center',
      align: 'center',
      width: 150,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'tel',
      label: '手机号码',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'list',
      prop: 'rylx',
      label: '员工类型',
      headeralign: 'center',
      align: 'center',
      width: 80,
      options: [{
          label: '操作工',
          value: '操作工'
        }, {
          label: '巡检',
          value: '巡检'
        }, {
          label: '大班长',
          value: '大班长'
        }, {
          label: '组长',
          value: '组长'
        }
      ],
	  hideoptionval:true
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,
      optionconfig:{
		  method: 'get',
		  url: '/lbj/baseinfo/gwzdbyscx',
		  querycnf:[{scx:'scx'}]
	  },
      options: [],
      relation: 'gwhoptions',
    }, {
      coltype: 'list',
      prop: 'bzxx',
      label: '班组',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '白班',
          value: '白班'
        }, {
          label: '夜班',
          value: '夜班'
        }
      ],
	  hideoptionval:true,
      width: 80
    }, {
      coltype: 'bool',
      prop: 'hgsg',
      label: '合格上岗',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
      width: 80
    }, {
      coltype: 'image',
      prop: 'xpmc',
      label: '照片',
      headeralign: 'center',
      align: 'center',
      action: 'http://172.16.201.125:7002/api/upload/image',
      accept: '.jpg,.jpeg,.png',
      before_upload: function (file) {
        var isJPG = file.type === "image/jpeg" || file.type === "image/jpg" || file.type === "image/png";
        var isLt2M = file.size / 1024 / 1024 < 5;
        if (!isJPG) {
          this.$message.error("上传头像图片只能是JPG,PNG格式!");
        }
        if (!isLt2M) {
          this.$message.error("上传头像图片大小不能超过 5MB!");
        }
        return isJPG && isLt2M;
      },
      upload_success: function (res, file) {
        if (res.code === 1) {
          if (res.files.length > 0) {
            var rowkey = res.extdata.rowkey;
            var findrow = this.$basepage.list.find((t) => t.rowkey === rowkey);
            if (findrow) {
              findrow.xpmc = res.files[0].fileid;
            }
          }
        } else if (res.code === 0) {
          this.$message.error(res.msg);
        }
      }
    }, {
      coltype: 'date',
      prop: 'csrq',
      label: '出生日期',
      headeralign: 'center',
      with : 200,
      align: 'center',
    }, {
      coltype: 'date',
      prop: 'rsrq',
      label: '入司日期',
      headeralign: 'center',
      with : 200,
      align: 'center',
    }, {
      coltype: 'bool',
      prop: 'scbz',
      label: '是否离职',
      headeralign: 'center',
      align: 'center',
      activevalue: 'Y',
      inactivevalue: 'N',
      width: 80
    }
  ],
  form: {
    gcdm: '9902',
    scx: '',
    usercode: '',
    username: '',
    password: '123456',
    ryxb: '男',
    rylx: '操作工',
    tel: '',
    gwh: '',
    bzxx: '白班',
    hgsg: 'Y',
    xpmc: '',
    csrq: '',
    rsrq: '',
    scbz: 'N',
    gwhoptions: [],
    isdb: false,
    isedit: true,

  },
  addapi: {
    url: '/lbj/ryxx/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/ryxx/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/ryxx/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/ryxx/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
