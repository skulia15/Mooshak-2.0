﻿using Microsoft.AspNet.Identity;
using mooshak_2._0.Controllers;
using mooshak_2._0.Models.ViewModels;
using mooshak_2._0.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace mooshak_2._0.Controllers
{

    [Authorize(Roles = "student")]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            CourseService _courseService = new CourseService(new AssignmentsService());
            //List<CourseViewModel> UserCourse = _courseService.GetCoursesByUser(User.Identity.GetUserId());
            //return View(UserCourse);
            return View();
        }

        public ActionResult Course(int? id)
        {
            if (id.HasValue)
            {
                int realID = id.Value;
                CourseService _courseService = new CourseService(new AssignmentsService());
                CourseViewModel model = _courseService.GetCourseByID(realID);
                return View(model);
            }
            return View("Error");
        }

        public ActionResult Assignment(int? id)
        {
            if (id.HasValue)
            {
                int realID = id.Value;
                AssignmentsService _assignmentService = new AssignmentsService();
                AssignmentViewModel model = _assignmentService.GetAssignmentByID(realID);
                return View(model);
            }
            return View("Error");
        }
     /*   public bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
        */
       
        public ActionResult Submission()
        {

            foreach (string upload in Request.Files)
            {
                //if (!Request.Files[upload].HasFile()) continue;

                string mimeType = Request.Files[upload].ContentType;
                Stream fileStream = Request.Files[upload].InputStream;
                string fileName = Path.GetFileName(Request.Files[upload].FileName);
                string userName = Request.Form["user_name"];
                int fileLength = Request.Files[upload].ContentLength;
                byte[] fileData = new byte[fileLength];
                fileStream.Read(fileData, 0, fileLength);

                const string connect = @"Data Source=hrnem.ru.is;Initial Catalog=VLN2_2016_H17;User ID=VLN2_2016_H17_usr;Password=tinynight17";
                using (var conn = new SqlConnection(connect))
                {
                    var qry = "INSERT INTO Submissions (FileContent, MimeType, FileName, UserName) VALUES (@FileContent, @MimeType, @FileName,@UserName)";
                    var cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@FileContent", fileData);
                    cmd.Parameters.AddWithValue("@MimeType", mimeType);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return View();
        }
       

    }
}