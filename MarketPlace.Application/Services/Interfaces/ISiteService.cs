using MarketPlace.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface ISiteService : IAsyncDisposable
    {
        #region Site Setting

        Task<SiteSetting> GetDefaultSiteSetting();

        #endregion


        #region slider

        Task<List<Slider>> GetAllActiveSliders();

        #endregion


        #region site banners

        Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements);

        #endregion
    }
}
