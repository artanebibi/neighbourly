using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Neighbourly_application.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;

namespace Neighbourly_application.Controllers
{
    public class CanvasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Canvas
        public ActionResult Index()
        {
            // Eager load Participants via CanvasParticipants
            var canvases = db.Canvas
                .Include(c => c.CParticipants.Select(cp => cp.Participant))
                .ToList();

            return View(canvases);
        }

        // GET: Canvas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var canvas = db.Canvas
                .Include(c => c.CParticipants.Select(cp => cp.Participant))
                .SingleOrDefault(c => c.Id == id);

            if (canvas == null)
            {
                return HttpNotFound();
            }

            return View(canvas);
        }

        // GET: Canvas/Create
        public ActionResult Create()
        {

            var teamLeadEmail = User.Identity.GetUserName();
            var teamLead = db.Participants.FirstOrDefault(p => p.Email == teamLeadEmail);

            if (teamLead != null)
            {
                ViewBag.TeamLeaderId = new SelectList(db.Participants, "Id", "Name", teamLead.Id);
            }

            ViewBag.Participants = new MultiSelectList(db.Participants, "Id", "Name");
            ViewBag.TeamLeaderId = new SelectList(db.Participants, "Id", "Name");

            return View();
        }

        // POST: Canvas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,TeamLeaderId,SelectedParticipants")] CanvasViewModel canvasViewModel)
        {
            if (ModelState.IsValid)
            {
                var canvas = new Canvas
                {
                    Name = canvasViewModel.Name,
                    Description = canvasViewModel.Description,
                    TeamLeaderId = canvasViewModel.TeamLeaderId,
                    CParticipants = canvasViewModel.SelectedParticipants?.Select(participantId => new CanvasParticipant
                    {
                        ParticipantId = participantId
                    }).ToList()
                };

                db.Canvas.Add(canvas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamLeaderId = new SelectList(db.Participants, "Id", "Name", canvasViewModel.TeamLeaderId);
            ViewBag.Participants = new MultiSelectList(db.Participants, "Id", "Name", canvasViewModel.SelectedParticipants);
            return View(canvasViewModel);
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Canvas canvas = db.Canvas.Include(c => c.CParticipants).FirstOrDefault(c => c.Id == id);

            if (canvas == null)
            {
                return HttpNotFound();
            }

            ViewBag.TeamLeaderId = new SelectList(db.Participants, "Id", "Name", canvas.TeamLeaderId);
            ViewBag.SelectedParticipants = new MultiSelectList(db.Participants, "Id", "Name", canvas.CParticipants.Select(cp => cp.ParticipantId));

            return View(canvas);
        }

        // POST: Canvas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,TeamLeaderId")] Canvas canvasViewModel, int[] SelectedParticipants)
        {
            if (ModelState.IsValid)
            {
                var canvas = db.Canvas.Include(c => c.CParticipants).FirstOrDefault(c => c.Id == canvasViewModel.Id);

                if (canvas == null)
                {
                    return HttpNotFound();
                }

                canvas.Name = canvasViewModel.Name;
                canvas.Description = canvasViewModel.Description;
                canvas.TeamLeaderId = canvasViewModel.TeamLeaderId;

                if (SelectedParticipants != null)
                {
                    var participantsToRemove = canvas.CParticipants
                        .Where(cp => !SelectedParticipants.Contains(cp.ParticipantId))
                        .ToList();

                    foreach (var participant in participantsToRemove)
                    {
                        canvas.CParticipants.Remove(participant);
                        db.CParticipants.Remove(participant);
                    }

                    foreach (var participantId in SelectedParticipants)
                    {
                        if (!canvas.CParticipants.Any(cp => cp.ParticipantId == participantId))
                        {
                            var participant = db.Participants.Find(participantId);
                            if (participant != null)
                            {
                                canvas.CParticipants.Add(new CanvasParticipant { CanvasId = canvas.Id, ParticipantId = participantId });
                            }
                        }
                    }
                }

                db.Entry(canvas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamLeaderId = new SelectList(db.Participants, "Id", "Name", canvasViewModel.TeamLeaderId);
            ViewBag.SelectedParticipants = new MultiSelectList(db.Participants, "Id", "Name", SelectedParticipants);

            return View(canvasViewModel);
        }

        // GET: Canvas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var canvas = db.Canvas
                .Include(c => c.CParticipants)
                .SingleOrDefault(c => c.Id == id);

            if (canvas == null)
            {
                return HttpNotFound();
            }

            return View(canvas);
        }

        // POST: Canvas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var canvas = db.Canvas
                .Include(c => c.CParticipants)
                .SingleOrDefault(c => c.Id == id);

            if (canvas != null)
            {
                db.CParticipants.RemoveRange(canvas.CParticipants);
                db.Canvas.Remove(canvas);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        public ActionResult removeAssociateFromCanvas(int id, int canvasID)
        {
            var canvasParticipant = db.CParticipants.Find(id);
            db.CParticipants.Remove(canvasParticipant);
            db.SaveChanges();
            return RedirectToAction("Edit/" + canvasID, "Canvas");
        }

        public ActionResult CanvasOverview()
        {
            var userEmail = User.Identity.GetUserName();
            var userAsParticipant = db.Participants.Where(p => p.Email == userEmail).FirstOrDefault();

            if (userAsParticipant != null)
            {
                int userParticipantId = userAsParticipant.Id;

                var canvasesAssociate = db.Canvas
                    .Where(c => c.CParticipants.Any(cp => cp.ParticipantId == userParticipantId))
                .ToList();

                var canvasesTeamLeader = db.Canvas.Where(c => c.TeamLeader.Email == userEmail).ToList();


                ViewBag.loggedInUserCanvasesAssociate = canvasesAssociate;
                ViewBag.loggedInUserCanvasesTL = canvasesTeamLeader;


            }
            else
            {
                ViewBag.loggedInUserCanvases = new List<Canvas>();
            }

            ViewBag.canvasId = RouteData.Values["id"];
            var canvasId_ = Convert.ToInt32(ViewBag.canvasID);
            var canvas = db.Canvas.Find(canvasId_);
            ViewBag.canvas = canvas;

            return View(canvas);
        }



    }
}
