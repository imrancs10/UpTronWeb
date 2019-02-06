using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using UptronWeb.Global;
using System.Data.Entity;

namespace UptronWeb.BAL.Login
{
    public class LoginDetails
    {
        UptronWebEntities _db = null;

        /// <summary>
        /// Get Authenticate User credentials
        /// </summary>
        /// <param name="UserName">Username</param>
        /// <param name="Password">Password</param>
        /// <returns>Enums</returns>
        public UserMaster CheckAdminLogin(string UserName, string Password)
        {
            _db = new UptronWebEntities();
            var result = _db.UserMasters.Include("RoleMaster").Where(x => x.UserName == UserName && x.Password == Password && x.IsActive == true).FirstOrDefault();
            if (result != null)
                return result;
            else
                return null;
        }
        public bool ResetPassword(string txtoldpassowrd, string txtnewpassword, int userId)
        {
            _db = new UptronWebEntities();
            var result = _db.UserMasters.Where(x => x.Id == userId && x.Password == txtoldpassowrd).FirstOrDefault();
            if (result != null)
            {
                result.Password = txtnewpassword;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}