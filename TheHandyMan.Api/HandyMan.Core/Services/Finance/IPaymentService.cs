using HandyMan.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Services.Finance
{
    public interface IPaymentService
    {
        void TotalRate(User user, string token, int rate);
    }
}
