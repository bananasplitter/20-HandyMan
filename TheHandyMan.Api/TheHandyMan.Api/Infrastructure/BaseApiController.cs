using HandyMan.Core.Domain;
using HandyMan.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TheHandyMan.Api.Infrastructure
{
    public class BaseApiController : ApiController
    {
        protected readonly IUserRepository _userRepository;

        public BaseApiController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected User CurrentUser
        {
            get
            {
                return _userRepository.GetFirstOrDefault(u => u.UserName == User.Identity.Name);
            }
        }
    }
}