using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.SchoolTimetables.Entities.Repositories;
using ENB.SchoolTimetables.Entities;
using ENB.SchoolTimetables.Infrastructure;
using ENB.SchoolTimetables.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using ENB.SchoolTimetables.Entities.Collections;
using System.ComponentModel.DataAnnotations;
using ENB.SchoolTimetables.MVC.Help;

namespace ENB.SchoolTimetables.MVC.Controllers
{
    public class PlannedTimeTableController : Controller
    {
        private readonly IAsyncSubjectRepository _asyncSubjectRepository;
        private readonly IAsyncTeacherRepository _asyncTeacherRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<PlannedTimeTableController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public PlannedTimeTableController(IAsyncSubjectRepository asyncSubjectRepository,
                                IAsyncTeacherRepository asyncTeacherRepository,
                                      IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                     ILogger<PlannedTimeTableController> logger,
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
        public async Task<IActionResult> List(int SubjectId, int TeacherId, int ClassrId)
        {
            ViewBag.Idsubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdClassR = ClassrId;

            var subject = await _asyncSubjectRepository.FindById(SubjectId, x=>x.ClassRs);
            var classr = subject.ClassRs.Where(t=>t.TeacherId==TeacherId).Single(y => y.Id == ClassrId);

            ViewBag.Message = classr.Classroom.GetDisplayName();

            return View();
        }


        public async Task<IActionResult> GetPlannedTimeTables(int SubjectId, int TeacherId, int ClassrId)
        {

            var plannedTimeTable = (from pln in _asyncSubjectRepository.FindAll().Where(sb=>sb.Id==SubjectId).SelectMany(x => x.PlannedTimetables)
                                       .Where(cls => cls.TeacherId == TeacherId).Where(y => y.ClassRId == ClassrId)
                           join tc in _asyncTeacherRepository.FindAll() on pln.TeacherId equals tc.Id
                           join sb in _asyncSubjectRepository.FindAll() on pln.SubjectId equals sb.Id
                           join cls in _asyncSubjectRepository.FindAll().Where(cl=>cl.Id==SubjectId).SelectMany(z=>z.ClassRs)
                           on pln.ClassRId equals cls.Id
                           select new DisplayPlannedTimeTable
                           {
                               Id = pln.Id,
                               NameClass = cls.Classroom,
                               NameSubject = sb.SubjectName,
                               NameTeacher = tc.FullName,
                               PlannedDay=pln.PlannedDay,
                               TimetableStatus=pln.TimetableStatus, 
                               Period_Number=pln.Period_Number,
                               OtherDetails = pln.OtherDetails,
                               DateCreated = pln.DateCreated,
                               DateModified = pln.DateModified

                           }).ToList();

            // ViewBag.Message = customer.FullName;

            var Mpdata = new List<DisplayPlannedTimeTable>();

            _imapper.Map(await Task.FromResult(plannedTimeTable), Mpdata);

            return Json(new { data = Mpdata });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int SubjectId, int TeacherId, int ClassrId)
        {
            ViewBag.IdSubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdClassR = ClassrId;

            var subject = await _asyncSubjectRepository.FindById(SubjectId, x=>x.ClassRs);
            var classr=subject.ClassRs.Where(x=>x.TeacherId==TeacherId).Single(y=>y.Id==ClassrId);

            ViewBag.Message = classr.Classroom.GetDisplayName();


            return View();
        }





        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditPlannedTimeTable
                                                 createAndEditPlannedTimeTable, 
                                                  int SubjectId, 
                                                  int TeacherId,
                                                  int ClassrId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var subject = await _asyncSubjectRepository.FindById(SubjectId, x=>x.ClassRs) ;

                        var classrm= subject.ClassRs.Where(tc=>tc.TeacherId==TeacherId)
                                     .Single(y=>y.Id==ClassrId);
                        
                        PlannedTimetable plannedTimetable = new ();

                        _imapper.Map(createAndEditPlannedTimeTable, plannedTimetable);
                      
                        classrm.PlannedTimetables.Add(plannedTimetable);

                        _notyf.Success("PlannedTimetable  Added  Successfully! ");

                        return RedirectToAction(nameof(List), new { SubjectId, TeacherId, ClassrId });
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


            return View();
        }




