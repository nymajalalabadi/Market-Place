using MarketPlace.DataLayer.DTOs.SellerWallet;
using MarketPlace.DataLayer.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface ISellerWalletService : IAsyncDisposable
    {
        #region wallet

        Task<FilterSellerWalletDTO> FilterSellerWallet(FilterSellerWalletDTO filter);

        Task AddWallet(SellerWallet Wallet);

        #endregion
    }
}
