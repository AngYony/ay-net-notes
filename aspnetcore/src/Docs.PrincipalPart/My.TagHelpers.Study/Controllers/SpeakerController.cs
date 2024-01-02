using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace My.TagHelpers.Study.Controllers
{
    public class Speaker
    {
        public int SpeakerId { get; set; }
    }

    public class SpeakerController : Controller
    {
        private List<Speaker> Speakers = new List<Speaker>
        {
            new Speaker{ SpeakerId=1 },
            new Speaker{ SpeakerId=2 },
            new Speaker{ SpeakerId=3 }
        };

        public IActionResult Index()
        {
            return View(Speakers);
        }

        [Route("/Speaker/Evaluations", Name = "speakerevals")]
        public IActionResult Evaluations()
        {
            return View();
        }

        [Route("/Speaker/EvaluationsCurrent", Name = "speakervalscurrent")]
        public IActionResult Evaluations(int speakerId, bool currentYear)
        {
            return View();
        }

        [Route("Speaker/{id:int}")]
        public IActionResult Detail(int id)
        {
            return View(Speakers.FirstOrDefault(a => a.SpeakerId == id));
        }

        [Route("Speaker")]
        public IActionResult Detail2(int speakerid)
        {
            return View(Speakers.FirstOrDefault(a => a.SpeakerId == speakerid));
        }

        public IActionResult Tag()
        {
            return View();
        }
    }
}