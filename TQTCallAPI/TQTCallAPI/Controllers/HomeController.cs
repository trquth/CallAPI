using SSC.Core.Api.Models.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TQTCallAPI.Services.User;

namespace TQTCallAPI.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        public HomeController()
        {
            _userService = _userService.IsNull() ? new UserService() : _userService;
        }
        public ActionResult Index()
        {
            var model = _userService.GetListUser();

            return View(model);
        }
    }
}
