{
  isgradequery: true,
  isbatoperate: true,
  isoperate: true,
  isfresh: true,
  isselect: true,
  operate_fnlist: [{
      label: '上传Pdf',
      btntype: 'upload',
      action: 'http://172.16.201.216:7002/api/a1/upload/jstc_pdf',
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
            finditem.jcmc = file.name;
            finditem.wjlj = file.name;
            finditem.scry = _this.$store.getters.name;
            finditem.scsj = _this.$parseTime(new Date());
          }
        } else {
          this.$message.error(response.msg);
        }
      }
    }, {
      label: '查看Pdf',
      fnname: 'download_jstcpdf',
      btntype: 'text'
    }, {
      label: '分配明细',
      fnname: 'jtfp_detail',
      btntype: 'text',
    },
  ],
  batoperate: {
	  import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/ducar/jtgl/readxls', {
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
          _this.$request('get', '/ducar/jtgl/readxls_by_replace', {
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
          _this.$request('get', '/ducar/jtgl/readxls_by_zh', {
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
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.scry = this.$store.getters.name;
      row.scsj = this.$parseTime(new Date());
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
    download_jstcpdf: function (row) {
      var _this = this;
      this.$request('get', '/a1/jtfpscx/read_pdm_jstz', {
        jcbh: row.jcbh
      }).then(function (res) {});
      if (row.jtly === 1) {
        window.open("http://jsgltj.zsdl.cn/tjjstz/file/" + row.wjlj);
      } else if (row.jtly === 0) {
        window.open("http://172.16.201.216:81/技术通知/" + row.wjlj);
      }
    },
    jtfp_detail: function (row, item) {
      var _this = this;
      _this.dialog_title = row.jcbh + '分配明细';
      _this.dialog_width = '60%';
      _this.dialogVisible = true;
      _this.dialog_hidefooter = true;
      _this.dialog_viewpath = 'ducar/jstz/fpmx';
      _this.dialog_fnitem = item;
      _this.dialog_props = {
        row: row
      };
    },
    dialog_save_handle: function (vm) {
      console.log(vm)
    }
  },
  fields: [
  {
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
  },{
      coltype: 'string',
      label: '技通编号',
      prop: 'jcbh',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'list',
      label: '文件分类',
      prop: 'wjfl',
      searchable: false,
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '技通',
          value: '技术通知'
        }, {
          label: '变更',
          value: '变更通知'
        }, {
          label: '质量',
          value: '质量通知'
        }, ],
      hideoptionval: true,
      width: 100
    }, {
      coltype: 'string',
      label: '技通名称',
      prop: 'jcmc',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left'
    }, {
      coltype: 'string',
      label: '文件名称',
      prop: 'wjlj',
      overflowtooltip: true,
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'date',
      label: '有效期限开始',
      prop: 'yxqx1',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'date',
      label: '有效期限结束',
      prop: 'yxqx2',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    },
	{
      coltype: 'bool',
      label: '是否分配',
      prop: 'fpflg',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
	  activevalue:'Y',
	  inactivevalue:'N',
      width: 100
    },
	{
      coltype: 'string',
      label: '技通描述',
      prop: 'jcms',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    },
	{
      coltype: 'string',
      label: '创建者',
      prop: 'scry',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '创建时间',
      prop: 'scsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
	  scx:'',
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
    wjfl: '技术通知',
    lrr: '',
    lrsj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/ducar/jtgl/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/ducar/jtgl/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/ducar/jtgl/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/jtgl/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
