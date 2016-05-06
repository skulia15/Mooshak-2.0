﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mooshak_2._0.Services;
using mooshak_2._0.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using mooshak_2._0.Models.Entities;
using System.Security.Claims;

namespace mooshak_2._0.Controllers
{
    public class CourseController : Controller
    {
        private CourseService _service = new CourseService();
        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }

        //the method that is called to see details for an assignment
        public ActionResult Details(int id)
        {
            var viewModel = _service.GetCourseByID(id); // creates a viewmodel for the assignment
            return View(viewModel);
        }


        public ActionResult CourseIndex()
        {
            var db = new Dbcontext();
            return View(db.courses.ToList());
        }

        [HttpPost]
        public ActionResult CourseIndex(Course list)
        {
        
            if (ModelState.IsValid)
            {

                using (var db = new Dbcontext())
                {
                    string new_item = Request.Form["new_item"];
                    var dbList = db.courses.Create();
                    dbList.name = new_item;
                    db.courses.Add(dbList);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect format has been placed");

            }
            var listTable = new Dbcontext();
            return View(listTable.courses.ToList());
        }
    }
}