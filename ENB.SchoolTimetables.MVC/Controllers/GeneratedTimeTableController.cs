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
    public class GeneratedTimeTableController : Controller
    {
        private readonly IAsyncSubjectRepository _asyncSubjectRepository;
        private readonly IAsyncTeacherRepository _asyncTeacherRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<GeneratedTimeTableController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public GeneratedTimeTableController(IAsyncSubjectRepository asyncSubjectRepository,
                                        IAsyncTeacherRepository asyncTeacherRepository,
                                        IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                        ILogger<GeneratedTimeTableController> logger,
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
        public IActionResult Index(DateTime? eventDate)
        {
            ViewBag.EventDate = eventDate ?? DateTime.Now;
            return View();
        }
        public async Task<IActionResult> List(int SubjectId,
                                              int TeacherId, 
                                              int ClassrId,
                                              int PlannedTimetableId)
        {
            ViewBag.Idsubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdClassR = ClassrId;
            ViewBag.IdPlannedTimetableId = PlannedTimetableId;

            var subject = await _asyncSubjectRepository.FindById(SubjectId, x=>x.GeneratedTimetables);
            var generatedTimetable = subject.GeneratedTimetables.Where(t=>t.TeacherId==TeacherId)
                                   .Where(cl=>cl.ClassRId==ClassrId).Single(y => y.PlannedTimetableId == PlannedTimetableId);

           // ViewBag.Message = generatedTimetable.Classroom.GetDisplayName();

            return View();
        }


        public async Task<IActionResult> GetGeneratedTimeTables(int SubjectId,
                                                                int TeacherId, 
                                                                int ClassrId,
                                                                int PlannedTimetableId)
        {

            var generatedTimeTable = (from gn in _asyncSubjectRepository.FindAll().Where(sb=>sb.Id==SubjectId).SelectMany(x => x.GeneratedTimetables)
                                       .Where(cls => cls.TeacherId == TeacherId).Where(y => y.ClassRId == ClassrId)
                                       .Where(z=>z.PlannedTimetableId==PlannedTimetableId)
                           join tc in _asyncTeacherRepository.FindAll() on gn.TeacherId equals tc.Id
                           join sb in _asyncSubjectRepository.FindAll() on gn.SubjectId equals sb.Id
                           join cls in _asyncSubjectRepository.FindAll().Where(cl=>cl.Id==SubjectId).SelectMany(z=>z.ClassRs)
                           on gn.ClassRId equals cls.Id
                           select new DisplayGeneratedTimeTable
                           {
                               Id = gn.Id,
                               NameClass = cls.Classroom,
                               NameSubject = sb.SubjectName,
                               NameTeacher = tc.FullName,
                               Start=DateTime.Now,
                               End=DateTime.Now,
                               Color=gn.Color,
                               Classroom=cls.Classroom,                               
                               DateCreated = gn.DateCreated,
                               DateModified = gn.DateModified

                           }).ToList();

            // ViewBag.Message = customer.FullName;

            var Mpdata = new List<DisplayGeneratedTimeTable>();

            _imapper.Map(await Task.FromResult(generatedTimeTable), Mpdata);

            return Json(new { data = Mpdata });


        }

        public JsonResult GetEvents()
        {
            var generatedTimeTable = (from gn in _asyncSubjectRepository.FindAll().SelectMany(x => x.GeneratedTimetables)                                      
                                      join tc in _asyncTeacherRepository.FindAll() on gn.TeacherId equals tc.Id
                                      join sb in _asyncSubjectRepository.FindAll() on gn.SubjectId equals sb.Id
                                      join cls in _asyncSubjectRepository.FindAll().SelectMany(z => z.ClassRs)
                                        on gn.ClassRId equals cls.Id
                                      select new DisplayGeneratedTimeTable
                                      {
                                          Id = gn.Id,                                         
                                          NameSubject = sb.SubjectName,
                                          NameTeacher = tc.FullName,
                                          Start = gn.Start,
                                          End = gn.End,
                                          Title= sb.SubjectName,
                                          Description="Teacher: " + tc.FullName,
                                          Color = gn.Color,
                                          Classroom = cls.Classroom,
                                          DateCreated = gn.DateCreated,
                                          DateModified = gn.DateModified

                                      }).ToList();

            return Json(generatedTimeTable);
        }

            [HttpGet]
        public async Task<IActionResult> Create(int SubjectId, 
                                                int TeacherId, 
                                                int ClassrId,
                                                int PlannedTimetableId)
        {
            ViewBag.IdSubject = SubjectId;
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdClassR = ClassrId;
            ViewBag.IdPlannedTimetable = PlannedTimetableId;

            var subject = await  _asyncSubjectRepository.FindById(SubjectId,x=>x.PlannedTimetables);
            var plannedTimetable = subject.PlannedTimetables.Where(x=>x.TeacherId==TeacherId)
                                   .Where(y => y.ClassRId == ClassrId).Single(z=>z.Id==PlannedTimetableId);

            ViewBag.Message = plannedTimetable.Period_Number;


            return View();
        }





        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditGeneratedTimeTable
                                                 createAndEditGeneratedTimeTable, 
                                                  int SubjectId, 
                                                  int TeacherId,
                                                  int ClassrId,
                                                  int PlannedTimetableId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var subject = await _asyncSubjectRepository.FindById(SubjectId, x=>x.PlannedTimetables) ;

                        var plannedTimetable = subject.PlannedTimetables.Where(tc=>tc.TeacherId==TeacherId)
                                             .Where(y => y.ClassRId == ClassrId).Single(z=>z.Id==PlannedTimetableId);
                       
                        if (plannedTimetable is null) { return NotFound(); }
                       
                        GeneratedTimetable generatedTimetable = new();
                        
                    var start = Convert.ToDateTime(plannedTimetable.PlannedDay.Date.ToShortDateString() + " " + plannedTimetable.Period_Number.GetDisplayName().Substring(9, 5));
                    var end = Convert.ToDateTime(plannedTimetable.PlannedDay.Date.ToShortDateString() + " " + plannedTimetable.Period_Number.GetDisplayName().Substring(17, 5));
                    createAndEditGeneratedTimeTable.Start = start;
                    createAndEditGeneratedTimeTable.End = end;
                    createAndEditGeneratedTimeTable.Color = createAndEditGeneratedTimeTable.TimetableStatus.ToString();
                    plannedTimetable.TimetableStatus = createAndEditGeneratedTimeTable.TimetableStatus;
                        
                        _imapper.Map(createAndEditGeneratedTimeTable, generatedTimetable);
                      
                      
                        plannedTimetable.GeneratedTimetables.Add(generatedTimetable);

                        _notyf.Success("GeneratedTimetable  Added  Successfully! ");

                        return RedirectToAction(nameof(List), "PlannedTimeTable", new { SubjectId, TeacherId, ClassrId });
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
