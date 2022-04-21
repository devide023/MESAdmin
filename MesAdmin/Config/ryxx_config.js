{
  isgradequery: true,
  isoperate: false,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.adduser = this.$store.getters.name;
      row.addtime = this.$parseTime(new Date());
      this.list.unshift(row);
    },
  },
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
            for (var i = 0; i < result.list.length; i++) {
              var item = result.list[i];
              vm.$set(item, 'isdb', false);
              vm.$set(item, 'isedit', true);
              vm.list.unshift(item);
            }
          });
        } catch (error) {
          vm.$message.error(error);
        }
      } else {
        vm.$loading().close();
      }
    },
  },
  fields: [{
      coltype: 'list',
      prop: 'gcdm',
      label: '工厂',
      headeralign: 'center',
      align: 'center',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx'
      },
      options: []
    }, {
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
      options: []
    }, {
      coltype: 'string',
      prop: 'usercode',
      dbprop: 'user_code',
      label: '账号',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'password',
      label: '密码',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'username',
      dbprop: 'user_name',
      label: '姓名',
      headeralign: 'center',
      align: 'left',
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
      ]
    }, {
      coltype: 'list',
      prop: 'rylx',
      label: '员工类型',
      headeralign: 'center',
      align: 'center',
      width: 80,
      options: [{
          label: '操作员工',
          value: '操作员工'
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
      ]
    }, {
      coltype: 'list',
      prop: 'gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: []
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
          label: '晚班',
          value: '晚班'
        }
      ],
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
      action: 'http://localhost:52655/api/upload/image',
      accept: '.jpg,.jpeg,.png',
      before_upload: function (file) {
        var isJPG = file.type === "image/jpeg" || file.type === "image/jpg" || file.type === "image/png";
        var isLt2M = file.size / 1024 / 1024 < 2;
        if (!isJPG) {
          this.$message.error("上传头像图片只能是JPG,PNG格式!");
        }
        if (!isLt2M) {
          this.$message.error("上传头像图片大小不能超过 2MB!");
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
    rylx: '',
    gwh: '',
    bzxx: '白班',
    hgsg: 'Y',
    xpmc: '',
    csrq: '',
    rsrq: '',
    scbz: 'N',
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
