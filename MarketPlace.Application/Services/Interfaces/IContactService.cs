using MarketPlace.DataLayer.DTOs.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IContactService : IAsyncDisposable
    {
        #region Contact Us


        Task CreateContactUs(CreateContactUsDTO contact, string userIp, long? userId);


        #endregion


        #region Ticket

        Task<AddTicketResult> AddUserTicket(AddTicketViewModel ticket , long userId);

        Task<FilterTicketDTO> FilterTicket(FilterTicketDTO filterTicket);

        Task<TicketDetailDTO> GetTicketForShow(long ticketId , long UserId);
        Task<AnswerTicketResult> AnswerTicket(AnswerTicketDTO answer , long userId);

        #endregion
    }
}
