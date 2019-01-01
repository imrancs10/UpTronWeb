using DataLayer;
using UptronWeb.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace UptronWeb.Infrastructure.Utility
{
    public class EmailHelper
    {
        public static string GetContactUsEmail(string Name, string Email, string Phone, string Message)
        {
            string body = "Hi Admin<br/><br/>";
            body += Name + " has request through Contact Us Page, details are as below.<br/>";
            body += "<br/><b>Name</b>" + Name + "<br/><br/>";
            body += "<br/><b>Email</b>" + Email + "<br/><br/>";
            body += "<br/><b>Phone</b>" + Phone + "<br/><br/>";
            body += "<br/><b>Message</b>" + Message + "<br/><br/>";
            body += "Thank You,<br/>";
            body += "";
            return body;
        }
        public static string GetJobRegistrationEmail(string Name, string Email)
        {
            string defaultPassword = ConfigurationManager.AppSettings["JobRegistrationDefaultPassword"].ToString();
            string body = "Hi " + Name + "<br/><br/>";
            body += Name + "Your registration is successful. Here is your Login detail<br/>";
            body += "<br/><b>User Name</b>" + Email + "<br/><br/>";
            body += "<br/><b>Password</b>" + defaultPassword + "<br/><br/>";
            body += "Thank You,<br/>";
            body += "";
            return body;
        }
    }
}