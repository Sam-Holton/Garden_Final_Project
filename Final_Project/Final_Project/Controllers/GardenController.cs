using Final_Project.Models.DALModels;
using Final_Project.Models.ViewModels.GardenControllerViewModels;
using Final_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    public class GardenController : Controller
    {
        private readonly GardenDBContext _gardenDBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GeoIPClient _geoIPClient;
        private readonly TrefleClient _trefleClient;
        private readonly WeatherClient _weatherClient;

        public GardenController(GardenDBContext gardenDBContext,
            UserManager<IdentityUser> userManager,
             GeoIPClient geoIPClient,
            TrefleClient trefleClient,
            WeatherClient weatherClient
            )
        {
            _gardenDBContext = gardenDBContext;
            _userManager = userManager;
            _geoIPClient = geoIPClient;
            _trefleClient = trefleClient;
            _weatherClient = weatherClient;
        }

        //[Authorize] //uncomment when ready to test everything 
        public async Task<IActionResult> Index()
        {
            var gardener = await _userManager.GetUserAsync(User);



            return View();
        }

        public IActionResult MemberProfileForm()
        {//NO CONTENT NEEDED HERE
            return View();
        }

        public async Task<IActionResult> StoreGardenerData(MemberProfileFormViewModel model)
        {

            //use this to get userID
            var gardener = await _userManager.GetUserAsync(User);
            var profileDAL = new GardenerProfileDAL(); //for a new member that does not exist
            var response = await _geoIPClient.GetLocation();
            var memberAlreadyExist = new GardenerProfileDAL();
            memberAlreadyExist = _gardenDBContext.Gardener.Where(user => user.Id == gardener.Id).FirstOrDefault();
            var viewModel = new MemberProfileFormViewModel();

            //assume member doesn't already exist...can't update user profile 

            if (memberAlreadyExist == null
                && Validation.CheckName(model.FirstName)
                && Validation.CheckName(model.LastName)
                && Validation.CheckFavorite(model.FavoritePlant))
            {

                profileDAL.Id = gardener.Id;
                profileDAL.firstName = model.FirstName;
                profileDAL.lastName = model.LastName;
                profileDAL.usage = model.Usage;
                profileDAL.favoriteplant = model.FavoritePlant;

                //This needs to be done earlier in the process but adding for now
                profileDAL.homeLat = response.latitude.ToString();
                profileDAL.homeLon = response.longitude.ToString();

                //add to database
                _gardenDBContext.Gardener.Add(profileDAL);
                _gardenDBContext.SaveChanges();


                return View("MemberProfileForm");
            }
            else
            {
                //OMIT THE ID ASSIGNMENT IF JUST UPDATING DATABASE
                memberAlreadyExist.firstName = model.FirstName;
                memberAlreadyExist.lastName = model.LastName;

                memberAlreadyExist.usage = model.Usage;
                memberAlreadyExist.favoriteplant = model.FavoritePlant;

                //save database
                _gardenDBContext.SaveChanges();

                return View("MemberProfileForm");
            }

        }

        public async Task<IActionResult> MemberProfile()
        {
            var gardener = await _userManager.GetUserAsync(User);
            var viewModel = new MemberProfileViewModel();

            //Grab user ID to pull database info for user in the Member table

            var gardenerProfile = _gardenDBContext.Gardener.Where(member => member.Id == gardener.Id).FirstOrDefault();

            viewModel.FirstName = gardenerProfile.firstName;
            viewModel.FavoritePlant = $"{gardenerProfile.favoriteplant}s";
            viewModel.Usage = gardenerProfile.usage;


            //need to get image of favorite plant?
            //just grab the first image for now!
            var response = await _trefleClient.GetPlants(viewModel.FavoritePlant);

            if (response != null)
            {
                viewModel.ImgFavPlant = response.data[0].image_url;
                viewModel.TreflePlantName = response.data[0].common_name;
            }
            else
            {
                viewModel.ImgFavPlant = "https://dummyimage.com/100/fff/0011ff.png&text=Image+Not+Found";
                viewModel.TreflePlantName = "Looks like your favorite plant cannot be found";
            }

            return View(viewModel);
        }


        //THIS PAGE SHOULD ONLY BE ACCESSIBLE AFTER COMPLETING USER PROFILE INFO ABOVE!!!
        public IActionResult Weather()
        { //NO CONTENT NEEDED HERE
            return View();
        }

        public async Task<IActionResult> HomeWeather()
        {
            //use geo api to gather weather
            var response = await _geoIPClient.GetLocation();
            var weather = await _weatherClient.GetWeather(response.latitude, response.longitude);

            var viewModel = new HomeWeatherViewModel();
            //mapt responses to view Model
            viewModel.Temperature = weather.current.temp;
            var icon = weather.current.weather[0].icon;
            viewModel.ImgUrl = $"http://openweathermap.org/img/wn/{icon}@2x.png";

            return View(viewModel);
        }

        public async Task<IActionResult> VacationWeather()
        {
            //use lat/lon in member table to access weather
            var gardener = await _userManager.GetUserAsync(User);
            var viewModel = new VacationWeatherViewModel();

            //Grab user ID to pull database info for user in the Member table
            var gardenerProfile = _gardenDBContext.Gardener.Where(member => member.Id == gardener.Id).FirstOrDefault();

            //convert string to double
            double.TryParse(gardenerProfile.homeLat, out double latitude);
            double.TryParse(gardenerProfile.homeLon, out double longitude);

            //get weather
            var weather = await _weatherClient.GetWeather(latitude, longitude);

            viewModel.Temperature = weather.current.temp;
            var icon = weather.current.weather[0].icon;
            viewModel.ImgUrl = $"http://openweathermap.org/img/wn/{icon}@2x.png";

            return View(viewModel);
        }
        public async Task<IActionResult> MemberGarden()
        {
            var gardener = await _userManager.GetUserAsync(User);
            var viewModel = new MemberGardenViewModel();

            var gardenerView = _gardenDBContext.Garden.Where(member => member.Id == gardener.Id).ToList();


            viewModel.garden = gardenerView
                .Select(plant => new MemberGardenViewModel()
                {
                    common_name = plant.common_name,
                    scientific_name = plant.common_name,
                    plantDate = plant.plantDate,
                    harvestDate = plant.harvestDate,
                    image_url = plant.image_url,
                    Id = plant.GardenID

                })
                .ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> SearchResult(string search)
        {
            var searchresult = await _trefleClient.GetPlants(search);
            var viewModel = new SearchResultViewModel();

            viewModel.plants = searchresult.data
                .Select(plant => new SearchResultViewModel()
                {
                    common_name = plant.common_name,
                    scientific_name = plant.scientific_name,
                    image_url = plant.image_url
                })
                .ToList();

            return View(viewModel);
        }
        public IActionResult AddPlant(PlantInfoUpdateViewModel model)
        {
            var plantDB = new GardenDAL();

            plantDB.common_name = model.common_name;
            plantDB.scientific_name = model.scientific_name;
            plantDB.image_url = model.image_url;
       
            _gardenDBContext.Garden.Add(plantDB);
            _gardenDBContext.SaveChanges();

            return View("MemberGarden");
        }

        public IActionResult DeletePlant(int Id)
        {
            var plantDB = new GardenDAL();

            plantDB = _gardenDBContext.Garden
                .Where(garden => garden.GardenID == Id)
                .FirstOrDefault();

            _gardenDBContext.Garden.Remove(plantDB);
            _gardenDBContext.SaveChanges();

            return View("MemberGarden");
        }

        public IActionResult PlantInfoUpdate(SearchResultViewModel model)
        {
            var viewModel = new PlantInfoUpdateViewModel();

            viewModel.common_name = model.common_name;
            viewModel.scientific_name = model.scientific_name;
            viewModel.image_url = model.image_url;

            return View(viewModel);
        }













    }
}
