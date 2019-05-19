using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
namespace HairSalon.Controllers
{
  public class Stylists : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }
    
    [HttpGet("/stylists/update")]
    public ActionResult Update(int id)
    {
      Stylist stylist = new Stylist();
      stylist.Id = id;
      BaseEntity.Populate(stylist, "Stylist");
      return View(stylist);
    }
    
    [HttpPost("/stylists/update")]
    public ActionResult Update(int id, string name)
    {
      Stylist.Update("Stylist", id, name);
      return View("Index", Stylist.GetAll()); 
    }

    [HttpPost("/stylists/new")]
    public ActionResult Create(string name)
    {
      Stylist stylist = new Stylist();
      stylist.Name = name;
      stylist.Save();
      return View("Index", Stylist.GetAll());
    }
    [HttpGet("/stylists/delete")]
        public ActionResult Delete(int id, int stylistId)
        {
          Stylist.Delete(id);
          
          List<Stylist> allStylists = Stylist.GetAll();
          return View("Index", allStylists);
        }
  }
}
