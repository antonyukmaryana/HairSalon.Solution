using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;


namespace HairSalon.Controllers
{
    public class SpecialitiesController : Controller
    {
        [HttpGet("/specialities")]
        public ActionResult Index()
        {
            List<Specialty> specialties = Specialty.GetAll();
            return View(specialties);
        }

        [HttpGet("/specialities/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/specialities/new")]
        public ActionResult Create(string name)
        {
            Specialty speciality = new Specialty();
            speciality.Name = name;
            speciality.Save();
            List<Specialty> specialties = Specialty.GetAll();
            return View("Index", specialties);
        }
    
    }
}