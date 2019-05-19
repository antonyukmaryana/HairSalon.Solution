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
    public ActionResult Create(string name, int stylistId)
    {
      Client client = new Client();
      client.Name = name;
      client.StylistId = stylistId;
      client.Save();
      List<Client> allClients = Client.GetAll(stylistId);
      return View("Index", allClients);
    }
    
    [HttpGet("/clients/delete")]
    public ActionResult Delete(int id, int stylistId)
    {
      Client.Delete(id);
      
      List<Client> allClients = Client.GetAll(stylistId);
      return View("Index", allClients);
    }
  }
}
