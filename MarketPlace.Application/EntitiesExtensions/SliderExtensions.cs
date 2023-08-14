using MarketPlace.Application.Utils;
using MarketPlace.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.EntitiesExtensions
{
    public static class SliderExtensions
    {
        public static string GetSliderImageAddress(this Slider slider)
        {
              return PathExtension.SliderOrigin + slider.ImageName;            
        }
    }
}
