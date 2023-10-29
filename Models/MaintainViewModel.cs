using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PagedList;


namespace HW3.Models
{
    public class MaintainViewModel
    {

        public IPagedList<authors> Authors { get; set; }
        public IPagedList<types> Types { get; set; }
        public IEnumerable<books> Books { get; set; }
        public IPagedList<borrows> Borrows { get; set; }

    }
}