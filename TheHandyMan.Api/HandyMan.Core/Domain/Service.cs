using HandyMan.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Domain
{
    public class Service
    {
        //Primary Key
        public int ServiceId { get; set; }
        //Foreign Key none

        //Properties
        public string ServiceName { get; set; }

        //Relations
        public virtual ICollection<TicketService> TicketServices { get; set; }

        public Service()
        {

        }

        public Service(ServiceModel model)
        {
            this.Update(model);
        }

        public void Update(ServiceModel model)
        {
            ServiceId = model.ServiceId;
            ServiceName = model.ServiceName;

        }

    }
}
