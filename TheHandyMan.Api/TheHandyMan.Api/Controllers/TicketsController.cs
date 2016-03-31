using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using HandyMan.Core.Domain;
using HandyMan.Core.Repository;
using HandyMan.Core.Infrastructure;
using HandyMan.Core.Model;
using AutoMapper;
using TheHandyMan.Api.Infrastructure;

namespace TheHandyMan.Api.Controllers
{
    [Authorize]
    public class TicketsController : BaseApiController
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketServiceRepository _ticketServiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TicketsController(ITicketRepository ticketRepository, IUserRepository userRepository, ITicketServiceRepository ticketServiceRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _ticketRepository = ticketRepository;
            _unitOfWork = unitOfWork;
            _ticketServiceRepository = ticketServiceRepository;
        }

        // GET: api/Tickets
        public IEnumerable<TicketModel> GetTickets()
        {
            return Mapper.Map<IEnumerable<TicketModel>>(_ticketRepository.GetAll());
        }

        //GET: api/Ticket/5/TicketService
        [Route("api/tickets/{TicketId}/ticketservices")]
        public IEnumerable<TicketServiceModel> GetTicketServiceForTicket(int TicketId)
        {
            return Mapper.Map<IEnumerable<TicketServiceModel>>(
                _ticketServiceRepository.GetWhere(ts => ts.TicketId == TicketId)
            );
        }

        // GET: api/Tickets/User
        [Route("api/tickets/user")]
        public IEnumerable<TicketModel> GetUserTickets()
        {
            return Mapper.Map<IEnumerable<TicketModel>>(_ticketRepository.GetWhere(t => t.UserId == CurrentUser.Id));
        }

        // GET: api/Tickets/Open
        [Route("api/tickets/open")]
        public IEnumerable<TicketModel> GetOpenTickets()
        {
            return Mapper.Map<IEnumerable<TicketModel>>(_ticketRepository.GetWhere(t => t.Confirmation == null));
        }


        // GET: api/Tickets/5
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult GetTicket(int id)
        {
            //Submission submission = db.Submissions.Find(id);
            Ticket ticket = _ticketRepository.GetById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TicketModel>(ticket));
        }
        // PUT: api/Tickets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTicket(int id, TicketModel ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.TicketId)
            {
                return BadRequest();
            }

            var dbTicket = _ticketRepository.GetById(id);

            dbTicket.Update(ticket);

            _ticketRepository.Update(dbTicket);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult PostTicket(TicketModel ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbTicket = new Ticket(ticket);

            dbTicket.UserId = CurrentUser.Id;
            _ticketRepository.Add(dbTicket);
            _unitOfWork.Commit();

            ticket.TicketId = dbTicket.TicketId;
            ticket.AppointmentDate = dbTicket.AppointmentDate;

            return CreatedAtRoute("DefaultApi", new { id = ticket.TicketId }, ticket);
        }

        // DELETE: api/Tickets/5
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult DeleteTicket(int id)
        {
            Ticket ticket = _ticketRepository.GetById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _ticketRepository.Delete(ticket);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<TicketModel>(ticket));
        }

        private bool TicketExists(int id)
        {
            return _ticketRepository.Any(e => e.TicketId == id);
        }
    }
}
