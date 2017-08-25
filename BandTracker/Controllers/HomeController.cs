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

      return View(Venue.GetAll());
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

    [HttpGet("/bands/all")]
    public ActionResult BandListAll()
    {
      List<Band> allBands = Band.GetAll();
      return View(allBands);
    }

    [HttpGet("/venue/{id}")]
    public ActionResult VenueDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Venue selectedVenue = Venue.Find(id);
      List<Band> venueBands = selectedVenue.GetBands();
      model.Add("selectedVenue", selectedVenue);
      model.Add("venueBands", venueBands);
      return View(model);
    }

    [HttpGet("/venue/{id}/update-form")]
    public ActionResult VenueUpdateForm(int id)
    {
      Venue thisVenue = Venue.Find(id);

      return View(thisVenue);
    }
    [HttpPost("/venue/{id}/updated")]
    public ActionResult VenueUpdated(int id)
    {

      Dictionary<string, object> model = new Dictionary<string, object>();
      Venue selectedVenue = Venue.Find(id);
      selectedVenue.Update(Request.Form["new-venue-name"]);
      List<Band> venueBands = selectedVenue.GetBands();

      model.Add("selectedVenue", selectedVenue);
      model.Add("venueBands", venueBands);


      return View("VenueDetails", model);
    }
  }
}
