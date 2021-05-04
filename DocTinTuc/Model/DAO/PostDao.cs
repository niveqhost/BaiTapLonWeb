﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class PostDao
    {
        private DocTinTucEntities db = null;
        public PostDao()
        {
            db = new DocTinTucEntities();
        }



        //Thêm mới bài đăng
        public long CreatePost(BAIDANG bd)
        {
            db.BAIDANGs.Add(bd);
            db.SaveChanges();
            return bd.IDBaiDang;
        }
        public bool updatePost(BAIDANG tk)
        {
            try
            {
                var tk1 = db.BAIDANGs.SingleOrDefault(x => x.IDBaiDang == tk.IDBaiDang);
                tk1.TenBaiDang = tk.TenBaiDang;
                tk1.TieuDe = tk.TieuDe;
                tk1.NoiDung = tk.NoiDung;
                tk1.AnhDaiDien = tk.AnhDaiDien;
                tk1.IDTheLoai = tk.IDTheLoai;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Lấy tất cả bài đăng đã được duyệt
        public IEnumerable<BAIDANG> ListAll(string searchString, int page, int pageSize)
        {
            IQueryable<BAIDANG> model = db.BAIDANGs.Where(x => x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenBaiDang.Contains(searchString) || x.TieuDe.Contains(searchString));
            }
            return model.OrderByDescending(x => x.IDBaiDang).ToPagedList(page, pageSize);
        }

        //Lấy tất cả bài đăng chờ duyệt
        public IEnumerable<BAIDANG> ListAllWaiting(string searchString, int page, int pageSize)
        {
            IQueryable<BAIDANG> model = db.BAIDANGs.Where(x => x.TrangThaiBaiDang.ToUpper() == "CHỜ DUYỆT");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenBaiDang.Contains(searchString) || x.TieuDe.Contains(searchString));
            }
            return model.OrderByDescending(x => x.IDBaiDang).ToPagedList(page, pageSize);
        }

        //Lấy thông tin bài đăng theo ID bài đăng
        public List<BAIDANG> NewsFindByID(long id)
        {
            return db.BAIDANGs.Where(x => x.IDBaiDang == id).ToList();
        }

        public BAIDANG ViewDetail(int id)
        {
            return db.BAIDANGs.Find(id);
        }

        //Lấy thông tin bài đưng cùng thể loại
        public IEnumerable<BAIDANG> ListAllPostForType(string UrlRequire, int page, int pageSize)
        {
            IQueryable<BAIDANG> model = db.BAIDANGs.Where(x => x.THELOAI.UrlRequire == UrlRequire && x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Skip(0);
            return model.ToPagedList(page, pageSize);
        }

        //Lấy thông tin bài đăng theo tên người đăng
        public IEnumerable<BAIDANG> ListAllPostForPoster(int id, int page, int pageSize)
        {
            IQueryable<BAIDANG> model = db.BAIDANGs.Where(x => x.IDTaiKhoan == id && x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Skip(0);
            return model.ToPagedList(page, pageSize);
        }

        //Lấy tất cả bài đăng ra
        public List<BAIDANG> AllPost()
        {
            return db.BAIDANGs.OrderByDescending(x => x.IDBaiDang).ToList();
        }

        //Lấy ra bài đăng mới nhất TOP1
        public List<BAIDANG> LastestNews()
        {
            return db.BAIDANGs.Where(x => x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Take(1).ToList();
        }

        //Lấy ra bài đăng thứ 2 3 4 5
        public List<BAIDANG> SecondToFourthNews()
        {
            return db.BAIDANGs.Where(x => x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Take(5).Skip(1).ToList();
        }

        //Lấy tất cả bài đăng theo ID tài khoản
        public IEnumerable<BAIDANG> MyPost(string searchString, int page, int pageSize, int idtk)
        {
            IQueryable<BAIDANG> model = db.BAIDANGs.Where(x => x.IDTaiKhoan == idtk).OrderBy(x => x.TrangThaiBaiDang);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenBaiDang.Contains(searchString) || x.TieuDe.Contains(searchString));
            }
            return model.OrderByDescending(x => x.IDBaiDang).ToPagedList(page, pageSize);

        }

        //Xóa bài đăng
        public bool Delete(int ID)
        {
            try
            {
                var acc = db.BAIDANGs.SingleOrDefault(x => x.IDBaiDang == ID);
                List<LUOTXEM> lxbd = db.LUOTXEMs.Where(x => x.IDBaiDang == ID).ToList();

                //Xóa lượt xem trước khi xóa bài đăng
                if (lxbd.Count() != 0)
                {
                    foreach (LUOTXEM item in lxbd)
                    {
                        db.LUOTXEMs.Remove(item);
                        db.SaveChanges();
                    }
                }

                //Xóa bài đăng sau khi xóa lượt xem
                db.BAIDANGs.Remove(acc);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //Lay tat ca bai dang the loai tinh yeu
        public List<BAIDANG> postLove()
        {
            return db.BAIDANGs.Where(x => x.IDTheLoai == 1 && x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Take(4).ToList();
        }

        //doi song
        public List<BAIDANG> postDoiSong()
        {
            return db.BAIDANGs.Where(x => x.IDTheLoai == 2 && x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Take(4).ToList();
        }

        //phap luat
        public List<BAIDANG> postPhapLuat()
        {
            return db.BAIDANGs.Where(x => x.IDTheLoai == 3 && x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Take(4).ToList();
        }

        //suc khoe
        public List<BAIDANG> postSucKhoe()
        {
            return db.BAIDANGs.Where(x => x.IDTheLoai == 4 && x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Take(4).ToList();
        }

        //the thao
        public List<BAIDANG> postTheThao()
        {
            return db.BAIDANGs.Where(x => x.IDTheLoai == 5 && x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Take(4).ToList();
        }

        //Lay top 10 bai bao cung the loai
        public List<BAIDANG> top10Relative(long id)
        {
            var idtl = db.BAIDANGs.Find(id).IDTheLoai;
            return db.BAIDANGs.Where(x => x.IDBaiDang != id && x.IDTheLoai == idtl && x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Take(10).ToList();
        }

        //Lay top 10 bai bao cung người đăng
        public List<BAIDANG> top10CungNguoiDang(long id)
        {
            var idtk = db.BAIDANGs.Find(id).IDTaiKhoan;
            return db.BAIDANGs.Where(x => x.IDBaiDang != id && x.IDTaiKhoan == idtk && x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderByDescending(x => x.IDBaiDang).Take(5).ToList();
        }

        //Lay top 10 bai bao xem nhieu
        private Random c = new Random();
        public List<BAIDANG> top10VIew()
        {
            return db.BAIDANGs.Where(x => x.TrangThaiBaiDang.ToUpper() == "ĐÃ DUYỆT").OrderBy(x => x.TenBaiDang).Take(10).ToList();
        }




        //Lấy thể loại bài đăng
        public List<THELOAI> theloaibaidang(long id)
        {
            BAIDANG idtl = db.BAIDANGs.Where(x => x.IDBaiDang == id).SingleOrDefault();
            return db.THELOAIs.Where(x => x.IDTheLoai == idtl.IDTheLoai).ToList();
        }



        //Lấy thông tin thể loại theo URL REQUIRE
        public List<THELOAI> theloaibaidangtentheloai(string id)
        {
            return db.THELOAIs.Where(x => x.UrlRequire == id).ToList();
        }

        //DUYỆT BÀI
        public void DuyetBai(long id)
        {
            BAIDANG bd1 = db.BAIDANGs.Find(id);
            bd1.TrangThaiBaiDang = "Đã Duyệt";
            db.SaveChanges();
        }

        public void BoDuyetBai(long id)
        {
            BAIDANG bd1 = db.BAIDANGs.Find(id);
            bd1.TrangThaiBaiDang = "CHỜ DUYỆT";
            db.SaveChanges();
        }

        //Lấy thông tin người đăng theo id bài đăng
        public List<TAIKHOAN> GetInfoPoster(long id)
        {
            var bd1 = db.BAIDANGs.Find(id);
            var idtk = bd1.IDTaiKhoan;
            return db.TAIKHOANs.Where(x => x.IDTaiKhoan.ToString() == idtk.ToString()).ToList();
        }
    }
}
