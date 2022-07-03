{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: true,
  operate_fnlist: [{
      label: '上传视频',
      btntype: 'uploadvideo',
      action: 'http://172.16.201.125:7002/api/upload/video',
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
            finditem.gybh = '';
            finditem.wjlj = response.files[0].fileid;
            finditem.scry = this.$store.getters.name;
            finditem.scsj = this.$parseTime(new Date());
          }
        } else {
          this.$message.error(response.msg);
        }
      }
    }, {
      label: '下载视频',
      fnname: 'download_dzgyMp4',
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
    download_dzgyMp4: function (row) {
      var _this = this;
      this.$request('get', "download/ftp2web", {
        wjlx: '视频',
        wjlj: row.wjlj
      }).then(function (res) {
        if (res.code === 1) {
          window.open("http://172.16.201.125:7002/api/download/downloadpdf?wjlj=" + row.wjlj);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    }
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
      align: 'left',
      width: 180,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'gybh',
      label: '编号',
      headeralign: 'center',
      align: 'center',
      width: 150,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'statusno',
      label: '产品',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
      prop: 'gymc',
      label: '名称',
      headeralign: 'center',
      align: 'left',
      width: 280,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'gyms',
      label: '描述',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'jwdx',
      label: '文件大小',
      headeralign: 'center',
      align: 'left',
      width: 80,
    }, {
      coltype: 'list',
      prop: 'wjfl',
      label: '文件分类',
      headeralign: 'center',
      align: 'left',
      width: 80,
      options: [{
          label: '视频',
          value: '视频'
        }
      ]
    }, {
      coltype: 'datetime',
      prop: 'scsj',
      label: '上传日期',
      headeralign: 'center',
      align: 'center',
      width: 140
    }
  ],
  form: {
    gcdm: '9902',
    scx: '',
    statusno: '',
    gymc: '',
    gyms: '',
    jwdx: '',
    wjfl: '视频',
    scsj: '',
    wjlj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/dzgysp/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/dzgysp/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/dzgysp/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/dzgysp/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
