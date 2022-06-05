{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  operate_fnlist: [],
  bat_btnlist: [{
      btntxt: '模板下载',
      fnname: 'download_template_file'
    }
  ],
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      this.list.unshift(row);
    },
    download_template_file() {
      window.open('http://172.16.201.125:7002/template/lbj/刀柄刃具关系.xlsx');
    }
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
    export_excel(_this) {
      _this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
        pageindex: 1,
        pagesize: 65535,
        search_condition: _this.queryform.search_condition
      }).then(function (res) {
        if (res.code === 1) {
          let expdatalist = res.list;
          _this.export_handle(_this.pageconfig.fields, expdatalist);
        } else if (res.code === 0) {
          this.$message.error(res.msg);
        }
      });
    },
    import_by_replace(_this, res) {},
    import_by_zh(_this, res) {},
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
        url: '/lbj/baseinfo/gcxx',
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
      coltype: 'string',
      label: '刀柄号',
      prop: 'dbh',
      headeralign: 'center',
      width: 300,
      align: 'center',
    }, {
      coltype: 'string',
      label: '刀柄类型',
      prop: 'dblx',
      headeralign: 'center',
      width: 300,
      align: 'center'
    }, {
      coltype: 'string',
      label: '刃具类型',
      prop: 'djlx',
      headeralign: 'center',
      width: 200,
      align: 'center',
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
    url: '/lbj/dbrjgx/add',
    method: 'post',
    callback: function (_this, res) {},
  },
  editapi: {
    url: '/lbj/dbrjgx/edit',
    method: 'post',
    callback: function (_this, res) {},
  },
  delapi: {
    url: '/lbj/dbrjgx/del',
    method: 'post',
    callback: function (_this, res) {},
  },
  queryapi: {
    url: '/lbj/dbrjgx/list',
    method: 'post',
    callback: function (_this, res) {},
  },
}
