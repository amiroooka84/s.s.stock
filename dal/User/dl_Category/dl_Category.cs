using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal.User.dl_Category
{
    public class dl_Category
    {
        /////////////
        #region ReadNameCategory
        public string ReadNameCategory(long categoryid)
        {
            db db = new db();
            string NameCategory = "";
            foreach (var item in db.Categories)
            {
                if (categoryid == item.id)
                {
                    NameCategory = item.Name;
                }
            }
            return NameCategory;
        }
        #endregion
        /////////////

        /////////////
        #region ReadCategory
        public List<Category> ReadCategory()
        {
            db db = new db();
            return db.Categories.ToList();
        }
        #endregion
        /////////////


    }
}
