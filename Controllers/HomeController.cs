using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using HW3.Models;
using PagedList;

namespace HW3.Controllers
{
    public class HomeController : Controller
    {


        private const int PageSize = 10;
        private LibraryEntities db = new LibraryEntities();

        public async Task<ActionResult> Index(int? studentPage, int? bookPage)
        {
            var students = await db.students.Include(b => b.borrows).ToListAsync();
            var studentPagedList = students.ToPagedList(studentPage ?? 1, PageSize);

            var books = await db.books.Include(a => a.borrows)
                .Include(t => t.types).Include(b => b.authors).ToListAsync();
            var bookPagedList = books.ToPagedList(bookPage ?? 1, PageSize);

            var viewModel = new HomeViewModel
            {
                Students = studentPagedList,
                Books = bookPagedList
            };

            return View(viewModel);
        }







      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}