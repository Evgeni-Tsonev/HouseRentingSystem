namespace HouseRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AgentController : BaseController
    {
        [HttpPost]
        public IActionResult Rent(int id)
        {
            return RedirectToAction(nameof(HousesController.Mine));
        }

        [HttpPost]
        public IActionResult Leave(int id)
        {
            return RedirectToAction(nameof(HousesController.Mine));
        }
    }
}