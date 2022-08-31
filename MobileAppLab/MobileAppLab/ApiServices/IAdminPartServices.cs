using CommonClass.Models;
using CommonClass.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileAppLab.ApiServices
{
    public interface IAdminPartServices
    {
        Task<ServerRespone> CreateNew(AdminParts entity);
        Task<ServerRespone> Delete(object key);
        Task<IEnumerable<AdminParts>> GetAll(int skip = 0, int take = 0, bool isforceRefresh = false);
        Task<IReadOnlyDictionary<int, string>> GetAllAsDictionary();
        Task<AdminParts> GetByID(object key);
        Task<ServerRespone> Update(AdminParts entity);
    }
}