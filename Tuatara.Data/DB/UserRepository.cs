using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.DB
{
    public class UserRepository: EFRepositoryBase<TuataraUser>, IUserRepository
    {

    }
}