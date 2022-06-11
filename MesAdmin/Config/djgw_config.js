{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    },
  },
  batoperate: {
    import_by_add: function (_this, res) {
		if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/djgw/readxls', {
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
    export_excel(_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields, expdatalist);
        } else if(res.code === 0) {
          this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {},
    import_by_zh(_this,res) {},
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
      prop: 'djno',
      label: '点检编号',
      headeralign: 'center',
      align: 'left',
      width: 80,
      overflowtooltip: true,
    },
{
      coltype: 'string',
      prop: 'gwh',
      label: '岗位号',
      headeralign: 'center',
      align: 'center',
      width: 80,
    },
	{
      coltype: 'list',
      prop: 'gwh',
      label: '岗位名称',
      headeralign: 'center',
      align: 'center',
      width: 150,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: []
    }, {
      coltype: 'string',
      prop: 'statusno',
      dbprop: 'status_no',
      label: '产品',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
      prop: 'djxx',
      label: '点检内容',
      headeralign: 'center',
      align: 'left',
      overflowtooltip: true,
    }, {
      coltype: 'bool',
      prop: 'scbz',
      label: '删除标志',
      headeralign: 'center',
      align: 'center',
      width: 80,
      activevalue: 'Y',
      inactivevalue: 'N',
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'left',
      width: 80,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      width: 130,
      overflowtooltip: true,
    },
  ],
  form: {
    gcdm: '9902',
    scx: '',
    gwh: '',
    statusno: '',
    djno: '',
    djxx: '',
    scbz: 'N',
    lrr: '',
    lrsj: '',
    isdb: false,
    isedit: true,
  },
  addapi: {
    url: '/lbj/djgw/add',
    method: 'post',
    callback: function (vm, res) {}
  },
  delapi: {
    url: '/lbj/djgw/del',
    method: 'post',
    callback: function (vm, res) {}
  },
  editapi: {
    url: '/lbj/djgw/edit',
    method: 'post',
    callback: function (vm, res) {}
  },
  queryapi: {
    url: '/lbj/djgw/list',
    method: 'post',
    callback: function (vm, res) {}
  }
}
