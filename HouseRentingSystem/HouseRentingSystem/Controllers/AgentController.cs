namespace HouseRentingSystem.Controllers
{
    using HouseRentingSystem.Core.Services.Agents;
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

        public async Task<IActionResult> Become()
        {
            if (!await agentService.ExistsById(User.Id()))
            {
                return BadRequest();
            }

            return View(new BecomeAgentFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            var userId = User.Id();

            if (await agentService.ExistsById(userId))
            {
                return BadRequest();
            }

            if (await agentService.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber),
                    "Phone number already exists. Enter anothet one.");
            }

            if (await agentService.UserHasRents(userId))
            {
                ModelState.AddModelError("Error",
                    "You should have no rents to become an agent!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await agentService.Create(userId, model.PhoneNumber);

            return RedirectToAction(nameof(HousesController.All), "Houses");
        }
    }
}