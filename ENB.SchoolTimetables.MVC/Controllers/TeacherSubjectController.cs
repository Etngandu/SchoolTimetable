using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.SchoolTimetables.Entities;
using ENB.SchoolTimetables.Entities.Collections;
using ENB.SchoolTimetables.Entities.Repositories;
using ENB.SchoolTimetables.Infrastructure;
using ENB.SchoolTimetables.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Restaurant.Event.Bookings.MVC.Controllers
{
   // [Authorize]
    public class TeacherSubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IAsyncTeacherRepository _asyncTeacherRepository;
        private readonly IAsyncSubjectRepository  _asyncSubjectRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<TeacherSubjectController> _logger;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;


        public TeacherSubjectController(IAsyncTeacherRepository asyncTeacherRepository,
                           IAsyncSubjectRepository asyncSubjectRepository,
                           IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                           ILogger<TeacherSubjectController> logger,
                           IMapper mapper,
                           INotyfService notyf)
        {
            _asyncTeacherRepository = asyncTeacherRepository;
            _asyncSubjectRepository = asyncSubjectRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _logger = logger;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> SubjectsperTeacherList(int TeacherId)
        {
            ViewBag.Idteacher = TeacherId;
            var teacher = await _asyncTeacherRepository.FindById(TeacherId);

            ViewBag.Message = teacher.FullName;

            return View();
        }

        public async Task<IActionResult> TeachersperSubjectList(int SubjectId)
        {
            ViewBag.Idsubject = SubjectId;
            var subject = await _asyncSubjectRepository.FindById(SubjectId);

            ViewBag.Message = subject.SubjectName;

            return View();
        }


        public  IActionResult GetSubjectsperTeacherData(int TeacherId)
        {

            var allSub = (from st in _asyncTeacherRepository.FindAll().Where(x => x.Id == TeacherId).SelectMany(y => y.SubjectTeachers)
                           join tc in _asyncTeacherRepository.FindAll() on st.TeacherId equals tc.Id
                           join sb in _asyncSubjectRepository.FindAll() on st.SubjectId equals sb.Id
                           select new DisplayTeacherSubject
                           {                               
                               
                               Id=sb.Id!,
                               TeacherName = tc.FullName,
                               SubjectName=sb.SubjectName  ,
                               DateCreated = st.DateCreated,
                               DateModified = st.DateModified

                           }).ToList();           


            return Json(new { data = allSub });


        }

        public IActionResult GetTeachersperSubjectData(int SubjectId)
        {

            var allTeach = (from st in _asyncSubjectRepository.FindAll().Where(x => x.Id == SubjectId).SelectMany(y => y.SubjectTeachers)
                            join tc in _asyncTeacherRepository.FindAll() on st.TeacherId equals tc.Id
                            join sb in _asyncSubjectRepository.FindAll() on st.SubjectId equals sb.Id
                            select new DisplayTeacherSubject
                            {

                                Id = tc.Id!,
                                TeacherName = tc.FullName,
                                SubjectName = sb.SubjectName,
                                DateCreated = st.DateCreated,
                                DateModified = st.DateModified

                            }).ToList();


            return Json(new { data = allTeach });


        }

        [HttpGet]
        public async Task<IActionResult> SubjectsperTeacherCreate(int TeacherId)
        {
            ViewBag.IdTeacher = TeacherId;

            var data = new CreateAndEditTeacherSubject()
            {

                ListSubject = _asyncSubjectRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.SubjectName,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()

            };

            var teacher = await _asyncTeacherRepository.FindById(TeacherId);

            ViewBag.Message = teacher.FullName;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> TeachersperSubjectCreate(int SubjectId)
        {
            ViewBag.Idsubject = SubjectId;

            var data = new CreateAndEditTeacherSubject()
            {

                ListTeacher = _asyncTeacherRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.FullName,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()

            };

            var subject = await _asyncSubjectRepository.FindById(SubjectId);

            ViewBag.Message = subject.SubjectName;

            return View(data);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  SubjectsperTeacherCreate(CreateAndEditTeacherSubject createAndEditTeacherSubject,
                                                  int TeacherId, IFormCollection form)
        {
            var subjectTeacherList = await _asyncTeacherRepository.FindById(TeacherId, x => x.SubjectTeachers);

            string strDDLValue = form["Model.ListSubject.Text"].ToString();

            if (subjectTeacherList.SubjectTeachers.Any(x => x.SubjectId == createAndEditTeacherSubject.SubjectId))
            {
                // throw new  ModelValidationException($" The subject {createAndEditTeacherSubject.SubjectId} alredy exist"  );
                _notyf.Error($" This subject {createAndEditTeacherSubject.SubjectId} already exist ");

                var ListSubject = _asyncSubjectRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.SubjectName,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList();

                createAndEditTeacherSubject.ListSubject = ListSubject;

                return View("SubjectsperTeacherCreate", createAndEditTeacherSubject);
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        

                        var teacher = await _asyncTeacherRepository.FindById(TeacherId);
                        
                        
                        SubjectTeacher subjectTeacher = new();

                        _mapper.Map(createAndEditTeacherSubject, subjectTeacher);
                         
                        teacher.SubjectTeachers.Add(subjectTeacher);

                        _notyf.Success("subjectTeacher  Added  Successfully! ");

                        return RedirectToAction(nameof(SubjectsperTeacherList), new { TeacherId });
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


            

            return View("SubjectsperTeacherCreate", createAndEditTeacherSubject);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeachersperSubjectCreate(CreateAndEditTeacherSubject  createAndEditTeacherSubject,
                                                                   int SubjectId)
        {

            var teacherSubjectList = await _asyncSubjectRepository.FindById(SubjectId, x => x.SubjectTeachers);

           // string strDDLValue = form["drpEmpList"].ToString();

            if (teacherSubjectList.SubjectTeachers.Any(x => x.TeacherId == createAndEditTeacherSubject.TeacherId))
            {
                // throw new  ModelValidationException($" The subject {createAndEditTeacherSubject.SubjectId} alredy exist"  );
                _notyf.Error($" This Teacher {createAndEditTeacherSubject.TeacherId} already exist ");

                var listTeacher = _asyncTeacherRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.FullName,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList();

                createAndEditTeacherSubject.ListTeacher = listTeacher;

                return View("TeachersperSubjectCreate", createAndEditTeacherSubject);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var subject = await _asyncSubjectRepository.FindById(SubjectId);


                       
                        SubjectTeacher subjectTeacher = new();

                        _mapper.Map(createAndEditTeacherSubject, subjectTeacher);
                    
                        subject.SubjectTeachers.Add(subjectTeacher);    

                        _notyf.Success("subjectTeacher  Added  Successfully! ");

                        return RedirectToAction(nameof(TeachersperSubjectList), new { SubjectId });
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

   

      


       
       

        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> SubjectsperTeacherDelete(int TeacherId, int SubjectId)
        {
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdSubject = SubjectId;
            

            var allSub = (from st in _asyncTeacherRepository.FindAll().Where(x => x.Id == TeacherId).SelectMany(y => y.SubjectTeachers)
                          join tc in _asyncTeacherRepository.FindAll() on st.TeacherId equals tc.Id
                          join sb in _asyncSubjectRepository.FindAll() on st.SubjectId equals sb.Id
                          select new DisplayTeacherSubject
                          {

                              // Id = (int)mm.SubjectId!,
                              SubjectId = st.SubjectId!,
                              TeacherId = st.TeacherId!,
                              TeacherName = tc.FullName,
                              SubjectName = sb.SubjectName

                          }).ToList();





            var mysglSubl = await Task.FromResult(allSub.Single(y => y.SubjectId == SubjectId));


            ViewBag.Message = mysglSubl.TeacherName;
            return View(mysglSubl);
        }

        public async Task<IActionResult> TeachersperSubjectDelete(int TeacherId, int SubjectId)
        {
            ViewBag.IdTeacher = TeacherId;
            ViewBag.IdSubject = SubjectId;
          

            var allTeach = (from st in _asyncSubjectRepository.FindAll().Where(x => x.Id == SubjectId).SelectMany(y => y.SubjectTeachers)
                            join sb in _asyncSubjectRepository.FindAll() on st.SubjectId equals sb.Id
                            join tc in _asyncTeacherRepository.FindAll() on st.TeacherId equals tc.Id
                            select new DisplayTeacherSubject
                            {

                                // Id = (int)mm.SubjectId!,
                                SubjectId = st.SubjectId!,
                                TeacherId = st.TeacherId!,
                                TeacherName = tc.FullName,
                                SubjectName = sb.SubjectName

                            }).ToList();





            var mysglteach = await Task.FromResult(allTeach.Single(y => y.TeacherId == TeacherId));



            ViewBag.Message = mysglteach.SubjectName;
            return View(mysglteach);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubjectsperTeacherDelete(DisplayTeacherSubject displayTeacherSubject, int TeacherId)
        {
           
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var teacher = await _asyncTeacherRepository.FindById(TeacherId, x => x.SubjectTeachers);
                var subjectTeacher = await Task.FromResult(teacher.SubjectTeachers.Single(x => x.SubjectId == displayTeacherSubject.SubjectId));


                teacher.SubjectTeachers.Remove(subjectTeacher);

                _notyf.Error("subjectTeacher  removed  Successfully");
            }
            return RedirectToAction(nameof(SubjectsperTeacherList), new { TeacherId });
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeachersperSubjectDelete(DisplayTeacherSubject  displayTeacherSubject, int SubjectId)
        {
            // ViewBag.Case_Id = caseid;
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var subject = await _asyncSubjectRepository.FindById(SubjectId, x => x.SubjectTeachers);
                var subjectTeacher = await Task.FromResult(subject.SubjectTeachers.Single(x => x.TeacherId == displayTeacherSubject.TeacherId));


                subject.SubjectTeachers.Remove(subjectTeacher);

                _notyf.Error("subjectTeacher  removed  Successfully");
            }
            return RedirectToAction(nameof(TeachersperSubjectList), new { SubjectId });
        }
    }
}
