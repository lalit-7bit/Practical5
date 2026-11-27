using Microsoft.AspNetCore.Mvc;
using Practical5.Models;

namespace Practical5.Controllers
{
    public class LeaveController : Controller
    {
        private string[] AcademicEvents =
        {
            "Sports Day",
            "Technical Fest",
            "Annual Day",
            "Mid Semester Exam",
            "End Semester Exam",
            "Cultural Fest"
        };

        private string[] LeaveTypes =
        {
            "Medical Leave",
            "Personal Leave",
            "Emergency Leave",
            "Other"
        };

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.AcademicEvents = AcademicEvents;
            ViewBag.LeaveTypes = LeaveTypes;

            ViewBag.SavedName = Request.Cookies["StudentName"];

            return View(new LeaveApplication());
        }

        [HttpPost]
        public IActionResult Index(LeaveApplication model)
        {
            ViewBag.AcademicEvents = AcademicEvents;
            ViewBag.LeaveTypes = LeaveTypes;

            if (model.StartDate > model.EndDate)
            {
                ModelState.AddModelError(
                    "EndDate",
                    "End date must be after start date."
                );
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Response.Cookies.Append(
                "StudentName",
                model.StudentName,
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(30)
                }
            );

            ViewBag.Message = "Leave application submitted successfully!";
            ViewBag.SavedName = model.StudentName;

            return View(model);
        }
    }
}