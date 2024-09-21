using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Users.ValueObject
{
    public class MobileNumberConverter : ValueConverter<MobileNumber, string>
    {
        public MobileNumberConverter()
            : base(
                mobileNumber => mobileNumber.Value,
                value => new MobileNumber(value)) 
        {
        }
    }
}
