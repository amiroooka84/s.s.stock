using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal.Admin.dl_Banner
{
    public class dl_Banner
    {
        /////////////
        #region AddBanner
        public void AddBanner(string path, string link)
        {
            db db = new db();
            Banner banner = new Banner();
            banner.ImagePath = path;
            banner.link = link;
            db.Banners.Add(banner);
            db.SaveChanges();
        }

        public void DeleteBanner(int id)
        {
            db db = new db();
            Banner banner = new Banner();
            foreach (var item in db.Banners)
            {
                if (item.id == id)
                {
                    banner = item;
                }
            }
            db.Banners.Remove(banner);
            db.SaveChanges();
        }
        #endregion
        /////////////

        /////////////
        #region ReadBanner
        public List<Banner> ReadBanner()
        {
            db db = new db();
            return db.Banners.ToList();
        }
        #endregion
        /////////////
    }
}
