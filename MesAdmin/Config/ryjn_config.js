{
    isgradequery: true,
        isoperate: false,
        pagefuns: {
        add_handle: function ()
        {
            var row = this.$deepClone(this.pageconfig.form);
            row.lrr = this.$store.getters.name;
            row.lrsj = this.$parseTime(new Date());
            this.list.unshift(row);
        },
        select_usercode_handle: function()
        {
            console.log(this.$basepage);
        }
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
            prop: 'jnbh',
            label: '技能编号',
            headeralign: 'center',
            align: 'center',
            width:80,
        },
        {
            coltype: 'string',
            prop: 'usercode',
            label: '账号',
            headeralign: 'center',
            align: 'center',
            suggest: function (key, cb)
            {
                this.$request('get', '/lbj/baseinfo/usercode', { key: key }).then(function(res)
                {
                    if (res.code === 1) {
                        cb(res.list);
                    } else {
                        this.$message.error(res.msg);
                    }
                });
            },
            select_handlename:'select_usercode_handle'
        },
        {
            coltype: 'string',
            prop: 'jnxx',
            label: '技能',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'list',
            prop: 'gwh',
            label: '岗位号',
            headeralign: 'center',
            align: 'center',
            inioptionapi: {
                method: 'get',
                url: '/lbj/baseinfo/gwzd'
            },
            options: []
        },
        {
            coltype: 'list',
            prop: 'jnfl',
            label: '技能分类',
            headeralign: 'center',
            align: 'center',
            options: [{ label: '装配', value: '装配' }, { label: '测试', value: '测试' }, { label: '校验', value: '校验' }, { label: '安全', value: '安全' }, { label: '其他', value: '其他' }]
        },
        {
            coltype: 'date',
            prop: 'jnsj',
            label: '技能时间',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'rate',
            prop: 'jnsld',
            label: '熟练度',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'bool',
            prop: 'sfhg',
            label: '是否合格',
            headeralign: 'center',
            align: 'center',
            activevalue: 'Y',
            inactivevalue: 'N',
        },
        {
            coltype: 'string',
            prop: 'lrr',
            label: '录入人',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'datetime',
            prop: 'lrsj',
            label: '录入时间',
            headeralign: 'center',
            align: 'center',
            overflowtooltip: true,
        }
    ],
        form: {
        gcdm: '9902',
            scx: '',
            usercode: '',
        jnbh: '',
            jnxx: '',
            gwh: '',
            sfhg: 'Y',
            lrr: '',
            lrsj: '',
            jnfl: '',
            jnsj: '',
            jnsld: 0,
            isdb: false,
                isedit:true,
    },
        addapi: {
        url: '/lbj/ryjn/add',
            method: 'post',
                callback: function(vm, res)
        {

        }
    },
    delapi: {
        url: '/lbj/ryjn/del',
            method: 'post',
                callback: function(vm, res)
        {

        }
    },
    editapi: {
        url: '/lbj/ryjn/edit',
            method: 'post',
                callback: function(vm, res)
        {

        }
    },
    queryapi: {
        url: '/lbj/ryjn/list',
            method: 'post',
                callback: function(vm, res)
        {

        }
    },
}