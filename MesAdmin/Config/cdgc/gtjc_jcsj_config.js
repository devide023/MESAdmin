{
  isgradequery: true,
  isbatoperate: true,
  isoperate: false,
  isfresh: true,
  isselect: true,
  operate_fnlist: [],
  batoperate:{
	  import_by_add: function (vm, res) {
      if (res.files.length > 0) {
        var fid = res.files[0].fileid;
        try {
          vm.$request('get', '/cdgc/gtjc/jcsj/readxls', {
            fileid: fid
          }).then(function (result) {
            vm.$loading().close();
            if (result.code === 1) {
              vm.$message.success(result.msg);
            } else if (result.code === 2) {
              vm.$message.warning(result.msg);
            } else if (result.code === 0) {
              vm.$message.error(result.msg);
            }
            vm.getlist(vm.queryform);
          });
        } catch (error) {
          vm.$message.error(error);
        }
      } else {
        vm.$loading().close();
      }
    },
  },
  pagefuns: {
    add_handle: function () {
      var row = this.$deepClone(this.pageconfig.form);
      row.lrr = this.$store.getters.name;
      row.lrsj = this.$parseTime(new Date());
      this.list.unshift(row);
    }
  },
  fields: [{
      coltype: 'string',
      prop: 'cplx',
      label: '产品类型',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
	  fixed:'left'
    }, {
      coltype: 'string',
      prop: 'mh',
      label: '产品模号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
	  fixed:'left'
    }, {
      coltype: 'list',
      prop: 'cpfw',
      label: '产品方位',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
      options: [{
          label: '1面',
          value: '1面'
        }, {
          label: '2面',
          value: '2面'
        }, {
          label: '3面',
          value: '3面'
        }, {
          label: '4面',
          value: '4面'
        }, {
          label: '5面',
          value: '5面'
        }, {
          label: '6面',
          value: '6面'
        }, {
          label: '裙部让位',
          value: '裙部让位'
        }, {
          label: '其他',
          value: '其他'
        }, {
          label: '结果',
          value: '结果'
        },
      ],
	  fixed:'left',
	  hideoptionval:true,
    }, {
      coltype: 'string',
      prop: 'th',
      label: '图号',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
	  fixed:'left'
    }, {
      coltype: 'string',
      prop: 'kxmc',
      label: '孔系名称',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
	  fixed:'left'
    }, 
	{
      coltype: 'string',
      prop: 'kjzsz',
      label: '孔径展示值',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
	  fixed:'left'
    },
	{
      coltype: 'string',
      prop: 'sdzsz',
      label: '深度/面距展示值',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
	  fixed:'left'
    },
	{
      coltype: 'list',
      prop: 'kjtype',
      label: '孔径输入类型',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
	  options:[{
		  label:'单选',
		  value:'radio'
	  },
	  {
		  label:'输入',
		  value:'text'
	  }
	  ],
	  hideoptionval:true
    },
	{
      coltype: 'list',
      prop: 'sdtype',
      label: '深度输入类型',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150,
	  options:[{
		  label:'输入',
		  value:'text'
	  },
	  {
		  label:'不填',
		  value:'none'
	  },
	  {
		  label:'T',
		  value:'T'
	  },
	  ],
	  hideoptionval:true
    },
	{
      coltype: 'string',
      prop: 'kjcc',
      label: '孔径尺寸',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100
    },
	{
      coltype: 'string',
      prop: 'kjccsx',
      dbprop: 'kjcc_sx',
      label: '孔径上限',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
      sortable: true,
    }, {
      coltype: 'string',
      prop: 'kjccxx',
      dbprop: 'kjcc_xx',
      label: '孔径下限',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100
    }, {
      coltype: 'string',
      prop: 'sdmj',
      label: '深度面距',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100
    }, {
      coltype: 'string',
      prop: 'sdmjsx',
      dbprop: 'sdmj_sx',
      label: '深度面距上限',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120
    }, {
      coltype: 'string',
      prop: 'sdmjxx',
      dbprop: 'sdmj_xx',
      label: '深度面距下限',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 120
    }, {
      coltype: 'string',
      prop: 'lrr',
      label: '录入人',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 100,
    }, {
      coltype: 'datetime',
      prop: 'lrsj',
      label: '录入时间',
      headeralign: 'center',
      align: 'center',
      sortable: true,
      width: 150
    }
  ],
  form: {
    id: '',
    cplx: '',
    mh: '',
    cpfw: '',
    th: '',
    kxmc: '',
    kjcc: '',
    kjccsx: '',
    kjccxx: '',
    sdmj: '',
    sdmjsx: '',
    sdmjxx: '',
    lrr: '',
    lrsj: '',
    isdb: false,
    isedit: true
  },
  addapi: {
    url: '/cdgc/gtjc/jcsj/add',
    method: 'post',
    callback: function (vm, res) {},
  },
  editapi: {
    url: '/cdgc/gtjc/jcsj/edit',
    method: 'post',
    callback: function (vm, res) {},
  },
  delapi: {
    url: '/cdgc/gtjc/jcsj/del',
    method: 'post',
    callback: function (vm, res) {},
  },
  queryapi: {
    url: '/cdgc/gtjc/jcsj/list',
    method: 'post',
    callback: function (vm, res) {},
  },
}
