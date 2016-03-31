using AutoMapper;
using HandyMan.Core.Domain;
using HandyMan.Core.Infrastructure;
using HandyMan.Core.Model;
using HandyMan.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheHandyMan.Api.Infrastructure;

namespace TheHandyMan.Api.Controllers
{

    public class TimeEntryController : BaseApiController
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository, IUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _timeEntryRepository = timeEntryRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TimeEntryModel> GetAll()
        {
            return Mapper.Map<IEnumerable<TimeEntryModel>>(
                _timeEntryRepository.GetWhere(te => te.UserId == CurrentUser.Id)
            );
        }

        // This creates a TimeEntry record for v this guy v, setting StartDate to DateTime.Now
        [Route("api/timeentry/in/{ticketId}")]
        public IHttpActionResult ClockIn(int ticketId)
        {
            // make a new time entry, bind it to
            var dbTimeEntry = new TimeEntry
            {
                User = CurrentUser,
                TicketId = ticketId,
                StartDate = DateTime.Now
            };

            // add this time entry to the repository (using _timeEntryRepository)
            _timeEntryRepository.Add(dbTimeEntry);
            // commit changes to the database (using _unitOfWork)
            _unitOfWork.Commit();

            return Ok();
        }

        [Route("api/timeentry/out")]
        public IHttpActionResult ClockOut(int ticketId)
        {
            var dbtimeEntry = _timeEntryRepository.GetFirstOrDefault(te => te.UserId == CurrentUser.Id && te.TicketId == ticketId);

            dbtimeEntry.EndDate = DateTime.Now;

            // update this time entry in the repository
            dbtimeEntry.UserId = CurrentUser.Id;
            _timeEntryRepository.Add(dbtimeEntry);

            _unitOfWork.Commit();

            // commit changes to the database

            return Ok();
        }

        [Route("api/timeentry/all/admin")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<TimeEntryModel> GetTimeEntries()
        {
            return Mapper.Map<IEnumerable<TimeEntryModel>>(
                _timeEntryRepository.GetAll()
            );
        }
    }
}
