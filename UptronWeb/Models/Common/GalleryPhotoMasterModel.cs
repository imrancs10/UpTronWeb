using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.Common
{
    public class GalleryPhotoMasterModel
    {
        public int Id { get; set; }
        public int GalleryId { get; set; }
        public string PhotoName { get; set; }
        public string Photo { get; set; }

    }
}