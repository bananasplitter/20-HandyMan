using HandyMan.Core.Domain;
using HandyMan.Core.Infrastructure;
using HandyMan.Core.Repository;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Services.Finance
{
    public class StripePaymentService : IPaymentService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StripePaymentService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public void TotalRate(User user, string token, int rate)
        {
            var myCharge = new StripeChargeCreateOptions();

            myCharge.Amount = rate;
            myCharge.Currency = "usd";

            myCharge.Source = new StripeSourceOptions()
            {
                TokenId = token,
            };

            var chargeService = new StripeChargeService("sk_test_TN7PiTwX6CACkBrg9ohMQDjS");

            StripeCharge stripeCharge = chargeService.Create(myCharge);

            if (stripeCharge.Paid)
            {
                user.Rate = user.Rate.GetValueOrDefault() + rate;

                _userRepository.Update(user);

                _unitOfWork.Commit();

            }
            else
            {
                throw new Exception("Payment was unsuccessful");
            }

        }
    }
}
