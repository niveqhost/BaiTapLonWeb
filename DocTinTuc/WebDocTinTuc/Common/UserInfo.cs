using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDocTinTuc
{
    [Serializable]
    public class UserInfo
    {
        public long UserID { set; get; }
        public string Username { set; get; }
    }
}