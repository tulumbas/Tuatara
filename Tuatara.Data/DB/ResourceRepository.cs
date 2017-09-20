using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.DB
{
    public class ResourceRepository : EFRepositoryBase<AssignableResource>, IResourceRepository
    {
    }
}
