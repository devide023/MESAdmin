{
    pagefuns: {
        editrow: function (row)
        {
            console.log(row);
            row.isedit = true;
        },
        add_handle: function ()
        {
            var row = this.deepClone(this.pageconfig.form);
            row.status = 1;
            row.adduser = this.$store.getters.name;
            row.addtime = this.parseTime(new Date());
            this.list.unshift(row);
        },
        delhandle: function ()
        {
            console.log(this);
        },
        savehandle: function ()
        {

        }
    },
    isgradequery: true,
        fields: [
            {
                coltype: 'bool',
                prop: 'status',
                label: '状态',
                headeralign: 'center',
                align: 'center',
                width: 100,
                activevalue: 1,
                inactivevalue: 0,
            },
            {
                coltype: 'string',
                prop: 'code',
                label: '用户编码',
                headeralign: 'center',
                align: 'left',
                width: 100,
            },
            {
                coltype: 'string',
                prop: 'name',
                label: '用户姓名',
                headeralign: 'center',
                align: 'left',
            },
            {
                coltype: 'string',
                prop: 'adduser',
                label: '录入人',
                headeralign: 'center',
                align: 'left',
            },
            {
                coltype: 'datetime',
                prop: 'addtime',
                label: '录入时间',
                headeralign: 'center',
                align: 'left',
            }
        ],
            form: {
        status: 1,
            code: '',
                name: '',
                    adduser: '',
                        addtime: '',
                            isdb: false,
                                isedit: true
    },
    addapi: {
        url: '/user/add',
            method: 'post',
                callback: function(vm, res)
        {
        }
    },
    delapi: {
        url: '/user/del',
            method: 'post',
                callback: function(vm, res)
        {
        }
    },
    editapi: {
        url: '/user/edit',
            method: 'post',
                callback: function(vm, res)
        {
        }
    },
    queryapi: {
        url: '/user/list',
            method: 'post',
                callback: function(vm, res)
        {
        }
    }
}