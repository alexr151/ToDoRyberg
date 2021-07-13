using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoRyberg.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoRyberg.Controllers
{
    public class HomeController : Controller
    {
        private ToDoContext context;
        public HomeController(ToDoContext ctx) => context = ctx;


        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;
            ViewBag.Priorities = context.Priorities.ToList();

            IQueryable<ToDo> query = context.ToDos
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Include(t => t.Priority);

            if (filters.HasCategory)
            {
                query = query.Where(c => c.CategoryId == filters.CategoryId);
            }

            if (filters.HasSatus)
            {
                query = query.Where(c => c.StatusId == filters.StatusId);
            }

            if (filters.HasPriority)
            {
                query = query.Where(c => c.PriorityId == filters.PriorityId);
            }

            if (filters.HasDue) { 
                var today = DateTime.Today;
                if (filters.IsPast)
                {
                    query = query.Where(t => t.DueDate < today);
                }

                if (filters.IsFuture)
                {
                    query = query.Where(t => t.DueDate > today);
                }

                if (filters.IsToday)
                {
                    query = query.Where(t => t.DueDate == today);
                }
            }

            var tasks = query.OrderBy(t => t.DueDate).ToList();
            return View(tasks);
        }
        
        [HttpGet]
        public IActionResult Add(string description, DateTime dueDate)
        {
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Priorities = context.Priorities.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            } 
            else
            {
                ViewBag.Categories = context.Categories.ToList();
                ViewBag.Statuses = context.Statuses.ToList();
                ViewBag.Priorities = context.Priorities.ToList();
                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] string id, ToDo selected)
        {
            if (selected.StatusId == null)
            {
                context.ToDos.Remove(selected);
            }
            else
            {
                string newStatusId = selected.StatusId;
                selected = context.ToDos.Find(selected.Id);
                selected.StatusId = newStatusId;
                context.ToDos.Update(selected);
            }

            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }
        
    }

}
