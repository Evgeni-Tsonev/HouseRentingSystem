namespace HouseRentingSystem.Controllers
{
    using HouseRentingSystem.Core.Models.Agents;
    using HouseRentingSystem.Infrastructure;
    using HouseRentingSystem.Models.Agents;
    using Microsoft.AspNetCore.Mvc;

    public class AgentsController : BaseController
    {
        private readonly IAgentService agentService;

        public AgentsController(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        public IActionResult Become()
        {
            if (agentService.ExistsById(User.Id()))
            {
                return BadRequest();
            }

            return View(new BecomeAgentFormModel());
        }

        [HttpPost]
        public IActionResult Become(BecomeAgentFormModel model)
        {
            var userId = User.Id();

            if (agentService.ExistsById(userId))
            {
                return BadRequest();
            }

            if (agentService.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber),
                    "Phone number already exists. Enter anothet one.");
            }

            if (agentService.UserHasRents(userId))
            {
                ModelState.AddModelError("Error",
                    "You should have no rents to become an agent!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            agentService.Create(userId, model.PhoneNumber);

            return RedirectToAction(nameof(HousesController.All), "Houses");
        }
    }
}