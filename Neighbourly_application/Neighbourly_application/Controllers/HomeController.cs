using Microsoft.AspNet.Identity;
using Neighbourly_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neighbourly_application.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {

            var userEmail = User.Identity.GetUserName();

            var canvases_TeamLeader = db.Canvas.Where(c => c.TeamLeader.Email == userEmail).ToList();

            var canvases_Participant = db.Canvas.Where(c => c.CParticipants.Any(p => p.Participant.Email == userEmail)).ToList();
            var associate_Canvases = canvases_Participant.Union(canvases_Participant).ToList();

            ViewBag.UserCanvases = canvases_TeamLeader;
            ViewBag.AssociateCanvases = associate_Canvases;

            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult createCanvas()
        {
            return RedirectToAction("Create", "Canvas");
        }

       
    }
}