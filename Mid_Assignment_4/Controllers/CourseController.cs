using Mid_Assignment_4.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mid_Assignment_4.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            var db = new MidAssignment4Entities1();
            var AllCourses = db.Courses.ToList();
            return View(AllCourses);
        }



        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Cours cs)
        {
            var db = new MidAssignment4Entities1();
            db.Courses.Add(cs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new MidAssignment4Entities1();
            var ext = (from cs in db.Courses
                       where cs.Id == id
                       select cs).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult Edit(Cours c)
        {
            var db = new MidAssignment4Entities1();
            var ext = (from cs in db.Courses
                       where cs.Id == c.Id
                       select cs).SingleOrDefault();
            ext.Name = c.Name;
            ext.PreReq = c.PreReq;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new MidAssignment4Entities1();
            var ext = (from cs in db.Courses
                       where cs.Id == id
                       select cs).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult Delete(Cours c)
        {
            var db = new MidAssignment4Entities1();
            var ext = (from cs in db.Courses
                       where cs.Id == c.Id
                       select cs).SingleOrDefault();
            db.Courses.Remove(ext);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult PreRegistration()
        {
            var db = new MidAssignment4Entities1();

            var mapTable = (from cs in db.CourseStudents
                            where (cs.StudentId == 1)
                            select cs).ToList();

            var courses = db.Courses;
            List<Cours> courseList = new List<Cours>();

            foreach (var c in courses)
            {
                foreach (var studentTakenCourse in mapTable)
                {
                    if(c.Id == studentTakenCourse.CourseId && (studentTakenCourse.Grade == "W" || studentTakenCourse.Marks < 60) && c.StCount < 40)
                    {
                        courseList.Add(c);
                        break;
                    }
                    else if(c.PreReq == studentTakenCourse.CourseId && studentTakenCourse.Status == "Complete" && c.StCount < 40)
                    {
                        courseList.Add(c);
                        break;
                    }
                }
            }
            return View(courseList);
        }
        [HttpPost]
        public ActionResult PreRegistration(int[] courses)
        {
            //Console.Write(courses.Length);

            var db = new MidAssignment4Entities1();

            if(courses.Length < 6)
            {
                foreach (var id in courses)
                {
                    db.CourseStudents.Add(new CourseStudent()
                    {
                        CourseId = id,
                        StudentId = 1,
                        Status = "Enrolled",
                        Grade = "N/A",
                        Marks = 420
                    });

                    var ext = (from cs in db.Courses
                               where cs.Id == id
                               select cs).SingleOrDefault();
                    ext.StCount = ext.StCount + 1;
                    db.SaveChanges();
                }
                db.SaveChanges();
                return RedirectToAction("PreRegistration");
            }

            TempData["msg"] = "Can not take more than 5 courses";
            return RedirectToAction("PreRegistration");
        }
    }
}