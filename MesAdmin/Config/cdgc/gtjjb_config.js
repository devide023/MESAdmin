{isgradequery:true,isbatoperate:true,isoperate:false,isfresh:true,isselect:true,batoperate:{import_by_add:function(_this, res){if (res.files.length > 0) {
    var fid = res.files[0].fileid;
    try {
        _this.$request('get', '/**/**/readxls', {
            fileid: fid
        }).then(function (result)
        {
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
}},import_by_replace:function(_this, res){if (res.files.length > 0) {
    var fid = res.files[0].fileid;
    try {
        _this.$request('get', '/**/**/readxls_by_replace', {
            fileid: fid
        }).then(function (result)
        {
            _this.$loading().close();
            if (result.code === 1) {
                _this.$message.success(result.msg);
                _this.getlist(_this.queryform);
            } else if (result.code === 2) {
                _this.$message.warning(result.msg);
            } else if (result.code === 0) {
                _this.$message.error(result.msg);
            }
        })
    } catch (error) {
        _this.$message.error(error);
    }
} else {
    _this.$loading().close();
}},import_by_zh:function(_this, res){if (res.files.length > 0) {
    var fid = res.files[0].fileid;
    try {
        _this.$request('get', '/**/**/readxls_by_zh', {
            fileid: fid
        }).then(function (result)
        {
            _this.$loading().close();
            if (result.code === 1) {
                _this.$message.success(result.msg);
                _this.getlist(_this.queryform);
            } else if (result.code === 2) {
                _this.$message.warning(result.msg);
            } else if (result.code === 0) {
                _this.$message.error(result.msg);
            }
        });
    } catch (error) {
        _this.$message.error(error);
    }
} else {
    _this.$loading().close();
}},export_excel:function(_this){_this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
    pageindex: 1,
    pagesize: 65535,
    search_condition: _this.queryform.search_condition
}).then(function (res)
{
    if (res.code === 1) {
        let expdatalist = res.list;
        _this.export_handle(_this.pageconfig.fields, expdatalist);
    } else if (res.code === 0) {
        _this.$message.error(res.msg);
    }
});},},pagefuns:{},fields:[],form:{isdb:false,isedit:true},addapi:{url:'',method:'post',callback:function(vm,res){},},editapi:{url:'',method:'post',callback:function(vm,res){},},delapi:{url:'',method:'post',callback:function(vm,res){},},queryapi:{url:'',method:'post',callback:function(vm,res){},},}