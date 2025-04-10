using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.Models.Process;

using System.Data;
using OfficeOpenXml;

namespace DemoMVC.Controllers
{
    public class DaiLyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public DaiLyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DaiLy
        public async Task<IActionResult> Index()
        {
            return View(await _context.DaiLy.ToListAsync());
        }

        // GET: DaiLy/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daiLy = await _context.DaiLy
                .FirstOrDefaultAsync(m => m.MaDaiLy == id);
            if (daiLy == null)
            {
                return NotFound();
            }

            return View(daiLy);
        }

        // GET: DaiLy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DaiLy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDaiLy,TenDaiLy,DiaChi,NguoiDaiDien,DienThoai,MaHTPP")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(daiLy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(daiLy);
        }

        // GET: DaiLy/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daiLy = await _context.DaiLy.FindAsync(id);
            if (daiLy == null)
            {
                return NotFound();
            }
            return View(daiLy);
        }

        // POST: DaiLy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDaiLy,TenDaiLy,DiaChi,NguoiDaiDien,DienThoai,MaHTPP")] DaiLy daiLy)
        {
            if (id != daiLy.MaDaiLy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(daiLy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DaiLyExists(daiLy.MaDaiLy))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(daiLy);
        }

        // GET: DaiLy/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daiLy = await _context.DaiLy
                .FirstOrDefaultAsync(m => m.MaDaiLy == id);
            if (daiLy == null)
            {
                return NotFound();
            }

            return View(daiLy);
        }

        // POST: DaiLy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var daiLy = await _context.DaiLy.FindAsync(id);
            if (daiLy != null)
            {
                _context.DaiLy.Remove(daiLy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DaiLyExists(string id)
        {
            return _context.DaiLy.Any(e => e.MaDaiLy == id);
        }
        public async Task<IActionResult> Upload()
        {
        return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file!=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension !=".xlx" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("","Please choose excel file to upload!");
                }
                else 
                {
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var filelocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        DataTable dt = (DataTable)_excelProcess.ExcelToDataTable(filelocation);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var ps = new DaiLy();
                            ps.MaDaiLy = dt.Rows[i][0].ToString();
                            ps.TenDaiLy = dt.Rows[i][1].ToString();
                            ps.DiaChi = dt.Rows[i][2].ToString();
                            ps.NguoiDaiDien = dt.Rows[i][3].ToString();
                            ps.DienThoai = dt.Rows[i][4].ToString();
                            ps.MaHTPP = dt.Rows[i][5].ToString();
                            _context.Add(ps);
                        
                        }
                        await _context.SaveChangesAsync();
                        return RediRectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
        private IActionResult RediRectToAction(string v)
        {
            throw new NotImplementedException();
        }
        public IActionResult Download()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Person");
            var fileName = "YouFileName"+ ".xlsx";
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                //add some text to cell A1
                worksheet.Cells["A1"].Value = "MaDaiLy";
                worksheet.Cells["B1"].Value = "TenDaiLy";
                worksheet.Cells["C1"].Value = "DiaChi";
                worksheet.Cells["D1"].Value = "NguoiDaiDien";
                worksheet.Cells["E1"].Value = "DienThoai";
                worksheet.Cells["F1"].Value = "MaHTPP";
                //get all DayLy
                var daiLyList = _context.DaiLy.ToList();
                //fill data to worksheet
                worksheet.Cells["A2"].LoadFromCollection(daiLyList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                //download file
                return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
            }
        }
    }
}
