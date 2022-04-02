{
    isgradequery: true,
        isoperate: true,
        operate_fnlist: [{
            label: '上传Pdf', btntype: 'upload', action: 'http://localhost:52655/api/upload/pdf', callback: function (response, file)
            {
                if (response.code === 1) {
                    this.$message.success(response.msg);
                    let rowid = response.extdata.rowkey;
                    let finditem = this.$basepage.list.find(function(i){ return i.rowkey === rowid;});
                    if (finditem) {
                        finditem.jwdx = file.size;
                        finditem.gymc = file.name;
                        finditem.gybh = '';
                        finditem.wjlj = file.name;
                        finditem.scry = this.$store.getters.name;
                        finditem.scsj = this.$parseTime(new Date());
                    }
                } else {
                    this.$message.error(response.msg);
                }
            }
        },
            {
                label: '下载Pdf', fnname:'download_dzgypdf',btntype:'text'
            }
        ],
        isbatoperate: true,
        isfresh:true,
        pagefuns: {
        add_handle: function ()
        {
            var row = this.$deepClone(this.pageconfig.form);
            row.adduser = this.$store.getters.name;
            row.addtime = this.$parseTime(new Date());
            this.list.unshift(row);
        },
        download_dzgypdf: function(row)
        {
            console.log(row);
            if (row.wjlj) {
                window.open("http://localhost:52655/upload/downloadpdf?filename=" + row.wjlj);
            }
        }
    },
    batoperate: {
        import_by_add: function(vm, res) {},
        import_by_replace: function(vm, res) {},
        import_by_zh: function(vm, res) {},
        export_excel: function(vm) {}
    },
    fields: [
        {
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
        },
        {
            coltype: 'list',
            prop: 'scx',
            label: '生产线',
            headeralign: 'center',
            align: 'center',
            width: 80,
            overflowtooltip: true,
            inioptionapi: {
                method: 'get',
                url: '/lbj/baseinfo/scx?gcdm=9902'
            },
            options: []
        },
        {
            coltype: 'string',
            prop: 'statusno',
            label: '产品',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'string',
            prop: 'gymc',
            label: '工艺名称',
            headeralign: 'center',
            align: 'left',
        },
        {
            coltype: 'string',
            prop: 'gyms',
            label: '工艺描述',
            headeralign: 'center',
            align: 'left',
            overflowtooltip: true,
        },
        {
            coltype: 'string',
            prop: 'jwdx',
            label: '文件大小',
            headeralign: 'center',
            align: 'left',
        },
        {
            coltype: 'list',
            prop: 'wjfl',
            label: '文件分类',
            headeralign: 'center',
            align: 'left',
            options: [{ label: '图纸', value: '图纸' }, { label: '视频', value: '视频' }]
        },
        {
            coltype: 'datetime',
            prop: 'scsj',
            label: '上传日期',
            headeralign: 'center',
            align: 'center',
        },
    ],
        form: {
        gcdm: '9902',
            scx: '',
            statusno: '',
            gymc: '',
            gyms: '',
            jwdx: '',
            wjfl: '图纸',
            scsj: '',
            wjlj:'',
        isdb: false,
            isedit: true,
    },
        addapi: {
        url: '/lbj/dzgy/add',
            method: 'post',
                callback: function(vm, res)
        {
        }
    },
    delapi: {
        url: '/lbj/dzgy/del',
            method: 'post',
                callback: function(vm, res)
        {
        }
    },
    editapi: {
        url: '/lbj/dzgy/edit',
            method: 'post',
                callback: function(vm, res)
        {
        }
    },
    queryapi: {
        url: '/lbj/dzgy/list',
            method: 'post',
                callback: function(vm, res)
        {
        }
    }
}