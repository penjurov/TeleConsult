namespace TeleConsult.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Web.Mvc;

    using Base;
    using Common;
    using Data.Filters.Admin;
    using Data.Models;
    using Data.Proxies;
    using Data.Repositories;
    using Web.Models;

    public class HospitalModel : AdminModel, IModel<bool>
    {
        public HospitalProxy ViewModel { get; set; }

        public List<HospitalProxy> GetHospitals(AdminFilter filter)
        {
            return this.RepoFactory.Get<HospitalRepository>().Get(filter).ToList();
        }

        public int Save(HospitalProxy proxy, ModelStateDictionary modelState)
        {
            if (proxy != null && modelState.IsValid)
            {
                try
                {
                    var repo = this.RepoFactory.Get<HospitalRepository>();
                    Hospital hospital;

                    var existing = repo.HospitalExist(proxy.Name, proxy.Id);

                    if (existing)
                    {
                        throw new Exception(GlobalConstants.Errors.HospitalExist);
                    }

                    if (proxy.Id.HasValue)
                    {
                        hospital = repo.GetById(proxy.Id);
                    }
                    else
                    {
                        hospital = new Hospital();
                        repo.Add(hospital);
                    }

                    hospital.Name = proxy.Name;
                    hospital.Address = proxy.Address;
                    hospital.Phone = proxy.Phone;

                    if (proxy.Latitude.HasValue)
                    {
                        hospital.Latitude = proxy.Latitude.Value;
                    }

                    if (proxy.Longitude.HasValue)
                    {
                        hospital.Longitude = proxy.Longitude.Value;
                    }

                    repo.SaveChanges();

                    return hospital.Id;
                }
                catch (DbEntityValidationException e)
                {
                    throw new Exception(this.HandleDbEntityValidationException(e));
                }
            }
            else
            {
                throw new Exception(this.HandleErrors(modelState));
            }
        }

        public void Delete(int id)
        {
            var repo = this.RepoFactory.Get<HospitalRepository>();
            var hospital = repo.GetById(id);

            if (hospital != null)
            {
                hospital.IsDeleted = true;
                hospital.DeletedOn = DateTime.Now;
                repo.SaveChanges();
            }
            else
            {
                throw new Exception("Лечебно заведение с такова Id не съществува");
            }
        }

        public void Activate(int id)
        {
            var repo = this.RepoFactory.Get<HospitalRepository>();
            var hospital = repo.GetById(id);

            if (hospital != null)
            {
                hospital.IsDeleted = false;
                hospital.DeletedOn = null;
                repo.SaveChanges();
            }
            else
            {
                throw new Exception("Лечебно заведение с такова Id не съществува");
            }
        }
    }
}