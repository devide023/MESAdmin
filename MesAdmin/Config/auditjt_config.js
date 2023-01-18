{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: true,
  operate_fnlist: [{
      label: '查看',
      fnname: 'view_jstz_handle',
      btntype: 'text'
    }, {
      label: '下载',
      fnname: 'download_jstcpdf',
      btntype: 'text'
    }, {
      label: '分配',
      fnname: 'jstz_fp_handle',
      btntype: 'text'
    }

  ],
  pagefuns: {
    jstz_fp_handle: function (row) {
      var _this = this;
      if (row.fpflg === 'N') {
        _this.dialog_title = row.jcbh + '分配';
        _this.jtfpform.jstc = row;
        _this.dialogvisible = true;
      } else {
        _this.$message.error('该电子文档已分配');
      }
    },
    jstz_qxfp_handle() {
      var _this = this;
      if (_this.selectlist.length > 0) {
        _this.$confirm('你确定要取消分配?', '提示', {
          type: "warning",
          cancelButtonClass: "el-button--primary",
          confirmButtonClass: "el-button--danger",
        }).then(function () {
          _this.$request('post', '/lbj/jtfp/qxfp', _this.selectlist).then(function (res) {
            if (res.code === 1) {
              _this.$message.success(res.msg);
              _this.getlist(_this.queryform);
            } else if (res.code === 0) {
              _this.$message.error(res.msg);
            }
          });
        });
      } else {
        _this.$message.warning('请选审核项目');
      }
    },
    audit_handle: function () {
      var _this = this;
      if (_this.selectlist.length > 0) {
        this.$request('post', '/lbj/jtgl/audit', _this.selectlist).then(function (res) {
          if (res.code === 1) {
            _this.$message.success(res.msg);
            _this.getlist(_this.queryform);
          } else if (res.code === 0) {
            _this.$message.error(res.msg);
          }
        });
      } else {
        _this.$message.warning('请选审核项目');
      }
    },
    download_jstcpdf: function (row) {
      var _this = this;
      this.$request('get', "download/ftp2web", {
        wjlx: '技术通知',
        wjlj: row.wjlj
      }).then(function (res) {
        if (res.code === 1) {
          window.open("http://172.16.201.125:7002/api/download/downloadpdf?wjlj=" + row.wjlj);
        } else if (res.code === 0) {
          _this.$message.error(res.msg);
        }
      });
    },
    view_jstz_handle: function (row) {
      window.open("http://172.16.201.125:81/技术通知/" + row.wjlj);
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
      width: 150,
      sortable: true,
      overflowtooltip: true,
    }, {
      coltype: 'list',
      prop: 'wjfl',
      label: '分类',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '技通',
          value: '技通'
        }, {
          label: '技改',
          value: '技改'
        }, {
          label: '清单',
          value: '清单'
        }, {
          label: '通知单',
          value: '通知单'
        }, {
          label: '作业文件',
          value: '作业文件'
        }, {
          label: '质量警示',
          value: '质量警示'
        }, {
          label: '内控标准',
          value: '内控标准'
        }, {
          label: '发放明细',
          value: '发放明细'
        }
      ]
    }, {
      coltype: 'string',
      prop: 'jcmc',
      label: '技通名称',
      headeralign: 'center',
      align: 'center',
      width: 150,
      sortable: true,
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
      width: 150,
      sortable: true,
    }, {
      coltype: 'date',
      prop: 'yxqx2',
      label: '有效日期结束',
      headeralign: 'center',
      align: 'center',
      width: 150,
      sortable: true,
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
      sortable: true,
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'bool',
      prop: 'fpflg',
      dbprop: 'fp_flg',
      label: '分配标识',
      headeralign: 'center',
      align: 'center',
      width: 100,
      activevalue: 'Y',
      inactivevalue: 'N',
      sortable: true,
    }, {
      coltype: 'string',
      prop: 'fpmx',
      label: '分配明细',
      headeralign: 'center',
      align: 'center',
      width: 80,
      overflowtooltip: true,
      searchable: false,
    }, {
      coltype: 'bool',
      prop: 'shbz',
      label: '审核标志',
      headeralign: 'center',
      align: 'center',
      width: 110,
      activevalue: 'Y',
      inactivevalue: 'N',
      sortable: true,
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
      width: 150,
      sortable: true,
    }, {
      coltype: 'string',
      prop: 'shr',
      label: '审核人',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'shsj',
      label: '审核日期',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }
  ],
  form: {
    isdb: false,
    isedit: true
  },
  queryapi: {
    url: '/lbj/jtgl/auditlist',
    method: 'post',
    callback: function (_this, res) {},
  },
}
