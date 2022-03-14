using Microsoft.AspNetCore.Mvc;
using IncomingMailWebApp.Models;
using IncomingMailWebApp.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;

namespace IncomingMailWebApp.Controllers
{
    public class TagController : Controller
    {
        private readonly ILogger<TagController> _logger;
        private readonly LoadDb _loadDb;
        private readonly AddLetterAndAttributes _addLetterAndAttributes;
        private readonly EditAndDelete _editAndDelete;

        public TagController(ILogger<TagController> logger, LoadDb loadDb, AddLetterAndAttributes addLetterAndAttributes, EditAndDelete editAndDelete)
        {
            _logger = logger;
            _loadDb = loadDb;
            _addLetterAndAttributes = addLetterAndAttributes;
            _editAndDelete = editAndDelete;
        }

        public async Task<IActionResult> Index()
        {
            var viewModelLetter = new ViewModelLetter();

            var tags = await _loadDb.LoadTag();

            viewModelLetter.Tags = tags;

            return View(viewModelLetter);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(Tag tag)
        {
            await _addLetterAndAttributes.AddTagDB(tag);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTag(int id)
        {
            if (id == 0)
                return NotFound();

            await _editAndDelete.DeleteTag(id);                   

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditTag(int id)
        {
            if (id == 0)
                return NotFound();

            var tag = await _loadDb.SearchTag(id);

            return View(tag);
        }
        
        [HttpPost]
        public async Task<IActionResult> EditTag(Tag tag)
        {
            await _editAndDelete.EditTag(tag);

            return RedirectToAction("Index");
        }
    }
}
