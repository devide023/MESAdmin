{
  isgradequery: true,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: false,
  operate_fnlist: [
  {
      label: '下载Pdf',
      fnname: 'download_jstcpdf',
      btntype: 'text'
    }],
  pagefuns: {
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
    }
  },
  fields: [{
      coltype: 'list',
      prop: 'scx',
	  dbprop:'ta.scx',
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
	  dbprop:'ta.jcbh',
      label: '技通编号',
      headeralign: 'center',
      align: 'center',
      width: 150,
      overflowtooltip: true,
    }, {
      coltype: 'list',
      prop: 'wjfl',
	  dbprop:'ta.wjfl',
      label: '分类',
      headeralign: 'center',
      align: 'center',
      options: [{
          label: '技通',
          value: '技通'
        }, {
          label: '技改',
          value: '技改'
        }, 		
		{
          label: '清单',
          value: '清单'
        },
		{
          label: '通知单',
          value: '通知单'
        },
		{
          label: '作业文件',
          value: '作业文件'
        },
		{
          label: '质量警示',
          value: '质量警示'
        },
		{
          label: '内控标准',
          value: '内控标准'
        },
		{
          label: '发放明细',
          value: '发放明细'
        }
      ]
    }, {
      coltype: 'string',
      prop: 'jcmc',
	  dbprop:'ta.jcmc',
      label: '技通名称',
      headeralign: 'center',
      align: 'center',
      width: 150,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'jcms',
	  dbprop:'ta.jcms',
      label: '技通描述',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'date',
      prop: 'yxqx1',
	  dbprop:'ta.yxqx1',
      label: '有效日期开始',
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'date',
      prop: 'yxqx2',
	  dbprop:'ta.yxqx2',
      label: '有效日期结束',
      headeralign: 'center',
      align: 'center',
      width: 150
    }, {
      coltype: 'string',
      prop: 'jwdx',
	  dbprop:'ta.jwdx',
      label: '文件大小',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      prop: 'scry',
	  dbprop:'ta.scry',
      label: '上传者',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'scsj',
	  dbprop:'ta.scsj',
      label: '上传时间',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'bool',
      prop: 'fpflg',
      dbprop: 'ta.fp_flg',
      label: '分配标识',
      headeralign: 'center',
      align: 'center',
      width: 80,
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'bool',
      prop: 'shbz',
	  dbprop:'ta.shbz',
      label: '审核标志',
      headeralign: 'center',
      align: 'center',
      width: 80,
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      prop: 'fpr',
	  dbprop:'ta.fpr',
      label: '分配人',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'fpsj',
      dbprop: 'ta.fp_sj',
      label: '分配日期',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'shr',
	  dbprop:'ta.shr',
      label: '审核人',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      prop: 'shsj',
	  dbprop:'ta.shsj',
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
    url: '/lbj/jtgl/mydoclist',
    method: 'post',
    callback: function (vm, res) {},
  },
}
