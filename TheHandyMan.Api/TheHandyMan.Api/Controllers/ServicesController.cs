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
using HandyMan.Core.Model;
using AutoMapper;

namespace TheHandyMan.Api.Controllers
{

    [Authorize]
    public class ServicesController : ApiController
    {
        // private HandyManDataContext db = new HandyManDataContext();
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServicesController(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Services
        public IEnumerable<ServiceModel> GetServices()
        {
            return Mapper.Map<IEnumerable < ServiceModel >> (_serviceRepository.GetAll());
        }

        // GET: api/Services/5
        [ResponseType(typeof(Service))]
        public IHttpActionResult GetService(int id)
        {
            Service service =_serviceRepository.GetById(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ServiceModel>(service));
        }

        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutService(int id, ServiceModel service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.ServiceId)
            {
                return BadRequest();
            }

            var dbService = _serviceRepository.GetById(id);

            dbService.Update(service);

            _serviceRepository.Update(dbService);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        [ResponseType(typeof(Service))]
        public IHttpActionResult PostService(ServiceModel service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbService = new Service(service);

            _serviceRepository.Add(dbService);
            _unitOfWork.Commit();

            service.ServiceId = dbService.ServiceId;

            return CreatedAtRoute("DefaultApi", new { id = service.ServiceId }, service);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(Service))]
        public IHttpActionResult DeleteService(int id)
        {
            Service service = _serviceRepository.GetById(id);
            if (service == null)
            {
                return NotFound();
            }

            _serviceRepository.Delete(service);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<ServiceModel>(service));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool ServiceExists(int id)
        {
            return _serviceRepository.Any(e => e.ServiceId == id);
        }
    }
}