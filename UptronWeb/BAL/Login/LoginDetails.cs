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
        public bool CheckAdminLogin(string UserName, string Password)
        {
            _db = new UptronWebEntities();
            var result = _db.UserMasters.Where(x => x.UserName == UserName && x.Password == Password && x.IsActive == true).FirstOrDefault();
            if (result != null)
                return true;
            else
                return false;
        }
    }
}