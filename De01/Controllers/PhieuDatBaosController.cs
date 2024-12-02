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
    public class PhieuDatBaosController : Controller
    {
        private QLDBEntities db = new QLDBEntities();

        // GET: PhieuDatBaos
        public ActionResult Index()
        {
            return View(db.PhieuDatBaos.ToList());
        }

        // GET: PhieuDatBaos/Details/5
        public ActionResult Details(int MaKH, String TenBao)
        {
            if (TenBao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatBao phieuDatBao = db.PhieuDatBaos.Where(x => (x.TenBao == TenBao && x.MaKH == MaKH)).FirstOrDefault();
            if (phieuDatBao == null)
            {
                return HttpNotFound();
            }
            return View(phieuDatBao);
        }

        // GET: PhieuDatBaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhieuDatBaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenBao,MaKH,NgayDat,Soluong,DonGia")] PhieuDatBao phieuDatBao)
        {
            if (ModelState.IsValid)
                if (ktTrungKhoa(phieuDatBao.TenBao, phieuDatBao.MaKH) == true)
                {
                    db.PhieuDatBaos.Add(phieuDatBao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else ViewBag.TB = "Trùng mã kh và tên báo- Trùng khoá";

            return View(phieuDatBao);
        }
        // Kiểm tra trùng khoá trong bảng PhieuDatBao
        bool ktTrungKhoa(string TenBao, int MaKH)
        {
            // tìm xem tên báo và mã KH này có trong bảng Phiêu đặt báo chưa
            PhieuDatBao pd = db.PhieuDatBaos.Where(x => (x.TenBao == TenBao && x.MaKH == MaKH)).FirstOrDefault();
            if (pd == null) return true; // không trùng khoá
            return false; // trùng khoá
        }

        // GET: PhieuDatBaos/Edit/5
        public ActionResult Edit(int MaKH, string TenBao)
        {
            if (TenBao==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Tìm phiếu đặt báo cần sửa thoả mãn 2 điều kiện  MAKH= Makh, Tenba=Tên mình đưa
            PhieuDatBao pd = db.PhieuDatBaos.Where(x => (x.TenBao == TenBao && x.MaKH == MaKH)).FirstOrDefault();
            if (pd == null)
            {
                return HttpNotFound();
            }
            return View(pd);
        }

        // POST: PhieuDatBaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenBao,MaKH,NgayDat,Soluong,DonGia")] PhieuDatBao phieuDatBao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuDatBao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phieuDatBao);
        }

        // GET: PhieuDatBaos/Delete/5
        public ActionResult Delete(int MaKH, string TenBao)
        {
            if (TenBao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatBao phieuDatBao= db.PhieuDatBaos.Where(x => (x.TenBao == TenBao && x.MaKH == MaKH)).FirstOrDefault();
            if (phieuDatBao == null)
            {
                return HttpNotFound();
            }
            return View(phieuDatBao);
        }

        // POST: PhieuDatBaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int MaKH, string TenBao)
        {
            PhieuDatBao phieuDatBao = db.PhieuDatBaos.Where(x => (x.TenBao == TenBao && x.MaKH == MaKH)).FirstOrDefault();
            db.PhieuDatBaos.Remove(phieuDatBao);
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
