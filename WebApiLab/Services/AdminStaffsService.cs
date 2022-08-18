using CommonClass.ErrorCodes;
using CommonClass.Models;
using CommonClass.Validations;
using System.Net;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;

namespace WebApiLab.Services
{

    /// <summary>CRUD trên bảng Admin.Staffs</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    public class AdminStaffsService : IBackendService<AdminStaff>
    {
        private LabDbContext _labDbContext;
        private IServiceProvider _serviceProvider;
        private AdminStaffValidator _validationRules;
        public AdminStaffsService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._labDbContext = serviceProvider.GetService<LabDbContext>();
            _validationRules = new AdminStaffValidator();
        }
        public Task<HttpResponseMessage> Create(AdminStaff data)
        {
            bool isDuplicateEmail = this._labDbContext.AdminStaffs.Any(staff => staff.Email == data.Email);
            bool isDuplicatePhoneNumber = this._labDbContext.AdminStaffs.Any(staff => staff.PhoneNumber == data.PhoneNumber);
            if (isDuplicateEmail)
            {
                var emailRespone = new HttpResponseMessage(HttpStatusCode.BadRequest);
                emailRespone.ReasonPhrase = AdminStaffErrorCode.DUPLICATE_EMAIL;
                return Task.FromResult(emailRespone);
            }
            if (isDuplicatePhoneNumber)
            {
                var phoneNumberRespone = new HttpResponseMessage(HttpStatusCode.BadRequest);
                phoneNumberRespone.ReasonPhrase = AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER;
                return Task.FromResult(phoneNumberRespone);
            }
            if (this._labDbContext?.AdminStaffs.AddIfNotExists(data, db => db.ID == data.ID) != null)
            {
                this._labDbContext?.SaveChanges();
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Created));
            }
            else
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotModified));


        }

        public Task<HttpResponseMessage> Delete(object key)
        {
            if (this._labDbContext?.AdminStaffs.DeleteIfExists(new AdminStaff { ID = Convert.ToInt32(key) }, db => db.ID == Convert.ToInt32(key)) != null)
            {
                this._labDbContext?.SaveChanges();
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }
            else
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotModified));
        }

        public Task<AdminStaff?> Get(object key)
        {
            if (int.TryParse(key.ToString(), out int value))
                return Task.FromResult<AdminStaff?>(this._labDbContext?.AdminStaffs.Where(user => user.ID == value).FirstOrDefault());
            else
                return Task.FromResult<AdminStaff?>(null);
        }

        public Task<IEnumerable<AdminStaff>> Gets(int skip, int take)
        {
            if (skip > 0 && take > 0)
                return Task.FromResult<IEnumerable<AdminStaff>>(
                    this._labDbContext?.AdminStaffs.Skip(skip).Take(take).ToList() ?? new List<AdminStaff>()
                    );
            else if (take > 0)
                return Task.FromResult<IEnumerable<AdminStaff>>(
                    this._labDbContext?.AdminStaffs.Take(take).ToList() ?? new List<AdminStaff>()
                    );
            else if (skip > 0)
                return Task.FromResult<IEnumerable<AdminStaff>>(
                    this._labDbContext?.AdminStaffs.Skip(skip).ToList() ?? new List<AdminStaff>()
                    );
            else
                return Task.FromResult<IEnumerable<AdminStaff>>(
                    this._labDbContext?.AdminStaffs.ToList() ?? new List<AdminStaff>()
                    );
        }

        public Task<HttpResponseMessage> Update(AdminStaff data)
        {

            if (this._labDbContext?.AdminStaffs.UpdateIfExists(data, db => db.ID == data.ID) != null)
            {
                this._labDbContext?.SaveChanges();
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }
            else
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotModified));
        }
    }
}
