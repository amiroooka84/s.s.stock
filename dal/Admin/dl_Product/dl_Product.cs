using Entity;
using Entity.AttributeProduct;
using Entity.ProductAndShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Admin.dl_Product
{
    public class dl_Product
    {
        //////////////
        #region Exists
        public bool ExistProduct(string NameProduct, long CodeProduct)
        {
            db db = new db();
            List<Product> lp = new List<Product>();
            foreach (var item in db.Products)
            {
                if (NameProduct == item.Name || CodeProduct == item.Code)
                {
                    lp.Add(item);
                }
            }
            if (lp.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ExistProductForEdit(string name, long code, int id)
        {
            db db = new db();
            int i = 0;
            await foreach (var item in db.Products)
            {
                if (item.id == id)
                {

                }
                else if (item.Name == name || item.Code == code)
                {
                    i = 1;
                    break;
                }
            }
            if (i <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        //////////////

        //////////////
        #region Add
        public bool AddProduct(Product product, List<long> Shopid, List<string> vs , List<AttributeProduct> attributeProducts)
        {
            db db = new db();
            var a = db.Products.Add(product);
            db.SaveChanges();
            if (Shopid != null)
            {
                foreach (var item in Shopid)
                {
                    ProductAndShop productAndShop = new ProductAndShop();
                    productAndShop.Productid = a.Entity.id;
                    productAndShop.Shopid = item;
                    db.ProductAndShop.Add(productAndShop);
                }
            }
            foreach (var item in attributeProducts)
            {
                AttributeProduct attribute = new AttributeProduct();
                attribute.ProductId = a.Entity.id;
                attribute.Size = item.Size;
                attribute.Color = item.Color;
                attribute.ColorCode = item.ColorCode;
                attribute.Number = item.Number;
                db.AttributeProducts.Add(attribute);
            }
            foreach (var item in vs)
            {
                ImagePath imagePath = new ImagePath();
                imagePath.Image = item;
                imagePath.Productid = a.Entity.id;
                imagePath.ProductName = a.Entity.Name;
                db.ImagePath.Add(imagePath);
            }
            db.SaveChanges();
            return true;
        }

        public List<AttributeProduct> ReadAttributeProduct(int id)
        {
            db db = new db();
            List<AttributeProduct> attributeProducts = new List<AttributeProduct>();
            foreach (var item in db.AttributeProducts)
            {
                if (item.ProductId == id)
                {
                    attributeProducts.Add(item);
                }
            }
            return attributeProducts;
        }
        #endregion
        //////////////

        //////////////
        #region Edit
        public async Task<bool> EditProduct(Product product, List<long> shops, List<string> vs, List<AttributeProduct> attributes, int id)
        {
            db db = new db();
            var p = db.Products.SingleOrDefault(b => b.id == id);
            p.Name = product.Name;
            p.Code = product.Code;
            p.Price = product.Price;
            p.Discount = product.Discount;
            p.Specifications = product.Specifications;
            p.Number = product.Number;
            p.Categoryid = product.Categoryid;
            p.Discription = product.Discription;
            if (product.ImagePath != null)
            {
                p.ImagePath = product.ImagePath;
            }
            await foreach (var item in db.ProductAndShop)
            {
                if (item.Productid == id)
                {
                    db.ProductAndShop.Remove(item);
                }
            }
            await foreach (var item in db.AttributeProducts)
            {
                //if (item.ProductId == id)
                //{
                //    db.AttributeProducts.Remove(item);
                //}
                foreach (var item1 in attributes)
                {
                    if (item.id == item1.id && item1.id != 0)
                    {
                        item.Size = item1.Size;
                        item.Color = item1.Color;
                        item.ColorCode = item1.ColorCode;
                        item.Number = product.Number;
                    }
                }
            }
             db.SaveChanges();
            foreach (var item in attributes)
            {
                if (item.id == 0)
                {
                    AttributeProduct attribute = new AttributeProduct();
                    attribute.ProductId = id;
                    attribute.Size = item.Size;
                    attribute.Color = item.Color;
                    attribute.ColorCode = item.ColorCode;
                    attribute.Number = item.Number;
                    db.AttributeProducts.Add(attribute);
                }

            }
            if (shops != null)
            {
                foreach (var item in shops)
                {
                    ProductAndShop productAndShop = new ProductAndShop();
                    productAndShop.Productid = id;
                    productAndShop.Shopid = item;
                    db.ProductAndShop.Add(productAndShop);
                }
            }
            if (vs.Count != 0)
            {
                await foreach (var item in db.ImagePath)
                {
                    if (item.Productid == id)
                    {
                        db.ImagePath.Remove(item);
                    }
                }
                foreach (var item in vs)
                {
                    ImagePath imagePath = new ImagePath();
                    imagePath.Image = item;
                    imagePath.Productid = id;
                    db.ImagePath.Add(imagePath);
                }
            }
            db.SaveChanges();
            return true;
        }

        #endregion
        //////////////
        
        //////////////
        #region Reads
        public Product ReadProductById(int id)
        {
            db db = new db();
            Product product = new Product();
            foreach (var item in db.Products)
            {
                if (item.id == id)
                {
                    product = item;
                }
            }
            return product;
        }
        public List<Product> ReadProduct()
        {
            db db = new db();
            return db.Products.ToList();
        }
        #endregion
        //////////////

        //////////////
        #region Search
        public List<Product> SearchProduct(string name)
        {
            db db = new db();
            List<Product> lc = new List<Product>();
            foreach (var item in db.Products)
            {
                if (item.Name.Contains(name))
                {
                    lc.Add(item);
                }
            }
            return lc;
        }
        #endregion
        //////////////

        //////////////
        #region Delete
        public bool DeleteProduct(int id)
        {
            db db = new db();
            foreach (var item in db.ImagePath)
            {
                if (item.Productid == id)
                {
                    db.ImagePath.Remove(item);
                }
            }
            Product product = new Product();
            product = ReadProductById(id);
            if (product == null)
            {
                return false;
            }
            else
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
        }
        #endregion
        //////////////
    }
}
