using System;
using System.Collections.Generic;
using System.Text;
using dal.Admin.dl_Category;
using Entity;

namespace bll.Admin.bl_Category
{
    public class bl_Category
    {
        dl_Category dl_Category = new dl_Category();
        public bool ExistCategoryById(long Categoryid)
        {
            return dl_Category.ExistCategoryById(Categoryid);
        }
        public bool ExistCategoryByNameForEdit(string CategoryName , int id)
        {
            return dl_Category.ExistCategoryByNameForEdit(CategoryName , id);
        }
        public bool ExistCategoryByName(string CategoryName)
        {
            return dl_Category.ExistCategoryByName(CategoryName);
        }

        public bool AddCategory(Category category)
        {
            return dl_Category.AddCategory(category);
        }

        public bool EditCategory(Category category , int id)
        {
            return dl_Category.EditCategory(category , id);
        }

        public List<Category> ReadCategory()
        {
            return dl_Category.ReadCategory();
        }
        public Category ReadCategoryById(int id)
        {
            return dl_Category.ReadCategoryById(id);
        }

        public List<Category> SearchCategory(string name)
        {
            return dl_Category.SearchCategory(name);
        }

        public bool DeleteCategory(int id)
        {
            return dl_Category.DeleteCategory(id);
        }

        public bool DiscountForCategory(long categoryId, int discount)
        {
            return dl_Category.DiscountForCategory(categoryId, discount);
        }
    }
}
