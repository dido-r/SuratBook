namespace SuratBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Web.Http.Cors;

    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public string Index()
        {
            return "This is the server";
        }
    }
}
