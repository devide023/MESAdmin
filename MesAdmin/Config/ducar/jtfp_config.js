{
  isgradequery: true,
  isbatoperate: true,
  isoperate: true,
  isfresh: true,
  isselect: true,
  operate_fnlist: [{
      label: '查看Pdf',
      fnname: 'download_jstcpdf',
      btntype: 'text'
    },
	{
      label: '技通分配',
      fnname: 'jstz_fp',
      btntype: 'text',
    },
  ],
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
    download_jstcpdf: function (row) {
      var _this = this;
      if (row.jtly === 1) {
        window.open("http://jsgltj.zsdl.cn/tjjstz/file/" + row.wjlj);
      } else if (row.jtly === 0) {
        window.open("http://172.16.201.216:7002/jstz/" + row.wjlj);
      }
    },
    suggest_fn: function (vm, key, cb, row, col) {
      if (col.prop === 'jxno') {
        row.username = '';
        this.$request('get', '/a1/baseinfo/jxno_by_code', {
          key: key
        }).then(function (res) {
          if (res.code === 1) {
            cb(res.list);
          }
        });
      }
    },
    select_fn: function (vm, item, row, col) {
      if (col.prop === 'jxno') {
        row.statusno = '';
        this.$request('get', '/ducar/baseinfo/ztbm_by_jxno', {
          jxno: item.value
        }).then(function (res) {
          if (res.code === 1) {
            row.statusno_list = res.list.map(function (i) {
              return {
                label: i,
                value: i
              };
            });
          }
        });
      }
    },
	jstz_fp: function (row, item) {
      var _this = this;
      _this.dialog_title = row.jcbh + '分配';
      _this.dialog_width = '30%';
      _this.dialogVisible = true;
      _this.dialog_hidefooter = true;
      _this.dialog_viewpath = 'ducar/jstz/jtfp';
      _this.dialog_fnitem = item;
      _this.dialog_props = {
        row: row
      };
    },
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
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/ducar/jtgl/wfplist',
    method: 'post',
    callback: function (vm, res) {},
  },
}
