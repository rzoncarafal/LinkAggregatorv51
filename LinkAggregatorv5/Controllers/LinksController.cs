using System;
using System.Linq;
using System.Threading.Tasks;
using LinkAggregatorv5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkAggregatorv5.Controllers
{
    public class LinksController : Controller
    {
        private readonly LinkAggregatorv5Context _context;

        public LinksController(LinkAggregatorv5Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Link data  
                var linkData = (from templink in _context.Link where
                                templink.AddDate >= DateTime.Now.Date.AddDays(-5) orderby templink.LikeCount descending
                                select templink);

                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    linkData = (from templink in _context.Link where
                                (templink.Description.Contains(searchValue) || templink.LinkURL.Contains(searchValue)) orderby templink.Description
                                select templink);
                }

                //total number of rows count   
                recordsTotal = linkData.Count();
                //Paging   
                var data = linkData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });

            }
            catch (Exception)
            {
                throw;
            }
        }


        public IActionResult Create()
        {
            var link = new Link{};

            return View(link);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IdLink,AddDate,Description,LikeCount,LinkURL,UpdateDate,WhoAdd")] Link link)
        {
            if (ModelState.IsValid)
            {
                link.IdLink = _context.Link.Max(i => i.IdLink) + 1; //W Internecie jest mnóstwo postów, że nie da się zrobić pola autonumber (najpopularniejsza opcja [DatabaseGenerated(DatabaseGeneratedOption.Identity)])
                link.AddDate = DateTime.Now;
                link.UpdateDate = DateTime.Now;
                link.LikeCount = 0;
                link.WhoAdd = User.Identity.Name;
                _context.Add(link);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index"); 
        }


        public async Task<IActionResult> Edit(int? pIdLink)
        {
            if (pIdLink == null)
            {
                return NotFound();
            }

            var link = await _context.Link.FindAsync(pIdLink);
            if (link == null)
            {
                return NotFound();
            }
            return View(link);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdLink,AddDate,Description,LikeCount,LinkURL,UpdateDate,WhoAdd")] Link link)
        {
            if (ModelState.IsValid)
            {
                link.UpdateDate = DateTime.Today;
                _context.Update(link);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(link);
        }


        public async Task<IActionResult> Like(int? pIdLink)
        {
            if (pIdLink == null)
            {
                return NotFound();
            }

            var link = await _context.Link.FindAsync(pIdLink);
            if (link == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //if (link.WhoAdd == User.Identity.Name)
                //{
                //    ModelState.AddModelError(string.Empty, "You can't like your own links!");
                //    return RedirectToAction("Index");
                //}
                link.LikeCount += 1;
                _context.Update(link);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(link);
        }


        public async Task<IActionResult> Delete(int? pIdLink)
        {
            if (pIdLink == null)
            {
                return NotFound();
            }

            var link = await _context.Link
                .FirstOrDefaultAsync(l => l.IdLink == pIdLink);
            if (link == null)
            {
                return NotFound();
            }

            _context.Link.Remove(link);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Goto(string pAddress)
        {
            var link = "http://" + pAddress;
            return new RedirectResult(link);
        }

    }
}
