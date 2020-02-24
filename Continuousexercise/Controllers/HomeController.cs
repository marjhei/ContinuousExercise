
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Continuousexercise.Models;


namespace Continuousexercise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()  
        {
            return View();
        }

        public IActionResult Home()
        {
            CEContext db = new CEContext();

            var haku = from t in db.Topic

                       select t;


            List<Topic> Kaikkiaiheet = new List<Topic>();

            foreach (Topic topic in haku)
            {


                Kaikkiaiheet.Add(new Topic() { Id = topic.Id, Title = topic.Title, Description = topic.Description, TimeToMaster = topic.TimeToMaster, Source = topic.Source, StartLearningDate = topic.StartLearningDate, CompletionDate = topic.CompletionDate, InProgress = topic.InProgress });

            }

            ViewBag.Tulos = Kaikkiaiheet;
            return View(Kaikkiaiheet);


        }
        public IActionResult Haku()   
        {
            return View();
        }

        [HttpPost]
        public IActionResult Haku(string haettuaihe)  
        {

            CEContext db = new CEContext();


            var haku = from t in db.Topic
                       where t.Title.Contains(haettuaihe)
                       select t;

            List<Topic> Hakuehdonaiheet= new List<Topic>();



            foreach (Topic topic in haku)
            {


                Hakuehdonaiheet.Add(new Topic() { Id = topic.Id, Title = topic.Title, Description = topic.Description, TimeToMaster = topic.TimeToMaster, Source = topic.Source, StartLearningDate = topic.StartLearningDate, CompletionDate= topic.CompletionDate, InProgress = topic.InProgress });

            }

            ViewBag.Tulos = Hakuehdonaiheet;
            return View("Hakuehdontayttavataiheet", Hakuehdonaiheet);
        }


        public IActionResult Aiheet()   
        {
            CEContext db = new CEContext();

            var haku = from t in db.Topic
                       
                       select t;


            List<Topic> Kaikkiaiheet = new List<Topic>();

            foreach (Topic topic in haku)
            {


                Kaikkiaiheet.Add(new Topic() { Id = topic.Id, Title = topic.Title, Description = topic.Description, TimeToMaster = topic.TimeToMaster, Source = topic.Source, StartLearningDate = topic.StartLearningDate, CompletionDate = topic.CompletionDate, InProgress = topic.InProgress });

            }

            ViewBag.Tulos = Kaikkiaiheet;
            return View(Kaikkiaiheet);


        }


        public IActionResult Hakuehdontayttavataiheet()  
        {

            return View();
        }

        public ActionResult LisaaAihe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Lisaa(Topic topic)
        {
            using (CEContext db = new CEContext())
            {
                db.Topic.Add(topic);
                db.SaveChanges();
            }
            return View("UusiAihe");
        }

        public ActionResult UusiAihe()
        {
            return View();
        }

        public ActionResult Delete(int? id)
        {
            using (CEContext db = new CEContext())
            {

               Topic topic = db.Topic.Find(id);
              
                return View(topic);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (CEContext db = new CEContext())
            {
               
                    Topic topic = db.Topic.Find(id);
                    db.Topic.Remove(topic);
                    db.SaveChanges();
                
               
                return RedirectToAction("Aiheet");
            }
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
