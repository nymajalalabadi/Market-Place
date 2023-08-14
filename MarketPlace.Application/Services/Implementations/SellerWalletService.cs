using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.DTOs.SellerWallet;
using MarketPlace.DataLayer.Entities.Wallet;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class SellerWalletService : ISellerWalletService
    {
        #region constructor

        private readonly IGenericRepository<SellerWallet> _sellerWalletRepository;

        public SellerWalletService(IGenericRepository<SellerWallet> sellerWalletRepository)
        {
            _sellerWalletRepository = sellerWalletRepository;
        }

        #endregion

        #region wallet

        public async Task<FilterSellerWalletDTO> FilterSellerWallet(FilterSellerWalletDTO filter)
        {
            var query = _sellerWalletRepository.GetQuery()
               .AsQueryable();

            #region filter

            if (filter.SellerId != null && filter.SellerId != 0)
                query = query.Where(s => s.SellerId == filter.SellerId.Value);

            if (filter.PriceFrom != null && filter.PriceFrom != 0)
                query = query.Where(s => s.Price >= filter.PriceFrom.Value);

            if (filter.PriceTo != null && filter.PriceTo != 0)
                query = query.Where(s => s.Price <= filter.PriceTo.Value);

            #endregion

            #region pager

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            var AllEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetPaging(pager).SetSellerWallets(AllEntities);
        }

        public async Task AddWallet(SellerWallet Wallet)
        {
           await _sellerWalletRepository.AddEntity(Wallet);
           await _sellerWalletRepository.SaveChanges();
        }
        #endregion

        public async ValueTask DisposeAsync()
        {
            await _sellerWalletRepository.DisposeAsync();
        }
    }
}
