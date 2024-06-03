using dal.Admin.dl_TrackingCode;
using Entity.TrackingCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace bll.Admin.bl_TrackingCode
{
    public class bl_TrackingCode
    {
        dl_TrackingCode dl_TrackingCode = new dl_TrackingCode();
        public void AddTrackingCode(string path , string date)
        {
            dl_TrackingCode.AddTrackingCode(path , date);
        }

        public List<TrackingCode> ReadTrackingCodes()
        {
            return dl_TrackingCode.ReadTrackingCodes();
        }

        public string DeleteTrackingCode(long trackingCodeid)
        {
           return dl_TrackingCode.DeleteTrackingCode(trackingCodeid);
        }
    }
}
