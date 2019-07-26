using BuyOnline_Assignment.Models.Data;
using BuyOnline_Assignment.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyOnline_Assignment.Area.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Pages
        public ActionResult Index()
        {
            List<PageVM> pagesList;
            using (DefaultConnection db = new DefaultConnection())
            {
                pagesList = db.Pages.ToArray().OrderBy(x => x.Sorting).Where(x => x.Slug != "home").Select(x => new PageVM(x)).ToList();
            }
            return View(pagesList);
        }
            
            // GET: Admin/Pages/AddPage
            [HttpGet]
            public ActionResult AddPage()                                                                               
            {
                return View();
            }
            [HttpPost]
            public ActionResult AddPage(PageVM model)
            {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (DefaultConnection db = new DefaultConnection())
            {
                // Declare slug
                string slug;

                // Init pageDTO
                PageDetail dto = new PageDetail();

                // DTO title
                dto.Title = model.Title;

                // Check for and set slug if need be
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }

                // Make sure title and slug are unique
                if (db.Pages.Any(x => x.Title == model.Title) || db.Pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That title or slug already exists.");
                    return View(model);
                }

                // DTO the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
                dto.Sorting = 100;

                // Save DTO
                db.Pages.Add(dto);
                db.SaveChanges();
            }

            // Set TempData message
            TempData["SM"] = "You have added a new page!";

            // Redirect
            return RedirectToAction("AddPage");
        }
       
        
        // GET: Admin/Pages/EditPage/id
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            // Declare pageVM
            PageVM model;

            using (DefaultConnection db = new DefaultConnection())
            {
                // Get the page
                PageDetail dto = db.Pages.Find(id);

                // Confirm page exists
                if (dto == null)
                {
                    return Content("The page does not exist.");
                }

                // Init pageVM
                model = new PageVM(dto);
            }
            // Return view with model
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (DefaultConnection db = new DefaultConnection())
            {
                // Get page id
                int id = model.Id;

                // Init slug
                string slug = "home";

                // Get the page
                PageDetail dto = db.Pages.Find(id);

                // DTO the title
                dto.Title = model.Title;

                // Check for slug and set it if need be
                if (model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                }

                // Make sure title and slug are unique
                if (db.Pages.Where(x => x.Id != id).Any(x => x.Title == model.Title) ||
                     db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That title or slug already exists.");
                    return View(model);
                }

                // DTO the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;

                // Save the DTO
                db.SaveChanges();
            }

            // Set TempData message
            TempData["SM"] = "You have edited the page!";

            // Redirect
            return RedirectToAction("EditPage");
        }

        // GET: Admin/Pages/PageDetails/id
        public ActionResult PageDetails(int id)
        {
            // Declare PageVM
            PageVM model;

            using (DefaultConnection db = new DefaultConnection())
            {
                // Get the page
                PageDetail dto = db.Pages.Find(id);

                // Confirm page exists
                if (dto == null)
                {
                    return Content("The page does not exist.");
                }

                // Init PageVM
                model = new PageVM(dto);
            }

            // Return view with model
            return View(model);
        }

        // GET: Admin/Pages/DeletePage/id
        public ActionResult DeletePage(int id)
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                // Get the page
                PageDetail dto = db.Pages.Find(id);

                // Remove the page
                db.Pages.Remove(dto);

                // Save
                db.SaveChanges();
            }

            // Redirect
            return RedirectToAction("Index");
        }

        // POST: Admin/Pages/ReorderPages
        [HttpPost]
        public void ReorderPages(int[] id)
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                // Set initial count
                int count = 1;

                // Declare PageDTO
                PageDetail dto;

                // Set sorting for each page
                foreach (var pageId in id)
                {
                    dto = db.Pages.Find(pageId);
                    dto.Sorting = count;

                    db.SaveChanges();

                    count++;
                }
            }
        }

        [HttpGet]
        public ActionResult EditSidebar()
        {
            // Declare model
            SidebarVM model;

            using (DefaultConnection db = new DefaultConnection())
            {
                // Get the DTO
                SidebarDTO dto = db.Sidebar.Find(1);

                // Init model
                model = new SidebarVM(dto);
            }

            // Return view with model
            return View(model);
        }
        [HttpPost]
        public ActionResult EditSidebar(SidebarVM model)
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                // Get the DTO
                SidebarDTO dto = db.Sidebar.Find(1);

                // DTO the body
                dto.Body = model.Body;

                // Save
                db.SaveChanges();
            }

            // Set TempData message
            TempData["SM"] = "You have edited the sidebar!";

            // Redirect
            return RedirectToAction("EditSidebar");
        }
    }
}