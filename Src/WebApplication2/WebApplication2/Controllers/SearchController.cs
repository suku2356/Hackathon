using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        
       {

           var search = new List<SearchMaster>();

           using (SearchEntities dc = new SearchEntities())
           {

               search = dc.SearchMasters.ToList();
           
           }
            
            return View(search);
        }

	}
}