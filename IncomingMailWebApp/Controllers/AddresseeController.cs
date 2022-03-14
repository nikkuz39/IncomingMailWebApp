using IncomingMailWebApp.Models;
using IncomingMailWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncomingMailWebApp.Controllers
{
    public class AddresseeController : Controller
    {
        private readonly ILogger<AddresseeController> _logger;
        private readonly LoadDb _loadDb;
        private readonly AddLetterAndAttributes _addLetterAndAttributes;
        private readonly EditAndDelete _editAndDelete;

        public AddresseeController(ILogger<AddresseeController> logger, LoadDb loadDb, AddLetterAndAttributes addLetterAndAttributes, EditAndDelete editAndDelete)
        {
            _logger = logger;
            _loadDb = loadDb;
            _addLetterAndAttributes = addLetterAndAttributes;
            _editAndDelete = editAndDelete;
        }

        public async Task<IActionResult> Index()
        {
            var viewModelLetter = new ViewModelLetter();

            var addressees = await _loadDb.LoadAddressee();

            viewModelLetter.Addressees = addressees;

            return View(viewModelLetter);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddressee(Addressee addressee)
        {
            await _addLetterAndAttributes.AddAddresseeInDB(addressee);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAddressee(int id)
        {
            if (id == 0)
                return NotFound();

            await _editAndDelete.DeleteAddressee(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAddressee(int id)
        {
            if (id == 0)
                return NotFound();

            var addressee = await _loadDb.SearchAddressee(id);

            return View(addressee);
        }

        [HttpPost]
        public async Task<IActionResult> EditAddressee(Addressee addressee)
        {
            await _editAndDelete.EditAddressee(addressee);

            return RedirectToAction("Index");
        }
    }
}
