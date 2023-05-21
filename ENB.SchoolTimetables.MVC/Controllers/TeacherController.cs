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
    public class TeacherController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherController> _logger;
        private readonly IAsyncTeacherRepository _asyncTeacherRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        private readonly IValidator<CreateAndEditTeacher> _validator;
        public TeacherController( IMapper mapper, ILogger<TeacherController> logger,
                                   IAsyncTeacherRepository asyncTeacherRepository, 
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf,
                                   IValidator<CreateAndEditTeacher> validator)
        {
            _mapper = mapper;
            _logger = logger;   
            _asyncTeacherRepository = asyncTeacherRepository;
            _asyncUnitOfWorkFactory= asyncUnitOfWorkFactory;
            _notyf = notyf;
            _validator = validator;
        }

        // GET: TeacherController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTeacherData()
        {
            IQueryable<Teacher> allTeacher = _asyncTeacherRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayTeacher>>(allTeacher).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // GET: TeacherController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Teacher not found");

            Teacher dbTeacher = await _asyncTeacherRepository.FindById(id);

            ViewBag.Message = dbTeacher.FullName;

            _logger.LogInformation($"Details of Teacher: {ViewBag.Message}");

            if (dbTeacher is null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayTeacher>(dbTeacher);

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
        public async Task<IActionResult> Create(CreateAndEditTeacher createAndEditTeacher )
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            ValidationResult result = await _validator.ValidateAsync(createAndEditTeacher);


            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Create", createAndEditTeacher);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Teacher dbTeacher = new();

                    _mapper.Map(createAndEditTeacher, dbTeacher);
                    await _asyncTeacherRepository.Add(dbTeacher);

                    _notyf.Success("Teacher Created  Successfully! ");

                    return RedirectToAction("Index");
                }
            }
            
        }

        // GET: TeacherController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            _logger.LogError($"Teacher {id} not found");
           
            Teacher dbTeacher = await _asyncTeacherRepository.FindById(id);

            if (dbTeacher is null)
            {
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditTeacher>(dbTeacher));

            return View(data);
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditTeacher createAndEditTeacher)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        await using (await _asyncUnitOfWorkFactory.Create())
            //        {

            //            Teacher dbTeacherToUpdate = await _asyncTeacherRepository.FindById(createAndEditTeacher.Id);

            //            _mapper.Map(createAndEditTeacher, dbTeacherToUpdate, typeof(CreateAndEditTeacher), typeof(Teacher));

            //             _notyf.Success("Teacher Update  Successfully! ");

            //            return RedirectToAction(nameof(Index));
            //        }
            //    }
            //    catch (ModelValidationException mvex)
            //    {
            //        foreach (var error in mvex.ValidationErrors)
            //        {
            //            ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
            //        }
            //    }
            //}
            //return View();
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditTeacher);

            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Edit", createAndEditTeacher);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Teacher dbTeacherToUpdate = await _asyncTeacherRepository.FindById(createAndEditTeacher.Id);

                    _mapper.Map(createAndEditTeacher, dbTeacherToUpdate, typeof(CreateAndEditTeacher), typeof(Teacher));

                    _notyf.Success("Teacher Update  Successfully! ");

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: TeacherController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Teacher dbTeacher = await _asyncTeacherRepository.FindById(id);
            ViewBag.Message = dbTeacher.FullName;

            if (dbTeacher is null)
            {
                return NotFound();
            }
            var data = _mapper.Map<DisplayTeacher>(dbTeacher);
            return View(data);
        }

        // POST: TeacherController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Teacher dbTeacher = await _asyncTeacherRepository.FindById(id);
         await using (await _asyncUnitOfWorkFactory.Create())
            {
                 _asyncTeacherRepository.Remove(dbTeacher);

                  _notyf.Error("Teacher Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