        public async Task<IActionResult> Edit(int SubjectId, int TeacherId,int ClassrId, int id)
        {

            var subject = await _asyncSubjectRepository.FindById(SubjectId, x => x.PlannedTimetables);
            var plntimetbl = subject.PlannedTimetables.Where(tc => tc.TeacherId == TeacherId)
                             .Where(cl=>cl.ClassRId==ClassrId);

            ViewBag.Message = subject.SubjectName;
            ViewBag.IdSubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdClassR = ClassrId;

            ViewBag.Id = id;


            if (subject is null)
            {
                return NotFound();
            }

            CreateAndEditPlannedTimeTable data = new();


            _imapper.Map(plntimetbl.Single(x => x.Id == id), data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditPlannedTimeTable
                                         createAndEditPlannedTimeTable,
                                           int SubjectId,
                                           int TeacherId,
                                           int ClassrId)
        {

            ViewBag.IdSubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdClassR = ClassrId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var subject = await _asyncSubjectRepository.FindById(SubjectId, x => x.PlannedTimetables);
                        var plannedTimeTable = subject.PlannedTimetables.Where(x => x.ClassRId == ClassrId)
                                                   .Where(y=>y.TeacherId==TeacherId);                       

                        _imapper.Map(createAndEditPlannedTimeTable, plannedTimeTable.Single(x => x.Id == createAndEditPlannedTimeTable.Id));

                        _notyf.Success("PlannedTimeTable updated Successfully");

                        return RedirectToAction(nameof(List), new { SubjectId, TeacherId, ClassrId });
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

            return View();
        }

        public async Task<IActionResult> Details(int SubjectId,int TeacherId, int ClassrId, int id)
        {

            var subject = await _asyncSubjectRepository.FindById(SubjectId);
            ViewBag.Message = subject.SubjectName;
            ViewBag.IdSubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdClassR = ClassrId;
            ViewBag.Id = id;


            var lstplntimetbl = from pln in _asyncSubjectRepository.FindAll().Where(x => x.Id == SubjectId).SelectMany(p => p.PlannedTimetables)
                                        .Where(y=>y.TeacherId== TeacherId).Where(z=>z.ClassRId== ClassrId)
                           join sb in _asyncSubjectRepository.FindAll() on pln.SubjectId equals sb.Id
                           join tc in _asyncTeacherRepository.FindAll() on pln.TeacherId equals tc.Id
                           join cls in _asyncTeacherRepository.FindAll().Where(t=>t.Id==TeacherId).SelectMany(z=>z.ClassRs)
                               on pln.ClassRId equals cls.Id
                           select new DisplayPlannedTimeTable
                           {
                               Id = pln.Id,
                               NameSubject = sb.SubjectName,
                               NameTeacher = tc.FullName,
                               PlannedDay = pln.PlannedDay,
                               Period_Number=pln.Period_Number,
                               NameClass=cls.Classroom,
                               OtherDetails=pln.OtherDetails,
                               DateCreated = pln.DateCreated,
                               DateModified = pln.DateModified


                           };


            if (lstplntimetbl is null)
            {
                return NotFound();
            }

            var sglTimetble = lstplntimetbl.Single(x => x.Id == id);

            ViewBag.Nameclass = sglTimetble.NameClass;

            return View(sglTimetble);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int SubjectId, int TeacherId, int ClassrId, int id)
        {
            var subject = await _asyncSubjectRepository.FindById(SubjectId);
            ViewBag.Message = subject.SubjectName;
            ViewBag.IdSubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdClassR = ClassrId;
            ViewBag.Id = id;


            var lstplntimetbl = from pln in _asyncSubjectRepository.FindAll().Where(x => x.Id == SubjectId).SelectMany(p => p.PlannedTimetables)
                                     .Where(y => y.TeacherId == TeacherId).Where(z => z.ClassRId == ClassrId)
                                join sb in _asyncSubjectRepository.FindAll() on pln.SubjectId equals sb.Id
                                join tc in _asyncTeacherRepository.FindAll() on pln.TeacherId equals tc.Id
                                join cls in _asyncTeacherRepository.FindAll().Where(t => t.Id == TeacherId).SelectMany(z => z.ClassRs)
                                    on pln.ClassRId equals cls.Id
                                select new DisplayPlannedTimeTable
                                {
                                    Id = pln.Id,
                                    NameSubject = sb.SubjectName,
                                    NameTeacher = tc.FullName,
                                    PlannedDay = pln.PlannedDay,
                                    Period_Number = pln.Period_Number,
                                    NameClass = cls.Classroom,
                                    OtherDetails = pln.OtherDetails,
                                    DateCreated = cls.DateCreated,
                                    DateModified = cls.DateModified


                                };


            if (lstplntimetbl is null)
            {
                return NotFound();
            }

            var sglTimetble = lstplntimetbl.Single(x => x.Id == id);



            return View(sglTimetble);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayPlannedTimeTable
                                                displayPlannedTimeTable, 
                                                int SubjectId,
                                                int TeacherId,
                                                int ClassrId)
        {
            // ViewBag.Case_Id = caseid;
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var subject = await _asyncSubjectRepository.FindById(SubjectId, x => x.PlannedTimetables);
                var plntimetbl = subject.PlannedTimetables.Where(x => x.TeacherId == TeacherId)
                                   .Where(y=>y.ClassRId==ClassrId)
                                   .Single(z=>z.Id==displayPlannedTimeTable.Id);

                subject.PlannedTimetables.Remove(plntimetbl);

                _notyf.Error("PlannedTimetable related to ClassR removed  Successfully");
            }
            return RedirectToAction(nameof(List), new { SubjectId,TeacherId,ClassrId });
        }
    }
}
