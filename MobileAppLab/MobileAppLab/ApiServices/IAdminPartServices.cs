using CommonClass.Models;
using CommonClass.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileAppLab.ApiServices
{
    public interface IAdminPartServices : IService<AdminParts>
    {
        Task<IReadOnlyDictionary<int, string>> GetAllAsDictionary();
    }
}