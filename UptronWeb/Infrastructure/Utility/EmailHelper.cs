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
            body += "<br/><b>Name:&nbsp;&nbsp;</b>" + Name + "<br/><br/>";
            body += "<br/><b>Email:&nbsp;&nbsp;</b>" + Email + "<br/><br/>";
            body += "<br/><b>Phone:&nbsp;&nbsp;</b>" + Phone + "<br/><br/>";
            body += "<br/><b>Message:&nbsp;&nbsp;</b>" + Message + "<br/><br/>";
            body += "Thank You,<br/>";
            body += "";
            return body;
        }
        public static string GetJobRegistrationEmail(string Name, string Email,string password)
        {
           
            string body = "Hi " + Name + "<br/><br/>";
            body += Name + "Your registration is successful. Here is your Login detail<br/>";
            body += "<br/><b>User Name:&nbsp;&nbsp;</b>" + Email + "<br/><br/>";
            body += "<br/><b>Password:&nbsp;&nbsp;</b>" + password + "<br/><br/>";
            body += "<br/><b>Your Profile will be activate after verification.<br/><br/>";
            body += "<br/><b>We will send you separate mail once your profile will be activated.<br/><br/>";
            body += "Thank You,<br/>";
            body += "";
            return body;
        }
        public static string GetJobRegistrationActiveEmail(string Name, string Email)
        {
            string body = "Hi " + Name + "<br/><br/>";
            body += Name + "Your profile is now Active.<br/>";
            body += "<br/><b>You can now login to your account.<br/><br/>";
            body += "Thank You,<br/>";
            body += "";
            return body;
        }

        public static string GetQuickEnquiryEmail(string Name, string Email, string Mobile, string EnquiryMessage)
        {
            string body = "Hi Admin<br/><br/>";
            body += Name + " has been request through Quick Enquiry, details are as below.<br/>";
            body += "<br/><b>Name:&nbsp;&nbsp;</b>" + Name + "<br/><br/>";
            body += "<br/><b>Email:&nbsp;&nbsp;</b>" + Email + "<br/><br/>";
            body += "<br/><b>Phone:&nbsp;&nbsp;</b>" + Mobile + "<br/><br/>";
            body += "<br/><b>Message:&nbsp;&nbsp;</b>" + EnquiryMessage + "<br/><br/>";
            body += "Thank You,<br/>";
            body += "";
            return body;
        }

        public static string GetEmployeeResignationEmail(JobResignation vendor)
        {
            string body = "Hi Admin<br/><br/>";
            body += vendor.JobRegistration.Name + " has been request through Employee Resignation Page for Resignation for this Job, details are as below.<br/>";
            body += "<br/><b>Name:&nbsp;&nbsp;</b>" + vendor.JobRegistration.Name + "<br/><br/>";
            body += "<br/><b>Vendor Name:&nbsp;&nbsp;</b>" + vendor.VendorDetail.VendorName + "<br/><br/>";
            body += "<br/><b>Resignation Date:&nbsp;&nbsp;</b>" + vendor.ResignationDate.Value.ToString("dd/MM/yyy") + "<br/><br/>";
            body += "<br/><b>Resignation Reason:&nbsp;&nbsp;</b>" + vendor.ResignationReason + "<br/><br/>";
            body += "Thank You,<br/>";
            body += "";
            return body;
        }
        public static string GetVendorVerificationEmail(string name, string verificationCode)
        {
            string body = string.Format("Hi {0}<br/><br/>", name);
            body += "As you requested, here is a OTP is : <b>" + verificationCode + "</b> you can use to verify your detail.<br/><br/>";
            body += "Thank You,<br/>";
            return body;
        }
    }
}