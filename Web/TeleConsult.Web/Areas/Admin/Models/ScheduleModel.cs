namespace TeleConsult.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Web.Mvc;

    using Base;
    using Data.Filters.Admin;
    using Data.Models;
    using Data.Models.Enumerations;
    using Data.Repositories;
    using TeleConsult.Data.Proxies;
    using Web.Models;

    public class ScheduleModel : AdminModel, IModel<bool>
    {
        public IEnumerable<SelectListItem> Specialists { get; set; }

        public ScheduleProxy ViewModel { get; set; }

        public override void Init(bool init)
        {
            base.Init(init);

            if (init)
            {
                this.Specialists = this.RepoFactory.Get<SpecialistRepository>().GetActive()
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.FullName
                    });
            }
        }

        public List<ScheduleProxy> GetSchedules(SchedulesFilter filter)
        {
            return this.RepoFactory.Get<ScheduleRepository>().Get(filter).ToList();
        }

        public int Save(ScheduleProxy proxy, ModelStateDictionary modelState)
        {
            if (proxy != null && modelState.IsValid)
            {
                try
                {
                    var repo = this.RepoFactory.Get<ScheduleRepository>();
                    Schedule schedule;

                    if (!proxy.Id.HasValue)
                    {
                        schedule = new Schedule();
                        repo.Add(schedule);
                    }
                    else
                    {
                        schedule = repo.GetById(proxy.Id);
                    }

                    schedule.Description = proxy.Description;
                    schedule.StartDate = proxy.StartDate.Value;
                    schedule.EndDate = proxy.EndDate.Value;
                    schedule.SpecialistId = proxy.SpecialistId;
                    schedule.IsAllDay = proxy.IsAllDay;

                    repo.SaveChanges();

                    this.Logger.Log(proxy.Id.HasValue ? ActionType.EditSchedule : ActionType.AddSchedule, string.Format("{0}: {1} - {2}", schedule.Specialist.UserName, schedule.StartDate, schedule.EndDate));

                    return schedule.Id;
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
    }
}