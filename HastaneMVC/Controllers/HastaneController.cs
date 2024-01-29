using HastaneMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace HastaneMVC.Controllers
{
    public class HastaneController : Controller
    {
        HttpClient client;
        private readonly string apiAdres = "https://localhost:7237/api/Hastane/action/GetAllHastane";

        public HastaneController()
        {
            client = new HttpClient();
        }
        public async Task<IActionResult> Index()
        {

            List<HospıtalVM> hospıtals = await client.GetFromJsonAsync<List<HospıtalVM>>(apiAdres + "GetAll");
            return View();
        }

        public async Task<ActionResult> GetById(int id=1)
        {
       
        HospıtalVM hospıtalVM= await  client.GetFromJsonAsync<HospıtalVM>(apiAdres + "Get/" + id);

            return View();
        }

        public async Task<IActionResult> Create(HospitalCreateVM hospitalCreateVM)
        {
           hospitalCreateVM.HastaneAd = "Sait Çiftçi Devlet Hatanesi";
            hospitalCreateVM.Adres = "Beşiktaş";

            var result = await client.PostAsJsonAsync(apiAdres + "Create", hospitalCreateVM);
            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
                
            }
            ModelState.AddModelError("Hata", "Eklenirken bir hata oluştu");
            return View();
        }

        public async Task<IActionResult> Update(int id = 1)
        {
            HospıtalVM hospitalVM = await client.GetFromJsonAsync<HospıtalVM>(apiAdres + "/" + id);
            hospitalVM = await client.GetFromJsonAsync<HospıtalVM>(apiAdres + "/" + id);
            hospitalVM.HastaneAd = "Kadıköy Hastanesi";
            hospitalVM.Adres = "Kadıköy";

            var result = await client.PutAsJsonAsync(apiAdres + "/" + id, hospitalVM);


            return View();
        }
        public async Task<IActionResult> Delete(int id = 1)
        {
            var result = await client.DeleteAsync(apiAdres + "/" + id);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("Hata", "Silme işleminiz sırasında hata oluştu");
            return View();



        }



    }
}
