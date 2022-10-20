{
  isgradequery: true,
  isbatoperate: false,
  isoperate: false,
  isfresh: true,
  isselect: true,
  batoperate: {
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
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    }
  },
  fields: [{
      coltype: 'list',
      label: '生产线',
      prop: 'scx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100,
	  options:[],
	  inioptionapi: {
        method: 'get',
        url: '/a1/baseinfo/scx'
      },
	  hideoptionval:true
    }, {
      coltype: 'string',
      label: '通知内容',
      prop: 'tzxx',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'left',
    }, {
      coltype: 'string',
      label: '发出人',
      prop: 'lrr',
      overflowtooltip: true,

      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 100
    }, {
      coltype: 'datetime',
      label: '发出日期',
      prop: 'lrsj',
      overflowtooltip: true,
      sortable: true,
      headeralign: 'center',
      align: 'center',
      width: 150
    }
  ],
  form: {
    scx: '1',
    tzxx: '',
    tzfl: '',
    fcbm: '',
    lrr: '',
    lrsj: '',
    xbsj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/a1/scxtz/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/a1/scxtz/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/a1/scxtz/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/a1/scxtz/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
