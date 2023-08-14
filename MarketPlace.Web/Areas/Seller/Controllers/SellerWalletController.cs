using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.SellerWallet;
using MarketPlace.Web.PresentationsExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.Seller.Controllers
{
    public class SellerWalletController : SellerBaseController
    {
        #region constructor

        private readonly ISellerWalletService _sellerWalletService;
        private readonly ISellerService _sellerService;

        public SellerWalletController(ISellerWalletService sellerWalletService, ISellerService sellerService)
        {
            _sellerWalletService = sellerWalletService;
            _sellerService = sellerService;
        }

        #endregion

        #region index

        [HttpGet("seller-wallet")]
        public async Task<IActionResult> Index(FilterSellerWalletDTO filter)
        {
            var seller = await _sellerService.GetLastActiveSellerByUserId(User.GetUserId());
            if (seller == null) return NotFound();

            filter.SellerId = seller.Id;
            filter.TakeEntity = 5;

            return View(await _sellerWalletService.FilterSellerWallet(filter));
        }

        #endregion
    }
}
