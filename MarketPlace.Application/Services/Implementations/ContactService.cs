using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Contacts;
using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.Entities.Contacts;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class ContactService : IContactService
    {
        #region constructor

        private readonly IGenericRepository<ContactUs> _contactUsRepository;
        private readonly IGenericRepository<Ticket> _ticketRepository;
        private readonly IGenericRepository<TicketMessage> _ticketMessageRepository;

        public ContactService(IGenericRepository<ContactUs> contactUsRepository , IGenericRepository<Ticket> ticketRepository 
            , IGenericRepository<TicketMessage> ticketMessageRepository)
        {
            _contactUsRepository = contactUsRepository;
            _ticketRepository = ticketRepository;
            _ticketMessageRepository = ticketMessageRepository;
        }

        #endregion


        #region Contact Us

        public async Task CreateContactUs(CreateContactUsDTO contact, string userIp, long? userId)
        {
            var newContact = new ContactUs()
            {
                UserId = userId != null && userId.Value != 0 ? userId.Value : (long?)null ,
                UserIp = userIp,
                FullName = contact.FullName,
                Subject = contact.Subject,
                Text = contact.Text,
                Email = contact.Email
            };
            await _contactUsRepository.AddEntity(newContact);
            await _contactUsRepository.SaveChanges();
        }

        #endregion

        #region Ticket

        public async Task<AddTicketResult> AddUserTicket(AddTicketViewModel ticket , long userId)
        {
            if (string.IsNullOrEmpty(ticket.Text)) return AddTicketResult.Error;

            //add ticket
            var newTicket = new Ticket()
            {
                OwnerId = userId,
                IsReadByOwner = true,
                TicketPriority = ticket.TicketPriority,
                Title = ticket.Title,
                TicketSection = ticket.TicketSection,
                TicketState = TicketState.UnderProgress
            };
            await _ticketRepository.AddEntity(newTicket);
            await _ticketRepository.SaveChanges();

            //add ticket message
            var newMessage = new TicketMessage()
            {
                TicketId = newTicket.Id,
                SenderId = userId,
                Text = ticket.Text
            };
            await _ticketMessageRepository.AddEntity(newMessage);
            await _ticketMessageRepository.SaveChanges();

            return AddTicketResult.Success;
        }



        public async Task<FilterTicketDTO> FilterTicket(FilterTicketDTO filterTicket)
        {
            var query = _ticketRepository.GetQuery().AsQueryable();

            #region state

            switch (filterTicket.FilterTicketState)
            {
                case FilterTicketState.All:
                    break;

                case FilterTicketState.Deleted:
                    query = query.Where(f => f.IsDelete);
                    break;

                case FilterTicketState.NotDeleted:
                    query = query.Where(f => !f.IsDelete);
                    break;
            }

            switch (filterTicket.OrderBy)
            {
                case FilterTicketOrder.CreateDate_ASC:
                    query = query.OrderBy(f => f.CreateDate);
                    break;

                case FilterTicketOrder.CreateDate_DES:
                    query = query.OrderByDescending(f => f.CreateDate);
                    break;              
            }

            #endregion

            #region filter

            if (filterTicket.TicketSection != null)
            {
                query = query.Where(f => f.TicketSection == filterTicket.TicketSection.Value);
            }

            if (filterTicket.TicketPriority != null)
            {
                query = query.Where(f => f.TicketPriority == filterTicket.TicketPriority.Value);
            }

            if (filterTicket.UserId != null && filterTicket.UserId != 0)
            {
                query = query.Where(f => f.OwnerId == filterTicket.UserId.Value);
            }

            if (!string.IsNullOrEmpty(filterTicket.Title))
            {
                query = query.Where(f => EF.Functions.Like(f.Title, $"%{filterTicket.Title}%"));
            }

            #endregion

            #region pager

            var pager = Pager.Build(filterTicket.PageId, await query.CountAsync(), filterTicket.TakeEntity, filterTicket.HowManyShowPageAfterAndBefore);

            var AllEntities = await query.Paging(pager).ToListAsync();
            #endregion


            return filterTicket.SetPaging(pager).SetTickets(AllEntities);
        }

        public async Task<TicketDetailDTO> GetTicketForShow(long ticketId, long UserId)
        {
            var ticket = await _ticketRepository.GetQuery().AsQueryable()
                .Include(t => t.Owner).SingleOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null || ticket.OwnerId != UserId) return null;

            return new TicketDetailDTO
            {
                Ticket = ticket,
                TicketMessages = await _ticketMessageRepository.GetQuery().AsQueryable().OrderByDescending(t => t.CreateDate)
                .Where(t => t.TicketId == ticketId && !t.IsDelete).ToListAsync()
            };
        }

        public async Task<AnswerTicketResult> AnswerTicket(AnswerTicketDTO answer, long userId)
        {
            var ticket = await _ticketRepository.GetEntityById(answer.Id);

            if (ticket == null) return AnswerTicketResult.NotFound;

            if (ticket.OwnerId != userId) return AnswerTicketResult.NotForUser;

            var ticketMessage = new TicketMessage()
            {
                TicketId = answer.Id,
                SenderId = userId,
                Text = answer.Text
            };

            await _ticketMessageRepository.AddEntity(ticketMessage);
            await _ticketMessageRepository.SaveChanges();

            ticket.IsReadByAdmin = false;
            ticket.IsReadByOwner = true;

            await _ticketRepository.SaveChanges();

            return AnswerTicketResult.Success;
        }
        #endregion


        #region Dispose
        public async ValueTask DisposeAsync()
        {
            await _contactUsRepository.DisposeAsync();
        }

        #endregion
    }
}
