using Cascading_Dropdown_MVC.Data;
using Cascading_Dropdown_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cascading_Dropdown_MVC.Controllers
{
    public class HomeController : Controller
    {
        private List<Models.Country> CountryList;
        private List<Models.State> StateList;
        private List<Models.City> CityList;
        public HomeController()
        {
            using (NorthwindEntities CE = new NorthwindEntities())
            {
                CountryList = CE.Countries.Select(y => new Models.Country() {
                    CountryId=y.CountryId,
                    CountryName=y.CountryName
                }).ToList();

                StateList = CE.States.Select(y => new Models.State()
                {
                    StateId =y.StateId,
                    StateName=y.StateName,
                    CountryId =y.CountryId.Value
                }).ToList();

                CityList = CE.Cities.Select(y => new Models.City()
                {
                    CityId = y.CityId,
                    CityName = y.CityName,
                    StateId = y.StateId.Value
                }).ToList();
            }
        }
        public ActionResult Index()
        {
            ViewBag.Country = new SelectList(CountryList, "CountryId", "CountryName");
            return View();
        }

        public JsonResult GetState(int? CountryId)
        {
            return Json(StateList.Where(s => s.CountryId == CountryId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCity(int? StateId)
        {
            return Json(CityList.Where(c => c.StateId == StateId), JsonRequestBehavior.AllowGet);
        }
    }
}