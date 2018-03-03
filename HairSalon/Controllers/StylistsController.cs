using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View("Index", allStylists);
        }

        [HttpGet("stylists/new")]
        public ActionResult CreateStylistForm()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult CreateStylist()
        {
            Stylist newStylist = new Stylist(Request.Form["stylistName"]);
            newStylist.Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult Detail(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist selectedStylist = Stylist.Find(id);
            List<Client> allClients = selectedStylist.GetClients();
            model.Add("stylist", selectedStylist);
            model.Add("clients", allClients);
            return View(model);
        }

        [HttpPost("/stylists/{id}/clients/new")]
        public ActionResult CreateClient(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist foundStylist = Stylist.Find(id);
            List<Client> stylistClients = foundStylist.GetClients();
            Client newClient = new Client(Request.Form["clientName"]);
            newClient.SetStylistId(foundStylist.GetId());
            stylistClients.Add(newClient);
            newClient.Save();
            model.Add("clients", stylistClients);
            model.Add("stylist", foundStylist);
            return View("Detail", model);
        }
    }
}
