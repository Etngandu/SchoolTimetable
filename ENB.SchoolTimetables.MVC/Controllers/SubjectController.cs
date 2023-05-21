using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.SchoolTimetables.Entities;
using ENB.SchoolTimetables.Entities.Repositories;
using ENB.SchoolTimetables.Infrastructure;
using ENB.SchoolTimetables.MVC.Help;
using ENB.SchoolTimetables.MVC.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;



namespace ENB.SchoolTimetables.MVC.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SubjectController> _logger;
        private readonly IAsyncSubjectRepository  _asyncSubjectRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        private readonly IValidator<CreateAndEditSubject> _validator;
        public SubjectController( IMapper mapper, ILogger<SubjectController> logger,
                                   IAsyncSubjectRepository asyncSubjectRepository, 
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf,
                                   IValidator<CreateAndEditSubject> validator)
        {
            _mapper = mapper;
            _logger = logger;   
            _asyncSubjectRepository = asyncSubjectRepository;
            _asyncUnitOfWorkFactory= asyncUnitOfWorkFactory;
            _notyf = notyf;
            _validator = validator;
        }

        // GET: TeacherController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetSubjectData()
        {
            IQueryable<Subject> allSubject = _asyncSubjectRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplaySubject>>(allSubject).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // GET: TeacherController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Subject not found");

            Subject dbSubject = await _asyncSubjectRepository.FindById(id);

            ViewBag.Message = dbSubject.SubjectName;

            _logger.LogInformation($"Details of Subject: {ViewBag.Message}");

            if (dbSubject is null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplaySubject>(dbSubject);

            return View(data);
        }

        // GET: TeacherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Create(CreateAndEditSubject  createAndEditSubject )
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            ValidationResult result = await _validator.ValidateAsync(createAndEditSubject);


            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Create", createAndEditSubject);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Subject dbSubject = new();

                    _mapper.Map(createAndEditSubject, dbSubject);
                    await _asyncSubjectRepository.Add(dbSubject);

                    _notyf.Success("Subject Created  Successfully! ");

                    return RedirectToAction("Index");
                }
            }
            
        }

        // GET: TeacherController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            _logger.LogError($"Teacher {id} not found");
           
            Subject dbSubject = await _asyncSubjectRepository.FindById(id);

            if (dbSubject is null)
            {
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditSubject>(dbSubject));

            return View(data);
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditSubject  createAndEditSubject)
        {
            
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditSubject);

            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Edit", createAndEditSubject);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Subject dbSubjectToUpdate = await _asyncSubjectRepository.FindById(createAndEditSubject.Id);

                    _mapper.Map(createAndEditSubject, dbSubjectToUpdate, typeof(CreateAndEditSubject), typeof(Subject));

                    _notyf.Success("Subject Update  Successfully! ");

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: TeacherController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Subject dbSubject = await _asyncSubjectRepository.FindById(id);
            ViewBag.Message = dbSubject.SubjectName;

            if (dbSubject is null)
            {
                return NotFound();
            }
            var data = _mapper.Map<DisplaySubject>(dbSubject);
            return View(data);
        }

        // POST: TeacherController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Subject dbSubject = await _asyncSubjectRepository.FindById(id);
         await using (await _asyncUnitOfWorkFactory.Create())
            {
                 _asyncSubjectRepository.Remove(dbSubject);

                  _notyf.Error("Subject Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
