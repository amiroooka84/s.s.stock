using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal.Admin.dl_Category
{
    public class dl_Category
    {
        //////////
        #region Exists
        public bool ExistCategoryById(long Categoryid)
        {
            db db = new db();
            int i = 0;
            foreach (var item in db.Categories)
            {
                if (item.id == Categoryid)
                {
                    i = 1;
                    break;
                }
            }
            if (Categoryid == 0)
            {
                return true;
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

        public bool ExistCategoryByName(string CategoryName)
        {
            db db = new db();
            int i = 0;
            foreach (var item in db.Categories)
            {
                if (item.Name == CategoryName)
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

        public bool ExistCategoryByNameForEdit(string CategoryName, int id)
        {
            db db = new db();
            int i = 0;
            foreach (var item in db.Categories)
            {
                if (item.id == id)
                {

                }
                else if (item.Name == CategoryName)
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

        public List<long> ReadSubsetCategory(long id)
        {
            db db = new db();
            List<long> categories = new List<long>();
            foreach (var item in db.Categories)
            {
                if (item.Categoryid == id)
                {
                    categories.AddRange(ReadSubsetCategory(item.id));
                }
                else if (item.id == id)
                {
                    categories.Add(item.id);
                }
            }
            return categories;
        }

        public bool DiscountForCategory(long categoryId, int discount)
        {
            db db = new db();
            List<long> CategoryId = new List<long>();
            CategoryId.AddRange(ReadSubsetCategory(categoryId));
            foreach (var product in db.Products)
            {
                foreach (var item in CategoryId)
                {
                    if (product.Categoryid == item)
                    {
                        product.Discount = discount;
                    }
                }
                    //List<Category> categories1 = new List<Category>();
                    //foreach (var categoryid in CategoryId)
                    //{
                    //    categories1.AddRange(ReadSubsetCategory(categoryid));
                    //}

                    //foreach (var item in categories1)
                    //{
                    //    if (item.id == product.Categoryid)
                    //    {
                            
                    //        break;
                    //    }
                    //}
            }
            db.SaveChanges();
            return true;
        }
        #endregion
        //////////

        /////////
        #region add
        public bool AddCategory(Category Category)
        {
            db db = new db();
            db.Categories.Add(Category);
            db.SaveChanges();
            return true;
        }
        #endregion 
        /////////

        /////////
        #region Reads
        public List<Category> ReadCategory()
        {
            db db = new db();
            return db.Categories.ToList();
        }

        public Category ReadCategoryById(int id)
        {
            db db = new db();
            Category category = new Category();
            foreach (var item in db.Categories)
            {
                if (item.id == id)
                {
                    category = item;
                }
            }
            return category;
        }
        #endregion
        /////////

        /////////
        #region Search
        public List<Category> SearchCategory(string name)
        {
            db db = new db();
            List<Category> lc = new List<Category>();
            foreach (var item in db.Categories)
            {
                if (name.Contains(item.Name))
                {
                    lc.Add(item);
                }
            }
            return lc;
        }
        #endregion
        /////////

        /////////
        #region Edit
        public bool EditCategory(Category category, int id)
        {
            db db = new db();
            var result = db.Categories.SingleOrDefault(b => b.id == id);
            result.Name = category.Name;
            if (category.ImagePath != null)
            {
                result.ImagePath = category.ImagePath;
            }
            result.Categoryid = category.Categoryid;
            db.SaveChanges();
            return true;
        }
        #endregion
        /////////

        /////////
        #region Delete
        public bool DeleteCategory(int id)
        {
            db db = new db();
            if (!ExistCategoryById(id))
            {
                return false;
            }
            else
            {
                Category category = new Category();
                category = ReadCategoryById(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
        }
        #endregion
        /////////
    }
}
