{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  operate_fnlist: [],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    },
  },
  batoperate: {
    import_by_add: function (_this, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          _this.$request('get', '/lbj/dbrjgx/readxls', {
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
    import_by_replace: function (vm, res) {},
    import_by_zh: function (vm, res) {},
    export_excel: function (vm) {}
  },
  fields: [{
      coltype: 'list',
      label: '工厂',
      prop: 'gcdm',
      headeralign: 'center',
      align: 'center',
      width: 150,
      inioptionapi: {
        method: 'get',
        url: 'lbj/baseinfo/gcxx',
      },
      options: [],
    }, {
      coltype: 'string',
      label: '产品',
      prop: 'cpzt',
      headeralign: 'center',
      width: 300,
      align: 'center',
    }, {
      coltype: 'list',
      label: '刀柄号',
      prop: 'dbh',
      headeralign: 'center',
      width: 300,
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: 'lbj/baseinfo/dbxx',
      },
      options: [],
    }, {
      coltype: 'list',
      label: '刃具类型',
      prop: 'djlx',
      headeralign: 'center',
      width: 200,
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: 'lbj/baseinfo/rjlx',
      },
      options: [],
    }, ],
  form: {
    gcdm: '9902',
    dbh: '',
    cpzt: '',
    djlx: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: 'lbj/dbrjgx/add',
    method: 'post',
    callback: function (_this, res) {},
  },
  editapi: {
    url: 'lbj/dbrjgx/edit',
    method: 'post',
    callback: function (_this, res) {},
  },
  delapi: {
    url: 'lbj/dbrjgx/del',
    method: 'post',
    callback: function (_this, res) {},
  },
  queryapi: {
    url: 'lbj/dbrjgx/list',
    method: 'post',
    callback: function (_this, res) {},
  },
}
