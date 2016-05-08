﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using mooshak_2._0.Models;
using mooshak_2._0.Models.Entities;
using Microsoft.AspNet.Identity;
using mooshak_2._0.Models.ViewModels;
using System.Security.Principal;

namespace mooshak_2._0.Services
{
    public class CourseService
    {
        private Dbcontext _db = new Dbcontext();
        private AssignmentsService _assignmentsService;

        public CourseService(AssignmentsService assignmentsService)
        {
            _assignmentsService = new AssignmentsService();
        }

        public CourseService()
        {
        }
        
        public List<CourseViewModel> GetAllCourses()
        {
            // TODO: 
            

            List<Course> allCourses = (from courses in _db.courses select courses).ToList();

            List<CourseViewModel> courseViewModelList = new List<CourseViewModel>();

            foreach (var course in allCourses)
            {
                CourseViewModel courseViewModel = new CourseViewModel();
                courseViewModel.Title = course.name;
                courseViewModel.Assignments = _assignmentsService.GetAssignmentsInCourse(course.id);
                courseViewModel.ID = course.id;
                    //teacher
                
                courseViewModelList.Add(courseViewModel);
            }
            return courseViewModelList;
            
        }
        
        public CourseViewModel GetCourseByID(int CourseID)
        {
            //Gets an assignment link by the assignmentID to the database ( a single assignment will be recived "single or default")
            var course = _db.courses.SingleOrDefault(x => x.id == CourseID);
            if (course == null)
            {
                //TODO: throw an exeption, an error has occured
            }
            //Does the assignment contain any milestones ?(can return many milestones if the assignment contains multiple parts)
            //Here we recive only the milestone for the assignment with the correct AssignmentID
            var assignments = _db.assignments.Where(x => x.courseId == CourseID)
                .Select(x => new AssignmentViewModel { Title = x.title }).ToList();

            //create a viewmodel fot the assignment that has a milestone
            var viewModel = new CourseViewModel();
            {
                viewModel.Title = course.name;
                viewModel.Assignments = assignments;
            };
            return viewModel;
        }

        public List<CourseViewModel> GetCoursesByStudent(string id)
        {
            IEnumerable<StudentCourse> AllStudentCourses =  from courses in _db.studentcourse
                                                            where courses.studentId.Equals(id)
                                                            select courses;
            
            List<CourseViewModel> studentCourses = new List<CourseViewModel>();
            foreach(var item in AllStudentCourses)
            {
                CourseViewModel tmpModel = GetCourseByID(item.courseId);
                studentCourses.Add(tmpModel);
            }
            return studentCourses;
        }

    }
}