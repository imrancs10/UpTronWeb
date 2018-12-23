using DataLayer;
using System.Data.Entity;
using System.Linq;
using UptronWeb.Global;

namespace UptronWeb.BAL.Master
{
    public class MasterBAL
    {
        private UptronWebEntities _db = null;
        public Enums.CrudStatus SaveGOCircluar(GOCircular goCircular)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            GOCircular _deptRow = _db.GOCirculars.Where(x => x.GONumber == goCircular.GONumber).FirstOrDefault();
            if (_deptRow == null)
            {
                GOCircular circular = new GOCircular()
                {
                    GODate = goCircular.GODate,
                    GOFile = goCircular.GOFile,
                    GONumber = goCircular.GONumber,
                    Subject = goCircular.Subject
                };

                _db.Entry(circular).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
            {
                return Enums.CrudStatus.DataAlreadyExist;
            }
        }

        public Enums.CrudStatus SaveTender(Tender tender)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            Tender _deptRow = _db.Tenders.Where(x => x.TenderNumber == tender.TenderNumber).FirstOrDefault();
            if (_deptRow == null)
            {
                Tender tenderlist = new Tender()
                {
                    TenderNumber = tender.TenderNumber,
                    TenderDate = tender.TenderDate,
                    TenderFile = tender.TenderFile,
                    Subject = tender.Subject
                };
                _db.Entry(tenderlist).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
            {
                return Enums.CrudStatus.DataAlreadyExist;
            }
        }
    }
}