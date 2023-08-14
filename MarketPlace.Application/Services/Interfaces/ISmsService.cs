using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface ISmsService
    {
        Task SendVerificationSms(string mobile, string activationCode);
        Task SendUserPasswordSms(string mobile, string password);
    }
}
