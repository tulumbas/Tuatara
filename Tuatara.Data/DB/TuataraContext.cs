using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tuatara.Data.Models;

namespace Tuatara.Data.DB
{
    public class TuataraContext : DbContext
    {
        public IDbSet<CalendarItem> CalendarItems { get; private set; }
        public IDbSet<Work> Works { get; private set; }
        public IDbSet<AssignableResource> Resources { get; private set; }
        public IDbSet<TuataraUser> Users { get; private set; }
        public IDbSet<Assignment> Assignments { get; private set; }

        public TuataraContext()
            : base("TuataraContext")
        {
            CalendarItems = Set<CalendarItem>();
            Works = Set<Work>();
            Resources = Set<AssignableResource>();
            Users = Set<TuataraUser>();
            Assignments = Set<Assignment>();
        }
    }
}