using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using De01.Models;

namespace De01.Controllers
{
    public class KhachHangsController : Controller
    {
        private QLDBEntities db = new QLDBEntities();

        // GET: KhachHangs
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }

        // GET: KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }
        // Viết action TimkiemDC 
        public ActionResult TimkiemDC(string txtDiachi)
        {
            var sql = (from phieu in db.PhieuDatBaos
                       join khach in db.KhachHangs on phieu.MaKH equals khach.MaKH
                       where khach.Diachi.Contains(txtDiachi)
                       select new DLTimkiem
                       {
                           MaKH = khach.MaKH,
                           TenKH = khach.TenKH,
                           Diachi = khach.Diachi,
                           TenBao = phieu.TenBao,
                           Ngaydat = phieu.NgayDat,
                           Soluong = phieu.Soluong,
                           Dongia = phieu.DonGia
                       }).ToList();
            return View("TimkiemDC",sql);
        }
        // TongtienMax
        public ActionResult TongtienMax()
        {
            var sql = (from phieu in db.PhieuDatBaos
                       join khach in db.KhachHangs on phieu.MaKH equals khach.MaKH
                       let thanhtien = phieu.Soluong * phieu.DonGia
                       group new { khach, thanhtien } by new
                       {
                           khach.MaKH,
                           khach.TenKH

                       } into g
                       select new DLTongtienMax
                       {
                           MaKH = g.Key.MaKH,
                           TenKH = g.Key.TenKH,
                           Tongtien = g.Sum(x => x.thanhtien)
                       }).OrderByDescending(x => x.Tongtien).ToList();
            // cần lấy thông tin của người có tổng tiền lớn nhất
            if(sql==null||!sql.Any())
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = sql.First().MaKH;
            ViewBag.TenKH = sql.First().TenKH;
            ViewBag.Tongtien = sql.First().Tongtien;
            return View();
        }
   
        // GET: KhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,TenKH,Diachi,Gioitinh")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
                if (ktTrungMaKH(khachHang.MaKH))
                {
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else ViewBag.TB = "trùng mã khách hàng";

            return View(khachHang);
        }
        // Viết hàm kiểm tra trùng mã khách hàng 
        bool ktTrungMaKH(int MaKH)
        {
            KhachHang kh = db.KhachHangs.FirstOrDefault(x => x.MaKH == MaKH);
            if (kh == null) return true; // k trùng
            return false; // trùng
        }
        // GET: KhachHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenKH,Diachi,Gioitinh")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
