using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Tuatara.Services.BL;
using Tuatara.Services;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;
using System.Linq.Expressions;

namespace Tuatara.Playground
{
    class PlayWithServices
    {
        private static int GetID<T>(IUnitOfWork unit, string name) where T : class, IBaseEntity
        {
            var repo = unit.Repository<T>();
            var par1 = Expression.Parameter(typeof(T), "x");
            var par2 = Expression.Constant(name);
            var check = (Expression<Func<T, bool>>)Expression.Lambda(
                Expression.Equal(
                    Expression.Property(par1, "Name"), 
                    par2), 
                par1);

            var result = repo.FirstOrDefault(check);
            return result?.ID ?? 0;            
        }

        public static void Run()
        {
            var service = UnityConfig.GetConfiguredContainer().Resolve<AssignmentService>();
            var statuses = UnityConfig.GetConfiguredContainer().Resolve<StatusService>();            

            var playbook = service.GetPlaybookForWeekShift(0);
            var newRow = new PlaybookRow
            {
                Description = "This is a description",
                Duration = 0.25,
                IntraweekID = GetID<IntraweekEntity>(service.UnitOfWork, "Tue"),
                PriorityID = GetID<PriorityEntity>(service.UnitOfWork, "BAU"),
                WhatID = 5,
                ResourceID = 2,
                StatusID = statuses.Booked.ID,
            };
            service.CreateAssignment(playbook.WeekID, newRow, 1);
        }
    }
}
