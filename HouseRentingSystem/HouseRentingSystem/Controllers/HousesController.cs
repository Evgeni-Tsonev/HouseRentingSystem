namespace HouseRentingSystem.Controllers
{
    using HouseRentingSystem.Core.Services.Agents;
    using HouseRentingSystem.Core.Services.Houses;
    using HouseRentingSystem.Infrastructure;
    using HouseRentingSystem.Models.Houses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HousesController : BaseController
    {
        private readonly IHouseService housesService;
        private readonly IAgentService agentService;

        public HousesController(IHouseService housesService, IAgentService agentService)
        {
            this.housesService = housesService;
            this.agentService = agentService;
        }

        [AllowAnonymous]
        public IActionResult All()
        {
            return View(new AllHousesQueryModel());
        }

        public IActionResult Mine()
        {
            return View(new AllHousesQueryModel());
        }

        public IActionResult Details(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        public async Task<IActionResult> Add()
        {
            if (!await agentService.ExistsById(User.Id()))
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }

            return View(new HouseFormModel()
            {
                Categories = await housesService.AllCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (!await agentService.ExistsById(User.Id()))
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }

            if (!await housesService.CategoryExists(model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId),
                    "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await housesService.AllCategories();

                return View(model);
            }

            var agentId = await agentService
                .GetAgentId(User.Id());

            var houseId = await housesService.Create(
                model.Title,
                model.Address,
                model.Description,
                model.ImageUrl,
                model.PricePerMonth,
                model.CategoryId,
                agentId);

            return RedirectToAction(nameof(Details), new { id = houseId });
        }

        public IActionResult Edit(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        [HttpPost]
        public IActionResult Edit(int id, HouseFormModel house)
        {
            return RedirectToAction(nameof(Details), new { id = "1" });
        }

        public IActionResult Delete(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        [HttpPost]
        public IActionResult Delete(HouseFormModel house)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public IActionResult Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public IActionResult Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
