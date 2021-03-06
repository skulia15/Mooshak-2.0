﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using mooshak_2._0.Models;
using mooshak_2._0.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using mooshak_2._0.Services;

namespace mooshak_2._0.Controllers
{
    public class HomeController : Controller
    {
        readonly CourseService _courseService = new CourseService(new AssignmentsService());

        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("student"))
            {
                return RedirectToAction("Index", "Student");
            }
            else if (User.IsInRole("teacher"))
            {
                return RedirectToAction("Index", "Teacher");
            }
            else if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                //Throw exception here
                return View();
            }
            
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application admin description page.";

            return View();
        }

        public ActionResult Help()
        {
            ViewBag.Message = "Help page";

            return View();
        }
        public ActionResult Error()
        {
            ViewBag.Message = "Error page";

            return View();
        }

        public ActionResult Navbar()
        {
            var model = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            return PartialView("~/Views/Shared/_HeaderBarLayout.cshtml", model);
        }
    }
}