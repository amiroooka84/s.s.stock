using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Entity.AttributeProduct;
using Entity.Basket;
using Entity.Interests;

namespace dal.User.dl_Product
{
    public class dl_Product
    {
        ////////////////
        #region Reads
        public List<Product> ReadSimilarProduct(long categoryid)
        {
            db db = new db();
            List<Product> products = new List<Product>();
            foreach (var item in db.Products)
            {
                if (item.Categoryid == categoryid)
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public List<Shop> ReadShopByProductId(long id)
        {
            db db = new db();
            List<long> lpas = new List<long>();
            foreach (var item in db.ProductAndShop)
            {
                if (item.Productid == id)
                {
                    lpas.Add(item.Shopid);
                }
            }
            List<Shop> ls = new List<Shop>();
            foreach (var item in db.Shops)
            {
                foreach (var ShopId in lpas)
                {
                    if (item.id == ShopId)
                    {
                        ls.Add(item);
                    }
                }
            }
            return ls;
        }

        public async Task<Product> ReadProductByName(string name)
        {
            db db = new db();
            Product product = new Product();
            await foreach (var item in db.Products)
            {
                if (item.Name == name)
                {
                    product = item;
                }
            }
            return product;
        }

        public async Task<bool> AddBasket(string UserId , long productid, string color, string size)
        {
            db db = new db();
            if (color == null)
            {
                color = "0";
            }
            if (size == null)
            {
                size = "0";
            }
            AttributeProduct attributeProduct = new AttributeProduct();
                await foreach (var item in db.AttributeProducts)
                {
                    if (productid == item.ProductId)
                    {
                        if (color == item.Color && size == item.Size)
                        {
                            attributeProduct = item;
                        }
                    }
                }
                if (UserId != null && productid != 0 && attributeProduct != null && attributeProduct.Number > 0)
                {
                    Basket basket = new Basket();
                    basket.UserId = UserId;
                    basket.ProductId = productid;
                    basket.AttributeId = attributeProduct.id;
                    basket.Number = 1;
                    await foreach (var item in db.Baskets)
                    {
                        if (item.UserId == UserId)
                        {
                            if (item.ProductId == productid && item.AttributeId == attributeProduct.id)
                            {
                                return false;
                            }
                        }
                    }
                    await db.Baskets.AddAsync(basket);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            
        }

        public Product ReadProductById(long productId)
        {
            db db = new db();
            foreach (var item in db.Products)
            {
                if (item.id == productId)
                {
                    return item;
                }
            }
            return null;
        }

        public List<Interests> ReadInterests(string userid)
        {
            db db = new db();
            List<Interests> interests = new List<Interests>();
            foreach (var item in db.Interests)
            {
                if (item.UserId == userid)
                {
                    interests.Add(item);
                }
            }
            return interests;
        }


        public Basket ReadItemBasket(string userId, AttributeProduct attributeProduct, long productid)
        {
            db db = new db();
            Basket basket = new Basket();
            basket = null;
            foreach (var item in db.Baskets)
            {
                if (item.UserId == userId && item.AttributeId == attributeProduct.id && item.ProductId == productid)
                {
                    basket = item;
                }
            }
            return basket;
        }

        public List<AttributeProduct> ReadAttribute()
        {
            db db = new db();
            return db.AttributeProducts.ToList();
        }

        public List<Product> ReadProduct()
        {
            db db= new db();
            return db.Products.ToList();
        }

        public List<Basket> ReadBasket(string userId)
        {
            db db = new db();
            List<Basket> db_baskets = new List<Basket>();
            db_baskets = db.Baskets.ToList();
            List<Basket> baskets = new List<Basket>();
            List<Basket> DelBasket = new List<Basket>();
            foreach (var item in db_baskets)
            {
                int t = 0;
                if (item.UserId == userId)
                {
                    foreach (var item1 in db.AttributeProducts)
                    {
                        if (item.UserId == userId && item1.ProductId == item.ProductId && item.AttributeId == item1.id)
                        {
                            if (item1.ProductId == item.ProductId)
                            {
                                t++;
                            }
                            if (item1.Number < 1)
                            {
                                DelBasket.Add(item);
                                break;
                            }
                        }
                    }
                    if (t == 0)
                    {
                        DelBasket.Add(item);
                    }
                }
            }
            foreach (var item in DelBasket)
            {
                db.Baskets.Remove(item);
                db_baskets.Remove(item);
            }
            db.SaveChanges();
            foreach (var item in db_baskets)
            {
                if (item.UserId == userId)
                {
                    foreach (var item1 in db.AttributeProducts)
                    {
                        if (item1.ProductId == item.ProductId)
                        {
                                baskets.Add(item);
                                break;
                        }
                    }
                }
            }
            return baskets;
        }

        public async Task<List<AttributeProduct>> ReadAttributeProduct(long ProductId)
        {
            db db = new db();
            List<AttributeProduct> attributeProducts = new List<AttributeProduct>();
            await foreach (var item in db.AttributeProducts)
            {
                if (item.ProductId == ProductId)
                {
                    attributeProducts.Add(item);
                }
            }
            return attributeProducts;
        }

        public async Task<List<ImagePath>> ReadImagesProduct(long productid)
        {
            db db = new db();
            List<ImagePath> imagePaths = new List<ImagePath>();
            await foreach (var item in db.ImagePath)
            {
                if (item.Productid == productid)
                {
                    imagePaths.Add(item);
                }
            }
            return imagePaths;
        }

        public List<Category> ReadSubsetCategory(long id)
        {
            db db = new db();
            List<Category> categories = new List<Category>();
            foreach (var item in db.Categories)
            {
                if (item.Categoryid == id)
                {
                    categories.Add(item);
                    categories.AddRange(ReadSubsetCategory(item.id));
                }
                else if (item.id == id)
                {
                    categories.Add(item);
                }
            }
            return categories;
        }
        #endregion
        ////////////////

        ////////////////
        #region SearchProduct
        public async Task<List<Product>> SearchProduct(string search)
        {
            db db = new db();
            List<Product> products = new List<Product>();
            await foreach (var item in db.Products)
            {
                if (item.Name.Contains(search))
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public async Task<List<Product>> SearchProductByFilter(string search, string order, List<string> categories)
        {
            db db = new db();
            List<Product> products = new List<Product>();
            List<long> CategoryId = new List<long>();
            if (categories != null)
            {
                CategoryId = ConvertNameCategoryToId(categories);
            }
            if (search == null)
            {
                await foreach (var product in db.Products)
                {
                    if (categories != null)
                    {
                        List<Category> categories1 = new List<Category>();

                        foreach (var categoryid in CategoryId)
                        {
                            categories1.AddRange(ReadSubsetCategory(categoryid));
                        }

                        foreach (var item in categories1)
                        {
                            if (item.id == product.Categoryid)
                            {
                                products.Add(product);
                                break;
                            }
                        }
                    }
                    else
                    {
                        products.Add(product);
                    }
                }
            }
            else
            {
                await foreach (var product in db.Products)
                {
                    if (categories != null)
                    {
                        List<Category> categories1 = new List<Category>();
                        foreach (var categoryid in CategoryId)
                        {
                            categories1.AddRange(ReadSubsetCategory(categoryid));
                        }

                        foreach (var item in categories1)
                        {
                            if (item.id == product.Categoryid)
                            {
                                if (product.Name.Contains(search) || product.Slack.Contains(search))
                                {
                                    products.Add(product);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (product.Name.Contains(search) || product.Slack.Contains(search))
                        {
                            products.Add(product);
                        }
                    }
                }

            }

            if (order == "Expensive")
            {
                var p = from product in products orderby product.Price descending select product;
                products = p.ToList();
            }
            else if (order == "Cheap")
            {
                var p = from product in products orderby product.Price ascending select product;
                products = p.ToList();
            }
            else if (order == "Discount")
            {
                var p = from product in products orderby product.Discount descending select product;
                products = p.ToList();
            }

            return products;
        }

        #endregion
        ////////////////

        ////////////////
        #region ConvertNameCategoryToId
        public List<long> ConvertNameCategoryToId(List<string> LC)
        {
            db db = new db();
            List<long> Category = new List<long>();
            foreach (var item in db.Categories)
            {
                foreach (var NameCategory in LC)
                {
                    if (item.Name == NameCategory)
                    {
                        Category.Add(item.id);
                    }
                }
            }
            return Category;
        }
        #endregion
        ////////////////


        ////////////////
        #region DeleteInterests
        public bool DeleteInterests(long productid, string userid)
        {
            db db = new db();
            Interests interests = new Interests()
            {
                Productid = productid,
                UserId = userid,
            };
            foreach (var item in db.Interests)
            {
                if (productid == item.Productid && userid == item.UserId)
                {
                    db.Interests.Remove(item);
                }
            }
            db.SaveChanges();
            return true;
        }
        #endregion
        ////////////////

        ////////////////
        #region AddInterests
        public bool AddInterests(long productid, string userid)
        {
            db db = new db();
            Interests interests = new Interests()
            {
                Productid = productid,
                UserId = userid,
            };
            db.Interests.Add(interests);
            db.SaveChanges();
            return true;
        }

        #endregion
        ////////////////

        ////////////////
        #region CalculatePrice
        public async Task<AttributeProduct> CalculatePrice(long productid, string color, string size)
        {
            db db = new db();
            AttributeProduct attributeProduct = new AttributeProduct();
            await foreach (var item in db.AttributeProducts)
            {
                if (item.ProductId == productid)
                {
                    if (color == item.Color && size == item.Size)
                    {
                        attributeProduct = item;
                    }
                }
            }
            return attributeProduct;
        }

        #endregion
        ////////////////
    }
}
