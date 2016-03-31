using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HandyMan.Core.Domain;
using HandyMan.Data.Infrastructure;
using HandyMan.Core.Repository;
using HandyMan.Core.Infrastructure;
using TheHandyMan.Api.Infrastructure;
using HandyMan.Core.Model;
using AutoMapper;

namespace TheHandyMan.Api.Controllers
{
    [Authorize]
    public class TicketServicesController : BaseApiController
    {
        private readonly ITicketServiceRepository _ticketServiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TicketServicesController(ITicketServiceRepository ticketServiceRepository, IUnitOfWork unitOfWork, IUserRepository userRepostory) : base(userRepostory)
        {
            _ticketServiceRepository = ticketServiceRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/TicketServices
        public IEnumerable<TicketServiceModel> GetTicketServices()
        {
            return Mapper.Map<IEnumerable<TicketServiceModel>>(_ticketServiceRepository.GetAll());
        }

        // GET: api/TicketServices/5
        [ResponseType(typeof(TicketService))]
        public IHttpActionResult GetTicketService(int id)
        {
            TicketService ticketService = _ticketServiceRepository.GetById(id);
            if (ticketService == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TicketServiceModel>(ticketService));
        }
        
        // PUT: api/TicketServices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTicketService(int id, TicketServiceModel ticketService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticketService.TicketServiceId)
            {
                return BadRequest();
            }

            var dbTicketService = _ticketServiceRepository.GetById(id);

            dbTicketService.Update(ticketService);

            _ticketServiceRepository.Update(dbTicketService);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!TicketServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TicketServices
        [ResponseType(typeof(TicketService))]
        public IHttpActionResult PostTicketService(TicketServiceModel ticketService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbTicketService = new TicketService(ticketService);

            _ticketServiceRepository.Add(dbTicketService);
            _unitOfWork.Commit();

            ticketService.TicketServiceId = dbTicketService.TicketServiceId;

            return CreatedAtRoute("DefaultApi", new { id = ticketService.TicketServiceId }, ticketService);
        }

        // DELETE: api/TicketServices/5
        [ResponseType(typeof(TicketService))]
        public IHttpActionResult DeleteTicketService(int id)
        {
            TicketService ticketService = _ticketServiceRepository.GetById(id);
            if (ticketService == null)
            {
                return NotFound();
            }

            _ticketServiceRepository.Delete(ticketService);
            _unitOfWork.Commit();

            return Ok(ticketService);
        }

        private bool TicketServiceExists(int id)
        {
            return _ticketServiceRepository.Any(e => e.TicketServiceId == id);
        }
    }
}