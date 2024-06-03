using Entity.TrackingCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal.Admin.dl_TrackingCode
{
    public class dl_TrackingCode
    {
        public void AddTrackingCode(string path , string date)
        {
            db db = new db();
            TrackingCode trackingCode = new TrackingCode() {
                Date = date,
                ImagePath = path
            };
            db.TrackingCodes.Add(trackingCode);
            db.SaveChanges();
        }

        public List<TrackingCode> ReadTrackingCodes()
        {
            db db = new db();
            return db.TrackingCodes.ToList();
        }

        public string DeleteTrackingCode(long trackingCodeid)
        {
            db db = new db();
            TrackingCode trackingCode = new TrackingCode();
            foreach (var item in db.TrackingCodes)
            {
                if (item.id == trackingCodeid)
                {
                    trackingCode = item;
                }
            }
            db.TrackingCodes.Remove(trackingCode);
            db.SaveChanges();
            return trackingCode.ImagePath;
        }
    }
}
