using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Common;
using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.DTOs.Seller;
using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Entities.Store;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class SellerService : ISellerService
    {
        #region constructor

        private readonly IGenericRepository<Seller> _sellerRepository;
        private readonly IGenericRepository<User> _userRepository;

        public SellerService(IGenericRepository<Seller> sellerRepository , IGenericRepository<User> userRepository)
        {
            _sellerRepository = sellerRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region seller

        public async Task<RequestSellerResult> AddNewSellerRequest(RequestSellerDTO seller, long userId)
        {
            var user = await _userRepository.GetEntityById(userId);

            if (user.IsBlocked) return RequestSellerResult.HasNotPermission;

            var hasUnderProgressRequest = await _sellerRepository.GetQuery().AsQueryable()
                .AnyAsync(s => s.UserId == userId && s.StoreAcceptanceState == StoreAcceptanceState.UnderProgress);

            if (hasUnderProgressRequest) return RequestSellerResult.HasUnderProgressRequest;

            var newSeller = new Seller()
            {
                UserId = userId,
                StoreName = seller.StoreName,
                Address = seller.Address,
                Phone = seller.Phone,
                StoreAcceptanceState = StoreAcceptanceState.UnderProgress
            };

            await _sellerRepository.AddEntity(newSeller);
            await _sellerRepository.SaveChanges();

            return RequestSellerResult.Success;
        }

        public async Task<FilterSellerDTO> FilterSellers(FilterSellerDTO filter)
        {
            var query = _sellerRepository.GetQuery()
                .Include(s=>s.User)
                .AsQueryable();

            #region state

            switch (filter.State)
            {
                case FilterSellerState.All:
                    query = query.Where(s => !s.IsDelete);

                    break;

                case FilterSellerState.Accepted:
                    query = query.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.Accepted && !s.IsDelete);
                    break;

                case FilterSellerState.UnderProgress:
                    query = query.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.UnderProgress && !s.IsDelete);
                    break;

                case FilterSellerState.Rejected:
                    query = query.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.Rejected && !s.IsDelete);
                    break;
            }

            #endregion


            #region filter

            if (filter.UserId != null && filter.UserId != 0)
                query = query.Where(s => s.UserId == filter.UserId);

            if (!string.IsNullOrEmpty(filter.StoreName))
                query = query.Where(s => EF.Functions.Like(s.StoreName, $"%{filter.StoreName}%"));

            if (!string.IsNullOrEmpty(filter.Phone))
                query = query.Where(s => EF.Functions.Like(s.Phone, $"%{filter.Phone}%"));

            if (!string.IsNullOrEmpty(filter.Address))
                query = query.Where(s => EF.Functions.Like(s.Address, $"%{filter.Address}%"));

            #endregion


            #region pager

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            var AllEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetPaging(pager).SetSellers(AllEntities);
        }


        public async Task<EditRequestSellerDTO> GetRequestSellerForEdit(long id, long currentUserId)
        {
            var seller = await _sellerRepository.GetEntityById(id);

            if (seller == null || seller.UserId != currentUserId) return null;

            return new EditRequestSellerDTO()
            {
                Id = seller.Id,
                StoreName = seller.StoreName,
                Phone = seller.Phone,
                Address = seller.Address
            };
            
        }

        public async Task<EditRequestSellerResult> EditRequestSeller(EditRequestSellerDTO request , long currentUserId)
        {
            var seller = await _sellerRepository.GetEntityById(request.Id);

            if(seller == null || seller.UserId != currentUserId) return EditRequestSellerResult.NotFound;

            seller.StoreName = seller.StoreName;
            seller.Phone = seller.Phone;
            seller.Address = seller.Address;
            seller.StoreAcceptanceState = StoreAcceptanceState.UnderProgress;

            _sellerRepository.EditEntity(seller);
            await _sellerRepository.SaveChanges();

            return EditRequestSellerResult.Success;
        }

        public async Task<bool> AcceptSellerRequest(long requestId)
        {
            var SellerRequest = await _sellerRepository.GetEntityById(requestId);
            if (SellerRequest != null)
            {
                SellerRequest.StoreAcceptanceState = StoreAcceptanceState.Accepted;
                SellerRequest.AdminDescription = "اطلاعات پنل قروشندگی شما با موفقیت تایید شده است";
                _sellerRepository.EditEntity(SellerRequest);
                await _sellerRepository.SaveChanges();

                return true;
            }

            return false;
        }

        public async Task<bool> RejectSellerRequest(RejectItemDTO reject)
        {
            var seller = await _sellerRepository.GetEntityById(reject.Id);
            if (seller != null)
            {
                seller.StoreAcceptanceState = StoreAcceptanceState.Rejected;
                seller.AdminDescription = reject.RejectMessage;
                _sellerRepository.EditEntity(seller);
                await _sellerRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Seller> GetLastActiveSellerByUserId(long userId)
        {
            return await _sellerRepository.GetQuery().OrderByDescending(s => s.CreateDate)
                .FirstOrDefaultAsync(s => s.UserId == userId && s.StoreAcceptanceState == StoreAcceptanceState.Accepted); 
        }

        public async Task<bool> HasUserAnyActiveSellerPanel(long userId)
        {
            return await _sellerRepository.GetQuery().OrderByDescending(s => s.CreateDate)
                .AnyAsync(s => s.UserId == userId && s.StoreAcceptanceState == StoreAcceptanceState.Accepted);
        }

        #endregion

        #region Dispose
        public async ValueTask DisposeAsync()
        {
            await _sellerRepository.DisposeAsync();
        }

        #endregion
    }
}
