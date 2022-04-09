{
  isgradequery: true,
  isoperate: true,
  operate_fnlist: [{
      label: '上传Pdf',
      btntype: 'upload',
      action: 'http://localhost:52655/api/upload/pdf',
      callback: function (response, file) {
        if (response.code === 1) {
          this.$message.success(response.msg);
          let rowid = response.extdata.rowkey;
          let finditem = this.$basepage.list.find(function (i) {
            return i.rowkey === rowid;
          });
          if (finditem) {
            finditem.jwdx = file.size;
            finditem.jcmc = file.name;
            finditem.wjlj = file.name;
            finditem.scry = this.$store.getters.name;
            finditem.scsj = this.$parseTime(new Date());
          }
        } else {
          this.$message.error(response.msg);
        }
      }
    }, {
      label: '下载Pdf',
      fnname: 'download_dzgypdf',
      btntype: 'text'
    }
  ],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.adduser = this.$store.getters.name;
      row.addtime = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_dzgypdf: function (row) {
      console.log(row);
      if (row.wjlj) {
        window.open("http://localhost:52655/upload/downloadpdf?filename=" + row.wjlj);
      }
    }
  },
  isbatoperate: true,
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/jtgl/readxls', {
            fileid: fid
          }).then(function (result) {
            _this.$loading().close();
            if (result.code === 1) {
              _this.$message.success(result.msg);
              _this.getlist(_this.queryform);
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
    import_by_replace: function (_this, res) {},
    import_by_zh: function (_this, res) {},
    export_excel: function (_this) {}
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
      prop: 'jcbh',
      label: '技通编号',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'wjfl',
      label: '分类',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'jcmc',
      label: '技通名称',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'jcms',
      label: '技通描述',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'date',
      prop: 'yxqx1',
      label: '有效日期开始',
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'date',
      prop: 'yxqx2',
      label: '有效日期结束',
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      prop: 'jwdx',
      label: '文件大小',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'scry',
      label: '上传者',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'scsj',
      label: '上传时间',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'bool',
      prop: 'fpflg',
      dbprop: 'fp_flg',
      label: '分配标识',
      headeralign: 'center',
      align: 'center',
      width: 80,
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      prop: 'fpr',
      label: '分配人',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'fpsj',
      dbprop: 'fp_sj',
      label: '分配日期',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }
  ],
  form: {
    gcdm: '9902',
    scx: '',
    jtid: '',
    wjfl: '技通',
    jcbh: '',
    jcmc: '',
    jcms: '',
    wjlj: '',
    jwdx: '',
    scry: '',
    scpc: '',
    scsj: '',
    yxqx1: '',
    yxqx2: '',
    fpflg: 'N',
    fpsj: '',
    fpr: '',
    isedit: true,
    isdb: false,
  },
  addapi: {
    url: '/lbj/jtgl/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/jtgl/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/jtgl/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/jtgl/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
