namespace HouseRentingSystem.Controllers
{
    using HouseRentingSystem.Models.Agents;
    using Microsoft.AspNetCore.Mvc;

    public class AgentsController : BaseController
    {
        public IActionResult Become()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Become(BecomeAgentFormModel agent)
        {
            return RedirectToAction(nameof(HousesController.All), nameof(HousesController));
        }
    }
}