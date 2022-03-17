{
    fields: [
        {
            value: 'lrr',
            type: 'string',
            label: '用户名'
        },
        {
            value: 'lrr',
            type: 'string',
            label: '录入人'
        },
        {
            value: 'lrsj',
            type: 'date',
            label: '录入时间'
        }
    ],
        form: {
        gcdm: '',
            scx: '',
                gwh: '',
                    isdb: false,
                        isedit: true
    },
    addapi: {
        url: '/user/add',
            method: 'post'
    },
    delapi: {
        url: '/user/del',
            method: 'post'
    },
    editapi: {
        url: '/user/edit',
            method: 'post',
                callback: function() {}
    },
    queryapi: {
        url: '/user/list',
            method: 'post'
    }
}
