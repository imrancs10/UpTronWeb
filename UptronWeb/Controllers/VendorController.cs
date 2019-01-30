using System.Web.Mvc;
using UptronWeb.BAL.Vendor;
using UptronWeb.Global;
using DataLayer;
using System;
using UptronWeb.Infrastructure.Utility;

namespace UptronWeb.Controllers
{
    public class VendorController : CommonController
    {
        // GET: Vendor
        public ActionResult VendorEntry()
        {
            VendorDetailBAL bal = new VendorDetailBAL();
            string vendorCodeActual = string.Empty;
            while (true)
            {
                string vendorCode = VerificationCodeGeneration.GetVendorCode();
                var vendor = bal.GetVendorDetailByVendorCode(vendorCode);
                if (vendor == null)
                {
                    vendorCodeActual = vendorCode;
                    break;
                }
            }
            ViewData["VendorCode"] = vendorCodeActual;
            TempData["VendorCode"] = vendorCodeActual;
            return View();
        }
        [HttpPost]
        public ActionResult VendorEntry(string txtVendorName, string txtContactPerson, string txtPhone, string txtTypeofFirm, string txtTypeofBusiness, string txtGSTNo, string txtTypeofStream, string txtAddress)
        {
            VendorDetailBAL bal = new VendorDetailBAL();
            VendorDetail vendor = new VendorDetail()
            {
                ContactPerson = txtContactPerson,
                CreatedDate = DateTime.Now,
                FullAddress = txtAddress,
                GSTNumber = txtGSTNo,
                IsActive = true,
                Permitted = false,
                PhoneNumber = txtPhone,
                TypeOfBusiness = txtTypeofBusiness,
                TypeofFirm = txtTypeofBusiness,
                TypeofStream = txtTypeofStream,
                VendorName = txtVendorName,
                VendorCode = Convert.ToString(TempData["VendorCode"])
            };
            var result = bal.SaveVendorDetail(vendor);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Vendor has been registered", "Vendor");
            }
            else
            {
                SetAlertMessage("vendor is not registered", "Vendor");
            }
            return RedirectToAction("VendorEntry");
        }
        public ActionResult VendorViewDetails()
        {
            VendorDetailBAL bal = new VendorDetailBAL();
            var result = bal.GetAllVendorDetails();
            return View(result);
        }
        public ActionResult VendorStatusChange(int Id)
        {
            VendorDetailBAL bal = new VendorDetailBAL();
            var result = bal.SetStatusVendorDetails(Id);
            if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Vendor status has been updated", "Vendor");
            }
            else
            {
                SetAlertMessage("Vendor status has not been updated", "Vendor");
            }
            return RedirectToAction("VendorViewDetails");
        }
        
    }
}