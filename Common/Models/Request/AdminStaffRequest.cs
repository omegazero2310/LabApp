using CommonClass.Enums;

namespace CommonClass.Models.Request
{
    public class AdminStaffRequest
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public GenderOptions Gender { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string ProfileImage { get; set; }

        public int PartID { get; set; }
    }
}
