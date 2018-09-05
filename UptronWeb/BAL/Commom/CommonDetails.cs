using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using UptronWeb.Models.Common;

namespace UptronWeb.BAL.Commom
{
    public class CommonDetails
    {
        UptronWebEntities _db = null;

        public List<DayModel> DaysList()
        {
            _db = new UptronWebEntities();
            var _list = (from day in _db.DayMasters
                         select new DayModel
                         {
                             DayId = day.DayId,
                             DayName = day.DayName
                         }).ToList();
            return _list != null ? _list : new List<DayModel>();
        }
    }
}