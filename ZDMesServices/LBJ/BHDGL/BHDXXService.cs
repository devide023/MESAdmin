﻿using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using ZDMesInterfaces.LBJ.BHDGL;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.BHDGL
{
    public class BHDXXService:BaseDao<base_bhdxx>,IBHD
    {
        public BHDXXService(string constr) :base(constr)
        {

        }

        public int Get_Max_BHD()
        {
            using (var db = new OracleConnection(ConString))
            {
                
                    return db.ExecuteScalar<int>("select count(gcdm) from BASE_BHDXX");
                
            }
        }

        public bool IsExistBhdNo(string bhdno)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {

                    return db.ExecuteScalar<int>("select count(gcdm) from BASE_BHDXX where bhdbh = :bhdbh", new { bhdbh = bhdno }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
