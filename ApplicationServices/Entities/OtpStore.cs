using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Entities
{
    public class OtpStore : BaseEntity<Guid>
    {
        internal OtpStore() { }
        private OtpStore(Guid id, string otp, string email, string phoneNumber, int minutes)
        {
            Id = id;
            OTP = otp;
            Email = email;
            PhoneNumber = phoneNumber;
            CreatedOn = DateTime.UtcNow;
            ExpiryDate = DateTime.UtcNow.AddMinutes(minutes);
            IsExpired = false;
            IsUsed = false;
            LastModified = DateTime.UtcNow;
        }

        public string OTP { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsExpired { get; set; }

        public bool IsUsed { get; set; }

        public class Factory
        {
            public static OtpStore GenerateOto(Guid id, string otp, string email, string phoneNumber, int minutes)
            {
                return new OtpStore(id, otp, email, phoneNumber, minutes);
            }
        }
        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
