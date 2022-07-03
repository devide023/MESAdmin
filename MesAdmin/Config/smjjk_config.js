{
  isgradequery: true,
  isselect: true,
  isoperate: false,
  pagefuns: {},
  fields: [{
      coltype: 'list',
      prop: 'gcdm',
      label: '工厂',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
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
      fixed: 'left',
      width: 110,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'statusno',
      dbprop: 'status_no',
      label: '状态码',
      headeralign: 'center',
      align: 'center',
      width: 110,
      fixed: 'left',
    }, {
      coltype: 'string',
      prop: 'wlmc',
      label: '名称',
      headeralign: 'center',
      align: 'left',
      width: 100,
      overflowtooltip: true,
      searchable: false,
    }, {
      coltype: 'string',
      prop: 'engineno',
      label: '产品件号',
      headeralign: 'center',
      align: 'left',
      width: 180,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'orderno',
      label: '订单号',
      headeralign: 'center',
      align: 'left',
      width: 100,
    }, {
      coltype: 'string',
      prop: 'bc',
      label: '班次',
      headeralign: 'center',
      align: 'center',
      width: 80,
    }, {
      coltype: 'string',
      prop: 'smjbs',
      label: '首末件标识',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'image',
      prop: 'jczpdz',
      label: '首末件照片',
      headeralign: 'center',
      align: 'center',
      width: 130,
      action: 'http://172.16.201.125:7002/api/upload/image',
      accept: '.jpg,.jpeg,.png',
      before_upload: function (file) {
        var isJPG = file.type === "image/jpeg" || file.type === "image/jpg" || file.type === "image/png";
        var isLt2M = file.size / 1024 / 1024 < 5;
        if (!isJPG) {
          this.$message.error("上传图片只能是JPG,PNG格式!");
        }
        if (!isLt2M) {
          this.$message.error("上传图片大小不能超过 5MB!");
        }
        return isJPG && isLt2M;
      },
      upload_success: function (res, file) {
        if (res.code === 1) {
          if (res.files.length > 0) {
            var rowkey = res.extdata.rowkey;
            var findrow = this.$basepage.list.find((t) => t.rowkey === rowkey);
            if (findrow) {
              findrow.jczpdz = res.files[0].fileid;
            }
          }
        } else if (res.code === 0) {
          this.$message.error(res.msg);
        }
      }
    }, {
      coltype: 'string',
      prop: 'zpjcjg',
      label: '首末件照片结果',
      headeralign: 'center',
      align: 'center',
      width: 130,
    }, {
      coltype: 'string',
      prop: 'jcr',
      label: '首末件检测人',
      headeralign: 'center',
      align: 'center',
      width: 130,
    }, {
      coltype: 'datetime',
      prop: 'jcsj',
      label: '检测时间',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'szbjg',
      label: '三坐标结果',
      headeralign: 'center',
      align: 'center',
      width: 100,
      overflowtooltip: true,
    }, {
      coltype: 'string',
      prop: 'cpzt',
      label: '三坐标检测人',
      headeralign: 'center',
      align: 'center',
      width: 120,
    }, {
      coltype: 'datetime',
      prop: 'szbjcsj',
      label: '三坐标结果时间',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,
    }, {
      coltype: 'datetime',
      prop: 'wcsj',
      label: '完成时间',
      headeralign: 'center',
      align: 'center',
      width: 120,
      overflowtooltip: true,
    }, {
      coltype: 'bool',
      prop: 'scbz',
      label: '删除标识',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'bool',
      prop: 'sfbh',
      label: '是否闭环',
      headeralign: 'center',
      align: 'center',
    }
  ],
  queryapi: {
    url: '/lbj/smjjk/list',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/smjjk/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
}
