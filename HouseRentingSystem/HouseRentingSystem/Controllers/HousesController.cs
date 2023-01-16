namespace HouseRentingSystem.Controllers
{
    using HouseRentingSystem.Core.Services.Agents;
    using HouseRentingSystem.Core.Services.Houses;
    using HouseRentingSystem.Infrastructure;
    using HouseRentingSystem.Core.Models.Houses;
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
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var queryResult = await housesService.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            var houseCategories = await housesService.AllCategoriesNames();
            query.Categories = houseCategories;

            return View(query);
        }

        public async Task<IActionResult> Mine()
        {
            IEnumerable<HouseServiceModel> myHouses = null;

            var userId = User.Id();

            if (await agentService.ExistsById(userId))
            {
                var currentAgentId = await agentService.GetAgentId(userId);

                myHouses = await housesService.AllHousesByAgentId(currentAgentId);
            }
            else
            {
                myHouses = await housesService.AllHousesByUserId(userId);
            }

            return View(myHouses);
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
