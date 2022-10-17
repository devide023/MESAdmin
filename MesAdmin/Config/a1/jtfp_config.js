{isgradequery:true,isbatoperate:true,isoperate:false,isfresh:true,isselect:true,batoperate:{export_excel:function(_this){_this.$request(_this.pageconfig.queryapi.method, _this.pageconfig.queryapi.url, {
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
});},},pagefuns:{},fields:[{coltype:'list',label:'岗位号',prop:'gwh',overflowtooltip:true,searchable:true,sortable:true,headeralign:'center',align:'center',options:[],},{coltype:'string',label:'机型',prop:'jxno',dbprop:'jx_no',overflowtooltip:true,searchable:true,sortable:true,headeralign:'center',align:'center',},{coltype:'string',label:'状态码',prop:'statusno',dbprop:'status_no',overflowtooltip:true,searchable:true,sortable:true,headeralign:'center',align:'center',},{coltype:'string',label:'备注',prop:'bz',overflowtooltip:true,searchable:true,sortable:true,headeralign:'center',align:'center',},{coltype:'string',label:'录入人',prop:'lrr1',overflowtooltip:true,searchable:true,sortable:true,headeralign:'center',align:'center',},{coltype:'datetime',label:'录入时间',prop:'lrsj1',overflowtooltip:true,searchable:true,sortable:true,headeralign:'center',align:'center',}],form:{id:'',jtid:'',gcdm:'',scx:'',gwh:'',jxno:'',statusno:'',bz:'',lrr1:'',lrsj1:'',lrr2:'',lrsj2:'',isdb:false,isedit:true},addapi:{url:'',method:'post',callback:function(vm,res){},},editapi:{url:'',method:'post',callback:function(vm,res){},},delapi:{url:'',method:'post',callback:function(vm,res){},},queryapi:{url:'',method:'post',callback:function(vm,res){},},}