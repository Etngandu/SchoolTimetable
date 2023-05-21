using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.SchoolTimetables.Entities;
using ENB.SchoolTimetables.Entities.Collections;
using ENB.SchoolTimetables.Entities.Repositories;
using ENB.SchoolTimetables.Infrastructure;
using ENB.SchoolTimetables.MVC.Help;
using ENB.SchoolTimetables.MVC.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ENB.SchoolTimetables.MVC.Controllers
{
    public class ClassRController : Controller
    {
       
        private readonly IAsyncSubjectRepository  _asyncSubjectRepository;
        private readonly IAsyncTeacherRepository _asyncTeacherRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<ClassRController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public ClassRController(IAsyncSubjectRepository asyncSubjectRepository ,
                                IAsyncTeacherRepository asyncTeacherRepository,
                                      IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                     ILogger<ClassRController> logger,
                                     IMapper imapper,
                                     INotyfService notyf)
        {
            _asyncSubjectRepository = asyncSubjectRepository;
            _asyncTeacherRepository = asyncTeacherRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _logger = logger;
            _imapper = imapper;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List(int SubjectId, int TeacherId)
        {
            ViewBag.Idsubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;

            var subject = await _asyncSubjectRepository.FindById(SubjectId);

            ViewBag.Message = subject.SubjectName;

            return View();
        }


        public async Task<IActionResult> GetClassRooms(int SubjectId, int TeacherId)
        {

            var classrm = (from cls in _asyncSubjectRepository.FindAll().SelectMany(x => x.ClassRs)
                                       .Where(cls=>cls.TeacherId == TeacherId).Where(y=>y.SubjectId == SubjectId)
                              join tc in _asyncTeacherRepository.FindAll() on cls.TeacherId equals tc.Id
                            select new DisplayClassR
                            {
                                Id = cls.Id,
                                Classroom=cls.Classroom,
                                ClassDescription=cls.ClassDescription,
                                NameTeacher=tc.FullName,
                                DateCreated = cls.DateCreated,
                                DateModified = cls.DateModified

                            }).ToList();

            // ViewBag.Message = customer.FullName;

            var Mpdata = new List<DisplayClassR>();

            _imapper.Map(await Task.FromResult(classrm), Mpdata);

            return Json(new { data = Mpdata });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int SubjectId, int TeacherId)
        {
            ViewBag.IdSubject = SubjectId;   
            ViewBag.IdTeacher = TeacherId;

            var subject = await _asyncSubjectRepository.FindById(SubjectId);

            ViewBag.Message = subject.SubjectName;


            return View();
        }

       



        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditClassR  createAndEditClassR, int SubjectId, int TeacherId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var subject = await _asyncSubjectRepository.FindById(SubjectId);

                        
                        ClassR classR = new();

                        _imapper.Map(createAndEditClassR, classR);

                        subject.ClassRs.Add(classR);

                        _notyf.Success("Class room  Added  Successfully! ");

                        return RedirectToAction(nameof(List), new { SubjectId, TeacherId });
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
            

            return View("Create", createAndEditClassR);
        }




        public async Task<IActionResult> Edit(int SubjectId,int TeacherId, int id)
        {

            var subject = await _asyncSubjectRepository.FindById(SubjectId, x => x.ClassRs);
            ViewBag.Message = subject.SubjectName;
            ViewBag.IdSubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.Id = id;


            if (subject is null)
            {
                return NotFound();
            }

            CreateAndEditClassR data = new();
           

            _imapper.Map(subject.ClassRs.Single(x => x.Id == id), data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditClassR 
                                         createAndEditClassR, 
                                           int SubjectId,
                                           int TeacherId)
        {

            ViewBag.IdSubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var subject = await _asyncSubjectRepository.FindById(SubjectId, x => x.ClassRs);
                        var classrm = subject.ClassRs.Where(x => x.TeacherId == TeacherId);

                        _imapper.Map(createAndEditClassR, classrm.Single(y=>y.Id==createAndEditClassR.Id));

                        _notyf.Success("Classroom updated Successfully");

                        return RedirectToAction(nameof(List), new { SubjectId, TeacherId });
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
                       
            return View("Edit", createAndEditClassR);
        }

        public async Task<IActionResult> Details(int subjectId, int id)
        {

            var subject = await _asyncSubjectRepository.FindById(subjectId);
            ViewBag.Message = subject.SubjectName;
            ViewBag.IdSubject = subjectId;
            ViewBag.Id = id;


            var lstclsrm = from cls in _asyncSubjectRepository.FindAll().Where(x => x.Id == subjectId).SelectMany(p => p.ClassRs)
                              join sb in _asyncSubjectRepository.FindAll() on cls.SubjectId equals sb.Id
                             
                              select new DisplayClassR
                              {
                                  Id = cls.Id,
                                  SubjectId = sb.Id,
                                  Classroom = cls.Classroom,
                                  ClassDescription = cls.ClassDescription,
                                  DateCreated = cls.DateCreated,
                                  DateModified = cls.DateModified
                                  

                              };


            if (lstclsrm is null)
            {
                return NotFound();
            }

            var sglClsrm = lstclsrm.Single(x => x.Id == id);

            

            return View(sglClsrm);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int SubjectId,int TeacherId, int id)
        {
            var subject = await _asyncSubjectRepository.FindById(SubjectId);
            ViewBag.Message = subject.SubjectName;
            ViewBag.IdSubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.Id = id;


            var lstclsrm = from cls in _asyncSubjectRepository.FindAll().Where(x => x.Id == SubjectId).SelectMany(p => p.ClassRs)
                           join sb in _asyncSubjectRepository.FindAll() on cls.SubjectId equals sb.Id

                           select new DisplayClassR
                           {
                               Id = cls.Id,
                               SubjectId = sb.Id,
                               Classroom=cls.Classroom,
                               ClassDescription=cls.ClassDescription,
                               DateCreated = cls.DateCreated,
                               DateModified = cls.DateModified


                           };


            if (lstclsrm is null)
            {
                return NotFound();
            }

            var sglClsrm = lstclsrm.Single(x => x.Id == id);

            ViewBag.Class = sglClsrm.Classroom.GetDisplayName();

            return View(sglClsrm);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayClassR  displayClassR, int SubjectId)
        {
            // ViewBag.Case_Id = caseid;
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var subject = await _asyncSubjectRepository.FindById(SubjectId, x => x.ClassRs);
                var classrm = subject.ClassRs.Single(x => x.Id == displayClassR.Id);

                subject.ClassRs.Remove(classrm);

                _notyf.Error("Classroom related to Subject removed  Successfully");
            }
            return RedirectToAction(nameof(List), new { SubjectId });
        }
    }
}
