using System.Web;
using System.Web.Mvc;

namespace demo.values.Controllers
{
  public class AccountController : Controller
  {
    [AllowAnonymous]
    public ActionResult AuthorizeSSO(string returnUrl)
    {
      // Uses the default authentication type "PivotalSSO"
      return new Models.ChallengeResult(
        Steeltoe.Security.Authentication.CloudFoundry.Owin.Constants.DefaultAuthenticationType,
        returnUrl ?? Url.Action("Secure", "Home"));
    }

    [AllowAnonymous]
    public ActionResult AccessDenied()
    {
      ViewBag.Message = "Insufficient permissions.";
      return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LogOff()
    {
      Request.GetOwinContext().Authentication.SignOut();
      return RedirectToAction("Index", "Home");
    }
  }
}