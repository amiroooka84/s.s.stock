using dal.User.dl_Category;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace bll.User.bl_Category
{
    public class bl_Category
    {
        dl_Category dl_Category = new dl_Category();
        public string ReadNameCategory(long categoryid)
        {
            return dl_Category.ReadNameCategory(categoryid);
        }

        public List<Category> ReadCategory()
        {
            return dl_Category.ReadCategory();
        }
    }
}
