using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
        [HttpGet("/stylists/{stylistID}/clients/new")]
        public ActionResult CreateClientForm(int stylistId)
        {
          Stylist foundStylist = Stylist.Find(stylistId);
          return View(foundStylist);
        }

        [HttpGet("/clients/{id}")]
        public ActionResult Detail(int id)
        {
          Client foundClient = Client.Find(id);
          Stylist foundStylist = Stylist.Find(foundClient.GetStylistId());
          Dictionary<string, object> clientDetail = new Dictionary <string, object>();
          clientDetail.Add("client", foundClient);
          clientDetail.Add("stylist", foundStylist);
          return View(clientDetail);
        }
    }
}
