using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW3.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using PagedList;




namespace HW3.Controllers
{
    public class MaintainController : Controller
    {
        private const int PageSize = 10;
            private LibraryEntities db = new LibraryEntities();

            public async Task<ActionResult> Maintain(int? authorPage, int? typePage, int? borrowPage)
            {
                var authors = await db.authors.Include(b => b.books).ToListAsync();
                var authorPagedList = authors.AsQueryable().ToPagedList(authorPage ?? 1, PageSize);

                var types = await db.types.Include(b => b.books).ToListAsync();
                var typePagedList = types.ToPagedList(typePage ?? 1, PageSize);

                var borrows = await db.borrows.Include(s => s.students)
                    .Include(b => b.books).ToListAsync();
                var borrowPagedList = borrows.ToPagedList(borrowPage ?? 1, PageSize);

                var viewModel = new MaintainViewModel
                {
                    Authors = authorPagedList,
                    Types = typePagedList,
                    Borrows = borrowPagedList
                };

                return View(viewModel);
            }



        // GET: Maintain
        public ActionResult Index()
        {
            return View();
        }
    }
}