using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dal.Admin.dl_Product;
using Entity;
using Entity.AttributeProduct;

namespace bll.Admin.bl_Product
{
    public class bl_Product
    {
        dl_Product dl_Product = new dl_Product();
        public bool ExistProduct(string NameProduct, long CodeProduct)
        {
            return dl_Product.ExistProduct(NameProduct, CodeProduct);
        }
        public bool AddProduct(Product product, List<long> Shopid, List<string> vs , List<AttributeProduct> attributeProducts)
        {
            return dl_Product.AddProduct(product , Shopid , vs , attributeProducts);
        }
        public List<Product> ReadProduct()
        {
            return dl_Product.ReadProduct();
        }

        public List<Product> SearchProduct(string name)
        {
            return dl_Product.SearchProduct(name);
        }

        public Product ReadProductById(int id)
        {
            return dl_Product.ReadProductById(id);
        }

        public async Task<bool> ExistProductForEdit(string name, long code, int id)
        {
            return await dl_Product.ExistProductForEdit(name , code , id);
        }

        public async Task<bool> EditProduct(Product product, List<long> shops, List<string> vs, List<AttributeProduct> attributes, int id)
        {
            return await dl_Product.EditProduct(product, shops , vs , attributes , id );
        }

        public bool DeleteProduct(int id)
        {
            return dl_Product.DeleteProduct(id);
        }

        public List<AttributeProduct> ReadAttributeProduct(int id)
        {
            return dl_Product.ReadAttributeProduct(id);
        }
    }
}
