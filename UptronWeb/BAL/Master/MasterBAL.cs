﻿using DataLayer;
using System;
using System.Collections.Generic;
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
            if (goCircular.Id > 0)
            {
                _db.Entry(goCircular).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
            {
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

        }
        public List<GOCircular> GetAllCircularList()
        {
            _db = new UptronWebEntities();
            var result = _db.GOCirculars.ToList();
            return result;
        }
        public bool DeleteGOCircular(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.GOCirculars.FirstOrDefault(x => x.Id == Id);
            _db.GOCirculars.Remove(result);
            _db.SaveChanges();
            return true;
        }
        public GOCircular GetGOCircular(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.GOCirculars.FirstOrDefault(x => x.Id == Id);
            return result;
        }
        public Enums.CrudStatus SaveTender(Tender tender)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (tender.id > 0)
            {
                var _deptRow = _db.Tenders.Where(x => x.id == tender.id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.id = tender.id;
                    _deptRow.Subject = tender.Subject;
                    _deptRow.TenderDate= tender.TenderDate;
                    _deptRow.TenderNumber = tender.TenderNumber;
                    if (tender.TenderFile != null)
                        _deptRow.TenderFile = tender.TenderFile;

                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                _effectRow = 0;
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
        public List<Tender> GetAllTenderViewList()
        {
            _db = new UptronWebEntities();
            var result = _db.Tenders.OrderBy(x => x.TenderDate).ToList();
            return result;
        }
        public Tender GetTender(int id)
        {
            _db = new UptronWebEntities();
            var result = _db.Tenders.FirstOrDefault(x => x.id == id);
            return result;
        }
        public Enums.CrudStatus SaveGalleryMaster(GalleryMaster gallery)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            var _deptRow = _db.GalleryMasters.Where(x => x.GalleryName == gallery.GalleryName).FirstOrDefault();
            if (_deptRow == null)
            {
                _db.Entry(gallery).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
            {
                return Enums.CrudStatus.DataAlreadyExist;
            }
        }
        public List<GalleryMaster> GetAllGalleryName()
        {
            _db = new UptronWebEntities();
            var result = _db.GalleryMasters.ToList();
            return result;
        }
        public Enums.CrudStatus SaveGalleryPhotoMaster(GalleryPhotoMaster galleryPhoto)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            var _deptRow = _db.GalleryPhotoMasters.Where(x => x.PhotoName == galleryPhoto.PhotoName).FirstOrDefault();
            if (_deptRow == null)
            {
                _db.Entry(galleryPhoto).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
            {
                return Enums.CrudStatus.DataAlreadyExist;
            }
        }
        public List<GalleryPhotoMaster> GetAllGalleryPhotoByGalleryId(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.GalleryPhotoMasters.Where(x => x.GalleryId == Id).ToList();
            return result;
        }
        public Enums.CrudStatus SaveNewsAndUpdate(NewsUpdateMaster newsAndUpdate)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (newsAndUpdate.Id > 0)
            {
                var _deptRow = _db.NewsUpdateMasters.Where(x => x.Id == newsAndUpdate.Id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.Id = newsAndUpdate.Id;
                    _deptRow.IsActive = newsAndUpdate.IsActive;
                    _deptRow.IsNew = newsAndUpdate.IsNew;
                    _deptRow.ModifiedDate = newsAndUpdate.ModifiedDate;
                    _deptRow.Title = newsAndUpdate.Title;
                    if (newsAndUpdate.NewsFile != null)
                        _deptRow.NewsFile = newsAndUpdate.NewsFile;

                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.NewsUpdateMasters.Where(x => x.Title == newsAndUpdate.Title && x.IsActive == true).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(newsAndUpdate).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }
        public List<NewsUpdateMaster> GetAllNewsUpdate()
        {
            _db = new UptronWebEntities();
            var result = _db.NewsUpdateMasters.ToList();
            return result;
        }
        public bool DeleteNewsAndUpdate(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.NewsUpdateMasters.FirstOrDefault(x => x.Id == Id);
            _db.NewsUpdateMasters.Remove(result);
            _db.SaveChanges();
            return true;
        }
        public List<NewsUpdateMaster> GetAllActiveNewsUpdate()
        {
            _db = new UptronWebEntities();
            var result = _db.NewsUpdateMasters.Where(x => x.IsActive == true).ToList();
            return result;
        }
        public NewsUpdateMaster GetNewsUpdateById(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.NewsUpdateMasters.FirstOrDefault(x => x.Id == Id);
            return result;
        }
        

        public Enums.CrudStatus SaveEventsUpcoming(UpcomingEventsMaster upcomingEvents)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (upcomingEvents.Id > 0)
            {
                var _deptRow = _db.UpcomingEventsMasters.Where(x => x.Id == upcomingEvents.Id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.Id = upcomingEvents.Id;
                    _deptRow.IsActive = upcomingEvents.IsActive;
                    _deptRow.IsNew = upcomingEvents.IsNew;
                    _deptRow.ModifiedDate = upcomingEvents.ModifiedDate;
                    _deptRow.Title = upcomingEvents.Title;
                    if (upcomingEvents.EventsFile != null)
                        _deptRow.EventsFile = upcomingEvents.EventsFile;

                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.UpcomingEventsMasters.Where(x => x.Title == upcomingEvents.Title && x.IsActive == true).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(upcomingEvents).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }

        public List<UpcomingEventsMaster> GetAllUpcomingEvents()
        {
            _db = new UptronWebEntities();
            var result = _db.UpcomingEventsMasters.ToList();
            return result;
        }

        public List<UpcomingEventsMaster> GetAllActiveUpcomingEvents()
        {
            _db = new UptronWebEntities();
            var result = _db.UpcomingEventsMasters.Where(x=>x.IsActive == true).ToList();
            return result;
        }

        public UpcomingEventsMaster GetAllUpcomingEventsById(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.UpcomingEventsMasters.FirstOrDefault(x => x.Id == Id);
            return result;
        }

        public bool DeleteUpcomingEvents(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.UpcomingEventsMasters.FirstOrDefault(x => x.Id == Id);
            _db.UpcomingEventsMasters.Remove(result);
            _db.SaveChanges();
            return true;
        }
        public Enums.CrudStatus SaveState(State state)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (state.StateId > 0)
            {
                var _deptRow = _db.States.Where(x => x.StateId == state.StateId).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.StateId = state.StateId;
                    _deptRow.StateName = state.StateName;
                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.States.Where(x => x.StateName == state.StateName).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(state).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }
        public List<State> GetAllStates()
        {
            _db = new UptronWebEntities();
            var result = _db.States.ToList();
            return result;
        }
        public bool DeleteState(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.States.FirstOrDefault(x => x.StateId == Id);
            _db.States.Remove(result);
            _db.SaveChanges();
            return true;
        }

        public Enums.CrudStatus SaveReligion(Religion religion)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (religion.ReligionId > 0)
            {
                var _deptRow = _db.Religions.Where(x => x.ReligionId == religion.ReligionId).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.ReligionId = religion.ReligionId;
                    _deptRow.ReligionName = religion.ReligionName;
                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.Religions.Where(x => x.ReligionName == religion.ReligionName).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(religion).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }
        public List<Religion> GetAllReligion()
        {
            _db = new UptronWebEntities();
            var result = _db.Religions.ToList();
            return result;
        }
        public bool DeleteReligion(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.Religions.FirstOrDefault(x => x.ReligionId == Id);
            _db.Religions.Remove(result);
            _db.SaveChanges();
            return true;
        }

        public Enums.CrudStatus SaveGender(Gender gender)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (gender.GenderId > 0)
            {
                var _deptRow = _db.Genders.Where(x => x.GenderId == gender.GenderId).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.GenderId = gender.GenderId;
                    _deptRow.GenderName = gender.GenderName;
                    _deptRow.IsActive = gender.IsActive;
                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.Genders.Where(x => x.GenderName == gender.GenderName).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(gender).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }

        public List<Gender> GetAllGender()
        {
            _db = new UptronWebEntities();
            var result = _db.Genders.ToList();
            return result;
        }
        public bool DeleteGender(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.Genders.FirstOrDefault(x => x.GenderId == Id);
            _db.Genders.Remove(result);
            _db.SaveChanges();
            return true;
        }

    }
}