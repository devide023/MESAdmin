{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    }
  },
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/dbxx/readxls', {
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
      coltype: 'string',
      label: '刀柄号',
      prop: 'dbh',
      headeralign: 'center',
      align: 'center',
      width: 150,
    }, {
      coltype: 'string',
      label: '刀柄名称',
      prop: 'dbmc',
      headeralign: 'center',
      align: 'center',
	  width:150,
	  overflowtooltip: true,
    }, {
      coltype: 'string',
      label: '刀柄类型',
      prop: 'dblx',
      headeralign: 'center',
      align: 'center',
    }, {
      coltype: 'date',
      label: '采购时间',
      prop: 'cgsj',
      headeralign: 'center',
      align: 'center',
      width: 100,
    }, {
      coltype: 'list',
      label: '刀柄状态',
      prop: 'dbzt',
      headeralign: 'center',
      align: 'center',
      width: 100,
      options: [{
          label: '空闲中',
          value: '空闲中'
        }, {
          label: '使用中',
          value: '使用中'
        }, {
          label: '报废',
          value: '报废'
        }
      ],
    }, {
      coltype: 'string',
      label: '录入人',
      prop: 'lrr',
      headeralign: 'center',
      align: 'center',
      width: 80,
    }, {
      coltype: 'datetime',
      label: '录入时间',
      prop: 'lrsj',
      headeralign: 'center',
      align: 'center',
      width: 130,
    }, ],
  form: {
    gcdm: '9902',
    dbmc: '',
    dblx: '',
    dbh: '',
    cgsj: '',
    dbzt: '',
    lrr: '',
    lrsj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/lbj/dbxx/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/lbj/dbxx/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/lbj/dbxx/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/lbj/dbxx/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
