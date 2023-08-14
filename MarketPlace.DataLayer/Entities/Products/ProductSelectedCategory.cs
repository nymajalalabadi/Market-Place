using MarketPlace.DataLayer.Entities.Common;

namespace MarketPlace.DataLayer.Entities.Products
{
    public class ProductSelectedCategory : BaseEntity
    {
        #region properties

        public long ProductId { get; set; }

        public long ProductCategoryId { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        public ProductCategory ProductCategory { get; set; }

        #endregion
    }
}
