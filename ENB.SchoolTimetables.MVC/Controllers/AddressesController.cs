using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENB.SchoolTimetables.Entities;
using ENB.SchoolTimetables.Entities.Repositories;
using ENB.SchoolTimetables.Infrastructure;
using ENB.SchoolTimetables.MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace ENB.InsuranceAndClaims.MVC.Controllers
{
   // [Authorize]
    public class AddressesController : Controller
    {
        private readonly IAsyncTeacherRepository _asyncTeacherRepository;       
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;


        /// <summary>
        /// Initializes a new instance of the AddressesController class.
        /// </summary>
        public AddressesController(IAsyncTeacherRepository asyncTeacherRepository,                                                              
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   IMapper mapper,
                                   INotyfService notyf)
        {
            _asyncTeacherRepository = asyncTeacherRepository; 
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Edit(int TeacherId)
        {
            ViewBag.TeachId = TeacherId;
           
           
            
            var address = new Address();
            var message = "";

            if (TeacherId != 0)
            {
                var teacher = await _asyncTeacherRepository.FindById(TeacherId);
                if (teacher is null)
                {
                    return NotFound();
                }
                address = teacher.AddressTeacher;
                message = teacher.FullName;
            }

            
            var data = new EditAddress();

            ViewBag.Message = message;

            _mapper.Map(address, data);
            return View(data);
        }

        public  IActionResult Redirect(int TeacherId)
        {
            ViewBag.CustId = TeacherId;
           
            var redirect= RedirectToAction("");        
            

            if (TeacherId != 0)
            {
              redirect=  RedirectToAction("Index","Teacher");
            }

            
            return redirect;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAddress editAddressModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        if (editAddressModel.TeacherId != 0)
                        {
                            var teacher = await _asyncTeacherRepository.FindById(editAddressModel.TeacherId);
                            _mapper.Map(editAddressModel, teacher.AddressTeacher);

                            _notyf.Success("Address created  Successfully! ");

                            return RedirectToAction(nameof(Index), "Teacher");
                        }


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
    }
}
