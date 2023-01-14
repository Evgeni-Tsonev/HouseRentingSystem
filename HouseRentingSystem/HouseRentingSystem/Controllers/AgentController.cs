namespace HouseRentingSystem.Controllers
{
    using HouseRentingSystem.Models.Agents;
    using Microsoft.AspNetCore.Mvc;

    public class AgentController : BaseController
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