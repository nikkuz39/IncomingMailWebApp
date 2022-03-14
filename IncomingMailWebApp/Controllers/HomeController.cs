using IncomingMailWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IncomingMailWebApp.Services;

namespace IncomingMailWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _db;
        private readonly LoadDb _loadDb;
        private readonly AddLetterAndAttributes _addLetterAndAttributes;
        private readonly EditAndDelete _editAndDelete;

        public HomeController(ILogger<HomeController> logger, ApplicationContext db, LoadDb loadDb, 
                                AddLetterAndAttributes addLetterAndAttributes, EditAndDelete editAndDelete)
        {
            _logger = logger;
            _db = db;
            _loadDb = loadDb;
            _addLetterAndAttributes = addLetterAndAttributes;
            _editAndDelete = editAndDelete;
        }

        public async Task<IActionResult> Index()
        {
            var viewModelLetter = new ViewModelLetter();

            var addressees = await _loadDb.LoadAddressee();
            var senders = await _loadDb.LoadSender();
            var tags = await _loadDb.LoadTag();

            viewModelLetter.Addressees = addressees;
            viewModelLetter.Senders = senders;
            viewModelLetter.Tags = tags;

            return View(viewModelLetter);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLetter(ViewModelLetter viewModelLetter)
        {
            int idLetter = await _addLetterAndAttributes.AddLetterInDB(viewModelLetter);

            foreach (int idTag in viewModelLetter.TagsId)
                await _addLetterAndAttributes.AddMailTagDB(idLetter, idTag);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
