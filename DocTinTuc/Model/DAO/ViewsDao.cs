using Model.EntityFramework;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ViewsDao
    {
        private DocTinTucDataContext db = null;
        public ViewsDao()
        {
            db = new DocTinTucDataContext();
        }


        public void AddViews(long id)
        {
            LUOTXEM lx = new LUOTXEM();
            lx.IDBaiDang = Convert.ToInt32(id);
            lx.NgayThang = DateTime.Now;
            db.LUOTXEMs.Add(lx);
            db.SaveChanges();
        }
    }
}
