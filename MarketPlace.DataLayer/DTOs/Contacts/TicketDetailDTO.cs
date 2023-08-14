using MarketPlace.DataLayer.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.DTOs.Contacts
{
    public class TicketDetailDTO
    {
        public Ticket Ticket { get; set; }

        public List<TicketMessage> TicketMessages { get; set; }
    }
}
