using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;
using System.Collections.Generic;
using System;

namespace BandTracker.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
    List<Venue> allVenues = Venue.GetAll();
    return View(allVenues);
    }

    [HttpGet("/venue/add-venue")]
    public ActionResult NewVenueForm()
    {
      return View();
    }

    [HttpPost("/venue-added")]
    public ActionResult AddVenue()
    {
      string name = Request.Form["venue-name"];

      Venue newVenue = new Venue(name);
      newVenue.Save();

      List<Venue> allVenues = Venue.GetAll();
      return View("Index", allVenues);
    }

  }

}
