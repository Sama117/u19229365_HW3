using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW3.Models
{
    public class HomeViewModel
    {
        public IPagedList<students> Students { get; set; }
        public IPagedList<books> Books { get; set; }
        public IPagedList<borrows> Borrows { get; set; }

    }
}