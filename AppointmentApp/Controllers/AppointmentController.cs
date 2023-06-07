using AppointmentApp.Models;
using AppointmentApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppointmentApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IRepository repo = new RepositoryImplementation();

        // GET: Appointment
        public ActionResult Index()
        {
            ViewBag.Data = repo.GetAllAsync();
            return View();
        }

        public ActionResult Create()
        {
            Appointment model = new Appointment();
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            if(ModelState.IsValid)
            {
                repo.AddAsync(appointment);
                ViewBag.Status = "Data Successfully Added";
            }
            else
            {
                ViewBag.Status = "Something went Wrong";
            }
            return RedirectToAction("Index");
         }
            

        
        public ActionResult Edit(int id)
        {
            if(id!=null && id!=0)
            {
                return View(repo.GetAsync(id));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            if(ModelState.IsValid)
            {
                repo.UpdateAsync(appointment);
                ViewBag.Status = "Data Successfully Updated";

            }
            else {
                ViewBag.Status = "Something Went Wrong";

            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            if(id!=null && id!=0)
            {
                if(repo.DeleteAsync(id))
                {
                    ViewBag.Status = "Succesfully Deleted";
                }
                else
                {
                    ViewBag.Status = "Something Went Wrong";
                }
            }    
            return RedirectToAction("Index");
        }
    }
}