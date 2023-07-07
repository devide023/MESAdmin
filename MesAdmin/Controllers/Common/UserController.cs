using Autofac;
using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace MesAdmin.Controllers.Common
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IDbOperate<mes_user_entity> _userservice;
        private IUser _user;
        public UserController(IDbOperate<mes_user_entity> userservice,IUser user)
        {
            _userservice = userservice;
            _user = user;
        }
        [HttpPost,CheckData,Route("add")]
        public IHttpActionResult Add(List<mes_user_entity> entitys)
        {
            try
            {
                IEnumerable<mes_user_entity> noklist = new List<mes_user_entity>();
                var errtel = entitys.Where(t => !ZDToolHelper.Tool.CheckTelNumber(t.tel));
                if (errtel.Count() > 0)
                {
                    string temp = string.Empty;
                    errtel.ToList().ForEach(t => temp = temp + t.tel+",");
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = $"手机号{temp}不正确"
                    });
                }
                else
                {
                    var ret = _userservice.Add(entitys, out noklist);
                    if (ret > 0)
                    {
                        if (noklist.Count() == 0)
                        {
                            return Json(new sys_result()
                            {
                                code = 1,
                                msg = "数据保存成功"
                            });
                        }
                        else
                        {
                            return Json(new
                            {
                                code = 2,
                                msg = "数据保存失败",
                                noklist = noklist
                            });
                        }
                    }
                    else
                    {
                        return Json(new sys_result()
                        {
                            code = 0,
                            msg = "数据保存失败"
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("del")]
        public IHttpActionResult Del(List<mes_user_entity> entitys)
        {
            try
            {
                var ret = _userservice.Del(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据删除成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据删除失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,CheckData, Route("edit")]
        public IHttpActionResult Edit(List<mes_user_entity> entitys)
        {
            try
            {
                List<mes_user_entity> retlist = new List<mes_user_entity>();
                foreach (var item in entitys)
                {
                    List<Int32> roleids = new List<int>();
                    item.role.ForEach(i => roleids.Add(Convert.ToInt32(i)));
                    var isok = _userservice.Modify(item);
                    if (isok)
                    {
                       var ok =  _user.Save_User_Roles(item.id, roleids);
                        if (ok)
                        {
                            retlist.Add(item);
                        }
                    }
                }
                if (retlist.Count == entitys.Count)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据修改成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据修改失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                parm.default_order_colname = "addtime";
                var list = _userservice.GetList(parm, out resultcount);
                return Json(new sys_search_result()
                {
                    code = 1,
                    msg = "ok",
                    resultcount = resultcount,
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("resetpwd")]
        public IHttpActionResult ResetPwd(sys_changpwd_form form)
        {
            try
            {
               var b = _user.ResetPwd(form.id, form.pass);
                if (b)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "重置密码成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "重置密码失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("changpwd")]
        public IHttpActionResult User_ChangePwd(sys_changpwd_form form)
        {
            try
            {
                var token = ZDToolHelper.TokenHelper.GetToken;
                var ret = _user.ChangePwd(token, form.pass);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "密码修改成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "密码修改失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("resettoken")]
        public IHttpActionResult ResetToken(mes_user_entity entity)
        {
            try
            {
                var ret = _user.ReSetToken(entity.id);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "重置Token成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "重置Token失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}