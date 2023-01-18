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

        public async Task<IActionResult> Details(int id)
        {
            if (!await housesService.Exists(id))
            {
                return BadRequest();
            }

            var house = await housesService.HouseDetailsById(id);

            return View(house);
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

        public async Task<IActionResult> Edit(int id)
        {
            if (!await housesService.Exists(id))
            {
                return BadRequest();
            }

            if (!await housesService.HasAgentWithId(id, User.Id()))
            {
                return Unauthorized();
            }

            var house = await housesService.HouseDetailsById(id);

            var houseCategoryId = await housesService.GetHouseCategoryId(house.Id);

            var model = new HouseFormModel()
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = houseCategoryId,
                Categories = await housesService.AllCategories(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel house)
        {
            if (!await housesService.Exists(id))
            {
                return View();
            }

            if (!await housesService.HasAgentWithId(id, User.Id()))
            {
                return Unauthorized();
            }

            if (!await housesService.CategoryExists(house.CategoryId))
            {
                ModelState.AddModelError(nameof(house.CategoryId),
                    "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                house.Categories = await housesService.AllCategories();
                return View(house);
            }

            await housesService.Edit(
                id,
                house.Title,
                house.Address,
                house.Description,
                house.ImageUrl,
                house.PricePerMonth,
                house.CategoryId);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await housesService.Exists(id))
            {
                return BadRequest();
            }

            if (!await housesService.HasAgentWithId(id, User.Id()))
            {
                return Unauthorized();
            }

            var house = await housesService.HouseDetailsById(id);

            var model = new HouseDetailsViewModel()
            {
                Id = id,
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel house)
        {
            if (!await housesService.Exists(house.Id))
            {
                return BadRequest();
            }

            if (!await housesService.HasAgentWithId(house.Id, User.Id()))
            {
                return Unauthorized();
            }

            await housesService.Delete(house.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (!await housesService.Exists(id))
            {
                return BadRequest();
            }

            if (await agentService.ExistsById(User.Id()))
            {
                return Unauthorized();
            }

            if (await housesService.IsRented(id))
            {
                return BadRequest();
            }

            await housesService.Rent(id, User.Id());

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public IActionResult Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
