using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class CateDao
    {
        private DocTinTucEntities db = null;


        public CateDao()
        {
            db = new DocTinTucEntities();
        }

        public List<CHUYENMUC> ListCate()
        {
            return db.CHUYENMUCs.ToList();
        }


        public IEnumerable<CHUYENMUC> ListAll(int page, int pageSize)
        {
            return db.CHUYENMUCs.OrderByDescending(i => i.IDCM).ToPagedList(page, pageSize);
        }

        public long addCate(CHUYENMUC tk)
        {
            db.CHUYENMUCs.Add(tk);
            db.SaveChanges();
            return tk.IDCM;
        }


        public bool Delete(int ID)
        {
            try
            {
                var acc = db.CHUYENMUCs.SingleOrDefault(x => x.IDCM == ID);
                db.CHUYENMUCs.Remove(acc);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
