namespace TeleConsult.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Web.Mvc;

    using Common;
    using Common.Helpers;
    using Data;
    using Data.Filters.Admin;
    using Data.Models;
    using Data.Models.Enumerations;
    using Data.Proxies;
    using Data.Repositories;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TeleConsult.Web.Areas.Admin.Models.Base;
    using Web.Models;

    public class SpecialistModel : AdminModel, IModel<bool>
    {
        public SpecialistProxy ViewModel { get; set; }

        public IEnumerable<SelectListItem> Specialities { get; set; }

        public IEnumerable<SelectListItem> Hospitals { get; set; }

        public IEnumerable<SelectListItem> Titles { get; set; }

        public override void Init(bool init)
        {
            base.Init(init);

            this.Specialities = this.RepoFactory.Get<SpecialityRepository>().GetActive()
                        .Select(s => new SelectListItem
                        {
                            Value = s.Id.ToString(),
                            Text = s.Name
                        });

            this.Hospitals = this.RepoFactory.Get<HospitalRepository>().GetActive()
                        .Select(s => new SelectListItem
                        {
                            Value = s.Id.ToString(),
                            Text = s.Name
                        });

            this.Titles = Enum.GetValues(typeof(Title)).Cast<Title>()
                        .Select(v => new SelectListItem
                        {
                            Text = v.GetDescription(),
                            Value = ((int)v).ToString()
                        });
        }

        public List<SpecialistProxy> GetSpecialists(SpecialistFilter filter)
        {
            return this.RepoFactory.Get<SpecialistRepository>().Get(filter).ToList();
        }

        public string Save(SpecialistProxy proxy, ModelStateDictionary modelState)
        {
            if (proxy != null && modelState.IsValid)
            {
                try
                {
                    var repo = this.RepoFactory.Get<SpecialistRepository>();
                    Specialist specialist;

                    var existing = repo.SpecialistExist(proxy.Uin, proxy.Id);

                    if (existing)
                    {
                        throw new Exception(GlobalConstants.Errors.SpecialistExist);
                    }

                    if (string.IsNullOrEmpty(proxy.Id))
                    {
                        var context = new TeleConsultDbContext();
                        var store = new UserStore<User>(context);
                        var manager = new UserManager<User>(store);

                        specialist = new Specialist
                        {
                            Title = proxy.Title,
                            UserName = proxy.UserName,
                            FirstName = proxy.FirstName,
                            LastName = proxy.LastName,
                            Uin = proxy.Uin,
                            PhoneNumber = proxy.PhoneNumber,
                            Email = proxy.Email,
                            HospitalId = proxy.HospitalId,
                            SpecialityId = proxy.SpecialityId
                        };

                        manager.Create(specialist, proxy.Password);
                        manager.AddToRole(specialist.Id, GlobalConstants.SpecialistRoleName);
                    }
                    else
                    {
                        specialist = repo.GetById(proxy.Id);

                        specialist.Title = proxy.Title;
                        specialist.FirstName = proxy.FirstName;
                        specialist.LastName = proxy.LastName;
                        specialist.Uin = proxy.Uin;
                        specialist.PhoneNumber = proxy.PhoneNumber;
                        specialist.Email = proxy.Email;
                        specialist.HospitalId = proxy.HospitalId;
                        specialist.SpecialityId = proxy.SpecialityId;

                        repo.SaveChanges();
                    }

                    return specialist.Id;
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

        public void Delete(string id)
        {
            var repo = this.RepoFactory.Get<SpecialistRepository>();
            var hospital = repo.GetById(id);

            if (hospital != null)
            {
                hospital.IsDeleted = true;
                hospital.DeletedOn = DateTime.Now;
                repo.SaveChanges();
            }
            else
            {
                throw new Exception("Специалист с такова Id не съществува");
            }
        }

        public void Activate(string id)
        {
            var repo = this.RepoFactory.Get<SpecialistRepository>();
            var hospital = repo.GetById(id);

            if (hospital != null)
            {
                hospital.IsDeleted = false;
                hospital.DeletedOn = null;
                repo.SaveChanges();
            }
            else
            {
                throw new Exception("Специалист с такова Id не съществува");
            }
        }
    }
}