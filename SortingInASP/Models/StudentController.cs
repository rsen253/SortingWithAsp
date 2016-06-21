using SortingInASP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SortingInASP.Models
{
    public class StudentController : Controller
    {
        private DataBaseContext db = new DataBaseContext();
        // GET: Student
        //public ActionResult Index()
        //{
        //    return View(db.Student.ToList());
        //}
        
        //[HttpPost]
        public ActionResult Index(string SortingOrder, string SearchData)
        {
            ViewBag.SortingFirstName = String.IsNullOrEmpty(SortingOrder) ? "FirstName" : "";
            ViewBag.SortingLastName = String.IsNullOrEmpty(SortingOrder) ? "LastName" : "";
            var studentList = from s in db.Student.OrderBy(s => s.FirstName) select s;
            if (SearchData != null && SearchData != "")
            {
                studentList = studentList.Where(stu => stu.FirstName.ToUpper().Contains(SearchData.ToUpper())
            || stu.LastName.ToUpper().Contains(SearchData.ToUpper()));
            }
            else if(SearchData == null || SearchData == "")
            {
                studentList = from s in db.Student.OrderBy(s => s.FirstName) select s;
            }
            switch (SortingOrder)
            {
                case "FirstName" :
                    studentList = studentList.OrderByDescending(s => s.FirstName);
                    break;
                case "LastName":
                    studentList = studentList.OrderByDescending(s => s.LastName);
                    break;
                default:
                    break;
            }
            return View(studentList.ToList());
        }
        

    }
}