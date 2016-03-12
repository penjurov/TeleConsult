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
    using Data.Models.Enumerations;
    using Data.Proxies;
    using Data.Repositories;
    using Web.Models;

    public class SpecialityModel : AdminModel, IModel
    {
        public SpecialityProxy ViewModel { get; set; }

        public List<SpecialityProxy> GetSpecialities(AdminFilter filter)
        {
            return this.RepoFactory.Get<SpecialityRepository>().Get(filter).ToList();
        }

        public int Save(SpecialityProxy proxy, ModelStateDictionary modelState)
        {
            if (proxy != null && modelState.IsValid)
            {
                try
                {
                    var repo = this.RepoFactory.Get<SpecialityRepository>();
                    Speciality speciality;

                    var existing = repo.SpecialityExist(proxy.Name, proxy.Id);

                    if (existing)
                    {
                        throw new Exception(GlobalConstants.Errors.SpecialityExist);
                    }

                    if (proxy.Id.HasValue)
                    {
                        speciality = repo.GetById(proxy.Id);
                    }
                    else
                    {
                        speciality = new Speciality();
                        repo.Add(speciality);
                    }

                    speciality.Name = proxy.Name;

                    repo.SaveChanges();

                    this.Logger.Log(proxy.Id.HasValue ? ActionType.EditSpecialty : ActionType.AddSpecialty, speciality.Name);

                    return speciality.Id;
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
            var repo = this.RepoFactory.Get<SpecialityRepository>();
            var speciality = repo.GetById(id);

            if (speciality != null)
            {
                speciality.IsDeleted = true;
                speciality.DeletedOn = DateTime.Now;
                repo.SaveChanges();

                this.Logger.Log(ActionType.DeleteSpecialty, speciality.Name);
            }
            else
            {
                throw new Exception("Специалност с такова Id не съществува");
            }
        }

        public void Activate(int id)
        {
            var repo = this.RepoFactory.Get<SpecialityRepository>();
            var speciality = repo.GetById(id);

            if (speciality != null)
            {
                speciality.IsDeleted = false;
                speciality.DeletedOn = null;
                repo.SaveChanges();

                this.Logger.Log(ActionType.ActivateSpeciality, speciality.Name);
            }
            else
            {
                throw new Exception("Специалност с такова Id не съществува");
            }
        }
    }
}