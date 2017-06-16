using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using Band_Tracker;
using Band_Tracker.Objects;

namespace Band_Tracker.Module
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["home.cshtml"];
      };
      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues_home.cshtml", allVenues];
      };
      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands_home.cshtml", allBands];
      };
      Get["/venues/add"] = _ => {
        return View["venue_add_form.cshtml"];
      };
      Get["/bands/add"] = _ => {
        return View["band_add_form.cshtml"];
      };

      Post["/venues/add"] = _ => {
        string testIfNameEmpty = Request.Form["venue-name"];
        string testIfCityEmpty = Request.Form["venue-city"];
        if ((testIfNameEmpty != "") && (testIfCityEmpty != ""))
        {
          Venue newVenue = new Venue(Request.Form["venue-name"], Request.Form["venue-city"]);
          newVenue.Save();
          return View["success.cshtml"];
        }
        else {
          return View["dun_goofed.cshtml"];
        }
      };
      Post["/bands/add"] = _ => {
        string testIfNameEmpty = Request.Form["band-name"];
        if (testIfNameEmpty != "")
        {
          Band newBand = new Band(Request.Form["band-name"]);
          newBand.Save();
          return View["success.cshtml"];
        }
        else {
          return View["dun_goofed.cshtml"];
        }
      };
    }
  }
}
