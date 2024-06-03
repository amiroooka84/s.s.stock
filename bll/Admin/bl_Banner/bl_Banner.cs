using dal.Admin.dl_Banner;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace bll.Admin.bl_Banner
{
    public class bl_Banner
    {
        dl_Banner dl_Banner = new dl_Banner();
        public void AddBanner(string path, string link)
        {
            dl_Banner.AddBanner(path , link);
        }

        public List<Banner> ReadBanner()
        {
            return dl_Banner.ReadBanner();
        }

        public void DeleteBanner(int id)
        {
            dl_Banner.DeleteBanner(id);
        }
    }
}
