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
//VENUES PATHING
      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues_home.cshtml", allVenues];
      };
      Get["/venues/add"] = _ => {
        return View["venue_add_form.cshtml"];
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
      Get["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedVenue = Venue.Find(parameters.id);
        var venuesBands = selectedVenue.GetBands();
        var allBands = Band.GetAll();
        model.Add("venue", selectedVenue);
        model.Add("bands", venuesBands);
        model.Add("allBands", allBands);
        return View["venue.cshtml", model];
      };
      Patch["/venues/{id}/edit"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        string newName = Request.Form["venue-name"];
        string newCity = Request.Form["venue-city"];
        if (newName != "")
        {
          selectedVenue.UpdateName(newName);
        }
        if (newCity != "")
        {
          selectedVenue.UpdateCity(newCity);
        }
        if (newName == "" && newCity == "")
        {
          return View["dun_goofed.cshtml"];
        }
        return View["success.cshtml"];
      };
      Delete["/venues/{id}/delete"] = _ => {
        Venue selectedVenue = Venue.Find(Request.Form["venue-id"]);
        selectedVenue.Delete();
        return View["success.cshtml"];
      };
//BANDS PATHING
      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands_home.cshtml", allBands];
      };
      Get["/bands/add"] = _ => {
        return View["band_add_form.cshtml"];
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
      Get["/bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedBand = Band.Find(parameters.id);
        var bandsVenues = selectedBand.GetVenues();
        var bandsGenres = selectedBand.GetGenres();
        var allVenues = Venue.GetAll();
        model.Add("band", selectedBand);
        model.Add("venues", bandsVenues);
        model.Add("allVenues", allVenues);
        model.Add("genres", bandsGenres);
        return View["band.cshtml", model];
      };
      Post["/bands/{id}/edit_venue"] = _ => {
        Band selectedBand = Band.Find(Request.Form["band-id"]);
        Venue selectedVenue = Venue.Find(Request.Form["venue-id"]);
        selectedBand.AddVenueToShowsJoinTable(selectedVenue);
        return View["success.cshtml"];
      };
//GENRE PATHING
      Get["/genres"] = _ => {
        List<Genre> allGenres = Genre.GetAll();
        return View["genres_home.cshtml", allGenres];
      };
      Get["/genres/add"] = _ => {
        return View["genre_add_form.cshtml"];
      };
      Post["/genres/add"] = _ => {
        string testIfNameEmpty = Request.Form["genre-name"];
        if (testIfNameEmpty != "")
        {
          Genre newGenre = new Genre(Request.Form["genre-name"]);
          newGenre.Save();
          return View["success.cshtml"];
        }
        else {
          return View["dun_goofed.cshtml"];
        }
      };
      Get["/genres/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedGenre = Genre.Find(parameters.id);
        var genresBands = selectedGenre.GetBands();
        var allBands = Band.GetAll();
        model.Add("genre", selectedGenre);
        model.Add("bands", genresBands);
        model.Add("allBands", allBands);
        return View["genre.cshtml", model];
      };
      Patch["/genres/{id}/edit_name"] = parameters => {
        Genre selectedGenre = Genre.Find(parameters.id);
        string testIfNameEmpty = Request.Form["genre-name"];
        if (testIfNameEmpty != "")
        {
          selectedGenre.UpdateName(testIfNameEmpty);
          return View["success.cshtml"];
        }
        else {
          return View["dun_goofed.cshtml"];
        }
      };
//JOIN TABLE PATHING
      Post["/venues/{id}/edit_band"] = _ => {
        Venue selectedVenue = Venue.Find(Request.Form["venue-id"]);
        Band selectedBand = Band.Find(Request.Form["band-id"]);
        selectedVenue.AddBandToShowsJoinTable(selectedBand);
        return View["success.cshtml"];
      };
      Post["/genres/{id}/edit_band"] = _ => {
        Genre selectedGenre = Genre.Find(Request.Form["genre-id"]);
        Band selectedBand = Band.Find(Request.Form["band-id"]);
        selectedGenre.AddBandToBands_GenresJoinTable(selectedBand);
        return View["success.cshtml"];
      };

    }
  }
}
