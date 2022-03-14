using IncomingMailWebApp.Models;
using IncomingMailWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncomingMailWebApp.Controllers
{
    public class LetterController : Controller
    {
        private readonly ILogger<LetterController> _logger;
        private readonly LoadDb _loadDb;
        private readonly AddLetterAndAttributes _addLetterAndAttributes;
        private readonly EditAndDelete _editAndDelete;

        public LetterController(ILogger<LetterController> logger, LoadDb loadDb, AddLetterAndAttributes addLetterAndAttributes, EditAndDelete editAndDelete)
        {
            _logger = logger;
            _loadDb = loadDb;
            _addLetterAndAttributes = addLetterAndAttributes;
            _editAndDelete = editAndDelete;
        }

        public async Task<IActionResult> Index()
        {
            var viewModelLetter = new ViewModelLetter();

            var mails = await _loadDb.LoadLetter();

            viewModelLetter.Mails = mails;

            return View(viewModelLetter);
        }

        public async Task<IActionResult> SearchLetter(int id)
        {
            if (id == 0)
                return NotFound();

            var viewModelLetter = new ViewModelLetter();

            var mails = new List<Mail>();
            mails.Add(await _loadDb.SearchLetter(id));

            viewModelLetter.Mails = mails;

            return View(viewModelLetter);
        }

        public async Task<IActionResult> DeleteLetter(int id)
        {
            if (id == 0)
                return NotFound();

            await _editAndDelete.DeleteLetter(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditLetter(int id)
        {
            if (id == 0)
                return NotFound();

            var  viewModel = new ViewModelLetter();

            viewModel.Addressees = await _loadDb.LoadAddressee();
            viewModel.Senders = await _loadDb.LoadSender();
            viewModel.Tags = await _loadDb.LoadTag();
            viewModel.Mail = await _loadDb.SearchLetter(id);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditLetter(Mail mail)
        {
            await _editAndDelete.EditLetter(mail);

            //foreach (int idTag in viewModelLetter.TagsId)
            //    await _editAndDelete.EditMailTag(viewModelLetter.Mail.Id, idTag);

            return RedirectToAction("Index");
        }
    }    
}
