using System.Web.Mvc;
using UptronWeb.BAL;
using UptronWeb.Infrastructure.Authentication;

namespace UptronWeb.Infrastructure.Utility
{
    public abstract class BaseViewPage : WebViewPage
    {

    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
        public virtual string GetEmployementDetail()
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var result = detail.GetJobPortalRegistrationById(User.Id);
            return result.EmployementStatus;
        }
    }
}