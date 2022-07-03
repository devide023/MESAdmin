{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  pagefuns: {
    add_handle: function () {
      this.dialogVisible = true;
    },
	download_template_file() {
      window.open('http://172.16.201.125:7002/template/lbj/刀柄刃具在线.xlsx');
    },
    rjrm_handle: function () {
      var _this = this;
      if (_this.selectlist.length > 0) {
        var postdata = this.selectlist.map(t => t.id);
        this.$request('post', '/lbj/dbrjly/zxrjrm', postdata).then(function (res) {
          if (res.code === 1) {
            _this.$message.success(res.msg);
          } else if (res.code === 0) {
            _this.$message.error(res.msg);
          }
        });
      } else {
        this.$message.warning('请选择刃具');
      }
    }
  },
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
      headeralign: 'center',
      align: 'center',
      width: 80,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gcxx',
      },
      options: [],
    }, {
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      headeralign: 'center',
      align: 'center',
      width: 180,
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902',
      },
      options: [],
    }, {
      coltype: 'string',
      label: '设备',
      prop: 'sbbh',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刀柄号',
      prop: 'dbh',
      dbprop: 't1.dbh',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刀柄类型',
      prop: 'dblx',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刀柄名称',
      prop: 'dbmc',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刃具类型',
      prop: 'rjlx',
      dbprop: 't1.rjlx',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刃具名称',
      prop: 'rjmc',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '产品状态',
      prop: 'cpzt',
      dbprop: 't3.cpzt',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '标准寿命',
      prop: 'rjbzsm',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '当前寿命',
      prop: 'rjdqsm',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'string',
      label: '刀柄领用人',
      prop: 'dblyr',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '刀柄领用时间',
      prop: 'dblysj',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '刃具领用人',
      prop: 'rjlyr',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'datetime',
      label: '刃具领用时间',
      prop: 'rjlysj',
      headeralign: 'center',
      align: 'center',
      overflowtooltip: true,
    }, ],
  form: {
    gcdm: '',
    scx: '',
    sbbh: '',
    dbh: '',
    rjlx: '',
    rjbzsm: '',
    rjdqsm: '',
    dblyr: '',
    dblysj: '',
    rjlyr: '',
    rjlysj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/dbrjly/add',
    method: 'post',
    callback: function (_this, res) {},
  },
  editapi: {
    url: '/lbj/dbrjly/edit',
    method: 'post',
    callback: function (_this, res) {},
  },
  delapi: {
    url: '/lbj/dbrjly/del',
    method: 'post',
    callback: function (_this, res) {},
  },
  queryapi: {
    url: '/lbj/dbrjly/list',
    method: 'post',
    callback: function (_this, res) {},
  },
}
