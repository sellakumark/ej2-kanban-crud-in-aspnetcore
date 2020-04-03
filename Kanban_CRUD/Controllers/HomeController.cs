using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kanban_CRUD.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Kanban_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private KanbanDataContext _context;

        public HomeController(KanbanDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public List<KanbanCard> LoadCard()
        {
            return _context.KanbanCards.ToList();
        }

        [HttpPost]
        public void UpdateCard([FromBody]EditParams param)
        {
            // this block of code will execute while inserting the new cards
            if (param.action == "insert" || (param.action == "batch" && param.added.Count > 0))
            {
                var value = (param.action == "insert") ? param.value : param.added[0];
                int intMax = _context.KanbanCards.ToList().Count > 0 ? _context.KanbanCards.ToList().Max(p => p.Id) : 1;
                KanbanCard card = new KanbanCard()
                {
                    Assignee = value.Assignee,
                    RankId = value.RankId,
                    Status = value.Status,
                    Summary = value.Summary
                };
                _context.KanbanCards.Add(card);
                _context.SaveChanges();
            }
            // this block of code will execute while updating the existing cards
            if (param.action == "update" || (param.action == "batch" && param.changed.Count > 0))
            {
                KanbanCard value = (param.action == "update") ? param.value : param.changed[0];
                IQueryable<KanbanCard> filterData = _context.KanbanCards.Where(c => c.Id == Convert.ToInt32(value.Id));
                if (filterData.Count() > 0)
                {
                    KanbanCard card = _context.KanbanCards.Single(A => A.Id == Convert.ToInt32(value.Id));
                    card.Summary = value.Summary;
                    card.Status = value.Status;
                    card.RankId = value.RankId;
                    card.Assignee = value.Assignee;
                }
                _context.SaveChanges();
            }
            // this block of code will execute while deleting the existing cards
            if (param.action == "remove" || (param.action == "batch" && param.deleted.Count > 0))
            {
                if (param.action == "remove")
                {
                    int key = Convert.ToInt32(param.key);
                    KanbanCard card = _context.KanbanCards.Where(c => c.Id == key).FirstOrDefault();
                    if (card != null)
                    {
                        _context.KanbanCards.Remove(card);
                    }
                }
                else
                {
                    foreach (KanbanCard cards in param.deleted)
                    {
                        KanbanCard card = _context.KanbanCards.Where(c => c.Id == cards.Id).FirstOrDefault();
                        if (cards != null)
                        {
                            _context.KanbanCards.Remove(card);
                        }
                    }
                }
                _context.SaveChanges();
            }
        }
    }

    public class EditParams
    {
        public string key { get; set; }
        public string action { get; set; }
        public List<KanbanCard> added { get; set; }
        public List<KanbanCard> changed { get; set; }
        public List<KanbanCard> deleted { get; set; }
        public KanbanCard value { get; set; }
    }
}
