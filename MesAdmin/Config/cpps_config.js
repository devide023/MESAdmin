{
  isgradequery: false,
  isbatoperate: false,
  isoperate: true,
  isfresh: true,
  isselect: false,
  operate_fnlist: [{
      label: '合格',
      fnname: 'product_audit_ok',
      btntype: 'text'
    }, {
      label: '不合格',
      fnname: 'product_audit_nok',
      btntype: 'text'
    },
	{
      label: '返工',
      fnname: 'product_audit_fg',
      btntype: 'text'
    }
  ],
  pagefuns: {
    product_audit_ok: function (row) {
      let postdata = {
        ID: row.ID,
        GCDM: row.GCDM,
        SCX: row.SCX,
        GWH: row.GWH,
        ORDER_NO: row.ORDER_NO,
        ENGINE_NO: row.ENGINE_NO,
        ZTBM: row.STATUS_NO,
        JCSJ: this.$parseTime(new Date()),
        JCRY: row.CREATE_USER_CODE,
        JCJG: "OK",
        JCZ: row.TYPE_ID,
        JGZ: null,
        SBBH: null,
        SBLX: null,
        IS_SAVE_GDXX: true,
        GWXH: row.GWXH
      };
      this.submit_data(postdata);
    },
    product_audit_nok: function (row) {
      let postdata = {
        ID: row.ID,
        GCDM: row.GCDM,
        SCX: row.SCX,
        GWH: row.GWH,
        ORDER_NO: row.ORDER_NO,
        ENGINE_NO: row.ENGINE_NO,
        ZTBM: row.STATUS_NO,
        JCSJ: this.$parseTime(new Date()),
        JCRY: row.CREATE_USER_CODE,
        JCJG: "NOK",
        JCZ: row.TYPE_ID,
        JGZ: null,
        SBBH: null,
        SBLX: null,
        IS_SAVE_GDXX: true,
        GWXH: row.GWXH
      };
      this.submit_data(postdata);
    },
	product_audit_fg:function(row){
		let postdata = {ID:row.ID,USER_CODE:this.$store.getters.userinfo.code};
		this.close_cpps(postdata);
	}
  },
  fields: [ {
      coltype: 'list',
      prop: 'SCX',
      label: '生产线',
      headeralign: 'center',
      align: 'center',
      fixed: 'left',
      overflowtooltip: true,
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/scx?gcdm=9902'
      },
      options: []
    }, {
      coltype: 'list',
      prop: 'GWH',
      label: '岗位',
      headeralign: 'center',
      align: 'center',
      inioptionapi: {
        method: 'get',
        url: '/lbj/baseinfo/gwzd'
      },
      options: [],
      relation: 'gwhoptions',
    }, {
      coltype: 'string',
      prop: 'ENGINE_NO',
      label: '件号',
      width: 200,
      headeralign: 'center',
      align: 'center',
      searchable: false,
    },
	{
      coltype: 'string',
      prop: 'STATUS_NO',
      label: '状态编码',
      headeralign: 'center',
      align: 'center',
      searchable: false,
    },
	{
      coltype: 'string',
      prop: 'ORDER_NO',
      label: '订单号',
      headeralign: 'center',
      align: 'center',
      searchable: false,
    }, {
      coltype: 'string',
      prop: 'STATUS_NAME',
      label: '状态',
      headeralign: 'center',
      align: 'center',
      searchable: false,
    }, 
	{
      coltype: 'string',
      prop: 'TYPE_NAME',
      label: '工废料废',
      headeralign: 'center',
      align: 'center',
      searchable: false,
    },
	{
      coltype: 'string',
      prop: 'CREATE_USER_CODE',
      label: '创建人工号',
      headeralign: 'center',
      align: 'center',
      searchable: false,
    }, {
      coltype: 'datetime',
      prop: 'CREATE_TIME',
      label: '创建时间',
      headeralign: 'center',
      align: 'center',
      searchable: false,
      sortable: true,
    },
	{
      coltype: 'string',
      prop: 'APPROVE_USER_CODE',
      label: '审核人工号',
      headeralign: 'center',
      align: 'center',
    },
	{
      coltype: 'datetime',
      prop: 'APPROVE_TIME',
      label: '审核时间',
      headeralign: 'center',
      align: 'center',
    }
  ],
  form: {
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '',
    method: 'post',
    callback: function (vm, res) {},
  },
}
