using IncomingMailWebApp.Models;
using IncomingMailWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncomingMailWebApp.Controllers
{
    public class SenderController : Controller
    {
        private readonly ILogger<SenderController> _logger;
        private readonly LoadDb _loadDb;
        private readonly AddLetterAndAttributes _addLetterAndAttributes;
        private readonly EditAndDelete _editAndDelete;

        public SenderController(ILogger<SenderController> logger, LoadDb loadDb, AddLetterAndAttributes addLetterAndAttributes, EditAndDelete editAndDelete)
        {
            _logger = logger;
            _loadDb = loadDb;
            _addLetterAndAttributes = addLetterAndAttributes;
            _editAndDelete = editAndDelete;
        }

        public async Task<IActionResult> Index()
        {
            var viewModelLetter = new ViewModelLetter();

            var senders = await _loadDb.LoadSender();

            viewModelLetter.Senders = senders;

            return View(viewModelLetter);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSender(Sender sender)
        {
            await _addLetterAndAttributes.AddSenderInDB(sender);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSender(int id)
        {
            if (id == 0)
                return NotFound();

            await _editAndDelete.DeleteSender(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditSender(int id)
        {
            if (id == 0)
                return NotFound();

            var sender = await _loadDb.SearchSender(id);

            return View(sender);
        }

        [HttpPost]
        public async Task<IActionResult> EditSender(Sender sender)
        {
            await _editAndDelete.EditSender(sender);

            return RedirectToAction("Index");
        }
    }
}
