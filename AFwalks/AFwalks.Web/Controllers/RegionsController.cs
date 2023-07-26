using AFwalks.Web.Models;
using AFwalks.Web.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace AFwalks.Web.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();

            try
            {
                // Get All Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7162/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                // var stringResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                //  var response = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

               // ViewBag.Response = stringResponseBody;
            }
            catch (Exception ex)
            {

               // throw;
            }

            return View(response);
        }




        //public async Task<IActionResult> Index()
        //{


        //    try
        //    {
        //        // Get All Regions from Web API
        //        var client = httpClientFactory.CreateClient();

        //        var httpResponseMessage = await client.GetAsync("https://localhost:7162/api/regions");

        //        httpResponseMessage.EnsureSuccessStatusCode();


        //        var response = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();

        //        return View(response);

        //    }
        //    catch (Exception ex)
        //    {

        //        // throw;
        //    }

        //    return View();
        //}

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
			var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7162/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
			};

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

			var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response != null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
		}

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // ViewBag.Id = id;
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7162/api/regions/{id.ToString()}");
            if (response != null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {

			var client = httpClientFactory.CreateClient();

			var httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri($"https://localhost:7162/api/regions/{request.Id}"),
				Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
			};

			var httpResponseMessage = await client.SendAsync(httpRequestMessage);
			httpResponseMessage.EnsureSuccessStatusCode();

			var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

			if (response != null)
			{
				return RedirectToAction("Edit", "Regions");
			}

			return View();


		}

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7162/api/regions/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {

                // Console
            }

            return View("Edit");
		}
    }
}
