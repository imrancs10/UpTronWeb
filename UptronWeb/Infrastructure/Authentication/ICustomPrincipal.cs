using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace UptronWeb.Infrastructure.Authentication
{
    public interface ICustomPrincipal : IPrincipal
    {
        int Id { get; set; }
        string EmailId { get; set; }
        string MobileNo { get; set; }
        string Name { get; set; }

    }
}
