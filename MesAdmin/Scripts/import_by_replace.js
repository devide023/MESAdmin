if (res.files.length > 0) {
    var fid = res.files[0].fileid;
    try {
        _this.$request('get', '@@', {
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
}