using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Data.Models;

namespace Tuatara.Data.Repositories
{
    public interface ICalendarItemRepository: IRepository<CalendarItem>
    {
        CalendarItem GetItemByDate(DateTime dt);
    }

    public interface IAssignmentRepository : IRepository<Assignment>
    {

    }

    public interface IProjectClientRepository : IRepository<Work>
    {

    }

    public interface IResourceRepository : IRepository<AssignableResource>
    {
    }

    public interface IUserRepository : IRepository<TuataraUser>
    {

    }
}
