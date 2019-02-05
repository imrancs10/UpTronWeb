using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Infrastructure.Authentication
{
    public class CustomPrincipalSerializeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
    }
}