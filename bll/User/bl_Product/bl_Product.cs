using dal.User.dl_Product;
using Entity;
using Entity.AttributeProduct;
using Entity.Basket;
using Entity.Interests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bll.User.bl_Product
{
    public class bl_Product
    {
        dl_Product dl_Product = new dl_Product();
        public async Task<Product> ReadProductByName(string name)
        {
            return await dl_Product.ReadProductByName(name);
        }

        public async Task<List<ImagePath>> ReadImagesProduct(long productid)
        {
            return await dl_Product.ReadImagesProduct(productid);
        }

        public async Task<List<Product>> SearchProduct(string search)
        {
            return await dl_Product.SearchProduct(search);
        }

        public List<Shop> ReadShopByProductId(long id)
        {
            return dl_Product.ReadShopByProductId(id);
        }

        public List<Product> ReadSimilarProduct(long categoryid)
        {
            return dl_Product.ReadSimilarProduct(categoryid);
        }

        public async Task<List<Product>> SearchProductByFilter(string search, string order, List<string> categories)
        {
            return await dl_Product.SearchProductByFilter(search , order , categories);
        }

        public async Task<List<AttributeProduct>> ReadAttributeProduct(long ProductId)
        {
            return await dl_Product.ReadAttributeProduct(ProductId);
        }

        public async Task<AttributeProduct> CalculatePrice(long productid, string color, string size)
        {
            return await dl_Product.CalculatePrice(productid, color, size);
        }

        public async Task<bool> AddBasket(string UserId, long productid, string color, string size)
        {
            return await dl_Product.AddBasket(UserId,productid, color, size);
        }

        public List<Basket> ReadBasket(string UserId)
        {
            return dl_Product.ReadBasket(UserId);
        }

        public List<Product> ReadProduct()
        {
            return dl_Product.ReadProduct();
        }

        public List<AttributeProduct> ReadAttribute()
        {
            return dl_Product.ReadAttribute();
        }

        public Basket ReadItemBasket(string UserId, AttributeProduct attributeProduct, long productid)
        {
            return dl_Product.ReadItemBasket(UserId , attributeProduct , productid);
        }

        public bool AddInterests(long productid, string userid)
        {
            return dl_Product.AddInterests(productid, userid);
        }

        public bool DeleteInterests(long productid, string userid)
        {
            return dl_Product.DeleteInterests(productid, userid);
        }

        public List<Interests> ReadInterests(string userid)
        {
            return dl_Product.ReadInterests(userid);
        }

        public Product ReadProductById(long productId)
        {
            return dl_Product.ReadProductById(productId);
        }
    }
}
