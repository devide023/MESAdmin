{
    isgradequery: true,
        isselect: false,
        isoperate: false,
        pagefuns: {},
    fields: [
        {
            coltype: 'string',
            prop: 'gcdm',
            label: '工厂',
            headeralign: 'center',
            align: 'center',
            inioptionapi: {
                method: 'get',
                url: '/lbj/baseinfo/gcxx'
            },
            options: []
        },
        {
            coltype: 'string',
            prop: 'scx',
            label: '生产线',
            headeralign: 'center',
            align: 'center',
            inioptionapi: {
                method: 'get',
                url: '/lbj/baseinfo/scx?gcdm=9902'
            },
            options: []
        },
        {
            coltype: 'string',
            prop: 'sbbh',
            label: '设备编号',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'string',
            prop: 'sbmc',
            label: '设备名称',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'string',
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
            coltype: 'string',
            prop: 'sblx',
            label: '设备类型',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'string',
            prop: 'ljlx',
            label: '连接类型',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'string',
            prop: 'ip',
            label: 'IP',
            headeralign: 'center',
            align: 'center',
        },
        {
            coltype: 'string',
            prop: 'port',
            label: '端口号',
            headeralign: 'center',
            align: 'center',
            width: 80
        },
        {
            coltype: 'string',
            prop: 'sfky',
            label: '是否可用',
            headeralign: 'center',
            align: 'center',
            width: 80
        },
        {
            coltype: 'string',
            prop: 'sflj',
            label: '是否连接',
            headeralign: 'center',
            align: 'center',
            width:80
        },
        {
            coltype: 'string',
            prop: 'bz',
            label: '备注',
            headeralign: 'center',
            align: 'center',
        }
    ],
        queryapi: {
        url: '/lbj/sbxx/list',
            method: 'post',
                callback: function(vm, res)
        {
        }
    }
}