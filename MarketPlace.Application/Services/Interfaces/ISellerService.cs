using MarketPlace.DataLayer.DTOs.Common;
using MarketPlace.DataLayer.DTOs.Seller;
using MarketPlace.DataLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface ISellerService : IAsyncDisposable
    {
        #region seller

        Task<RequestSellerResult> AddNewSellerRequest(RequestSellerDTO seller , long userId);
        Task<FilterSellerDTO> FilterSellers(FilterSellerDTO filter );
        Task<EditRequestSellerDTO> GetRequestSellerForEdit(long id , long currentUserId);
        Task<EditRequestSellerResult> EditRequestSeller(EditRequestSellerDTO request, long currentUserId);
        Task<bool> AcceptSellerRequest(long requestId);
        Task<bool> RejectSellerRequest(RejectItemDTO reject);
        Task<Seller> GetLastActiveSellerByUserId(long userId);
        Task<bool> HasUserAnyActiveSellerPanel(long userId);

        #endregion

    }
}
