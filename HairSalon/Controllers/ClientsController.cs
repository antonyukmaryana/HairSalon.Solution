using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
namespace HairSalon.Controllers
{
  public class Clients : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index(int stylistId)
    {
      List<Client> allClients = Client.GetAll(stylistId);
      return View(allClients);
    }

    [HttpGet("/clients/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/clients/new")]
    public ActionResult Create(string info, int stylistId)
    {
      Client client = new Client(info, stylistId, 0);
      client.Save();
      List<Client> allClients = Client.GetAll(stylistId);
      return View("Index", allClients);
    }
  }
}
