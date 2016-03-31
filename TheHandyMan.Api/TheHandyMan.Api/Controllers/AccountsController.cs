using AutoMapper;
using HandyMan.Core.Infrastructure;
using HandyMan.Core.Model;
using HandyMan.Core.Repository;
using HandyMan.Core.Services.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TheHandyMan.Api.Infrastructure;
using TheHandyMan.Api.Requests;

namespace TheHandyMan.Api.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IAuthorizationRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountsController(IPaymentService paymentService, IAuthorizationRepository authRepository, IUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _paymentService = paymentService;
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
        }

        [Route("api/accounts/register")]
        public async Task<IHttpActionResult> Register(RegistrationModel registration)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.RegisterUser(registration);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form was invalid.");
            }
        }

        [Route("api/accounts/currentuser")]
        [HttpGet]
        [ResponseType(typeof(UserModel.Profile))]
        public IHttpActionResult GetCurrentUser ()
        {
            return Ok(Mapper.Map<UserModel.Profile>(CurrentUser));
        }


        [Route("api/accounts/currentuser")]
        [HttpPut]
        public IHttpActionResult UpdateCurrentUser (string id, UserModel.Profile user)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/accounts/paytotal")]
        public IHttpActionResult PayTotal(PaymentRequest request)
        {
            _paymentService.TotalRate(CurrentUser, request.token, request.rate);

            return Ok();
        }
    }
}
