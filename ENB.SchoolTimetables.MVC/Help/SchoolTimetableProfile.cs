using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENB.SchoolTimetables.Entities;
using ENB.SchoolTimetables.MVC.Models;

namespace ENB.SchoolTimetables.MVC.Help
{
    public class SchoolTimetableProfile : Profile
    {
        public SchoolTimetableProfile()
        {


            #region Teacher 
            CreateMap<Teacher, DisplayTeacher>();

            CreateMap<CreateAndEditTeacher, Teacher>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.AddressTeacher, t => t.Ignore());
            CreateMap<Teacher, CreateAndEditTeacher>();
            #endregion


            #region Subject
            CreateMap<Subject, DisplaySubject>();

            CreateMap<CreateAndEditSubject, Subject>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Subject, CreateAndEditSubject>();

            #endregion

            #region Identity
            CreateMap<UserRegistrationModel, ApplicationUser>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            #endregion


            #region TeacherSubject
            CreateMap<SubjectTeacher, DisplayTeacherSubject>()
             .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
             .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId));

            CreateMap<CreateAndEditTeacherSubject, SubjectTeacher>()
              .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
              .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))              
              .ForMember(d => d.Teacher, t => t.Ignore())
              .ForMember(d => d.Subject, t => t.Ignore())
              .ReverseMap();
            

            #endregion

            #region ClassR 
            CreateMap<ClassR, DisplayClassR>()
              .ForMember(d => d.Subject, t => t.Ignore())
              .ForMember(d => d.Teacher, t => t.Ignore())
              .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
              .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId));
            CreateMap<CreateAndEditClassR, ClassR>()
              .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))
               .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.Subject, t => t.Ignore())
              .ForMember(d => d.Teacher, t => t.Ignore());
            CreateMap<ClassR, CreateAndEditClassR>();
            #endregion



            #region PlannedTimetable
            CreateMap<PlannedTimetable, DisplayPlannedTimeTable>()
              .ForMember(d => d.Subject, t => t.Ignore())
              .ForMember(d => d.Teacher, t => t.Ignore())
              .ForMember(d => d.ClassR, t => t.Ignore())             
              .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))
              .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
              .ForMember(d => d.ClassRId, t => t.MapFrom(y => y.ClassRId));
            CreateMap<CreateAndEditPlannedTimeTable, PlannedTimetable>()
              .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))
              .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
              .ForMember(d => d.ClassRId, t => t.MapFrom(y => y.ClassRId))
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.Subject, t => t.Ignore())
              .ForMember(d => d.Teacher, t => t.Ignore())
              .ForMember(d => d.ClassR, t => t.Ignore());
            CreateMap<PlannedTimetable, CreateAndEditPlannedTimeTable>();
            #endregion


            #region GeneratedTimetable
            CreateMap<GeneratedTimetable, DisplayGeneratedTimeTable>()
              .ForMember(d => d.ClassR, t => t.Ignore())
              .ForMember(d => d.Subject, t => t.Ignore())
              .ForMember(d => d.Teacher, t => t.Ignore())
              .ForMember(d => d.PlannedTimetable, t => t.Ignore())
              .ForMember(d => d.ClassRId, t => t.MapFrom(y => y.ClassRId))
              .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))
              .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
              .ForMember(d => d.PlannedTimetableId, t => t.MapFrom(y => y.PlannedTimetableId));
            CreateMap<CreateAndEditGeneratedTimeTable, GeneratedTimetable>()
              .ForMember(d => d.ClassRId, t => t.MapFrom(y => y.ClassRId))
              .ForMember(d => d.SubjectId, t => t.MapFrom(y => y.SubjectId))
              .ForMember(d => d.TeacherId, t => t.MapFrom(y => y.TeacherId))
              .ForMember(d => d.PlannedTimetableId, t => t.MapFrom(y => y.PlannedTimetableId))
              .ForMember(d => d.ClassR, t => t.Ignore())
              .ForMember(d => d.Subject, t => t.Ignore())
              .ForMember(d => d.Teacher, t => t.Ignore())
              .ForMember(d => d.PlannedTimetable, t => t.Ignore());             
            CreateMap<GeneratedTimetable, CreateAndEditGeneratedTimeTable>();
            #endregion

            //#region ClaimProcessing
            //CreateMap<ClaimProcessing, DisplayClaimProcessing>()
            //  .ForMember(d => d.Customer, t => t.Ignore())
            //  .ForMember(d => d.Policy, t => t.Ignore())
            //  .ForMember(d => d.ClaimHeader, t => t.Ignore())
            //  .ForMember(d => d.PolicyId, t => t.MapFrom(y => y.PolicyId))
            //  .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            //  .ForMember(d => d.ClaimHeaderId, t => t.MapFrom(y => y.ClaimHeaderId));
            //CreateMap<CreateAndEditClaimProcessing, ClaimProcessing>()
            //  .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            //  .ForMember(d => d.PolicyId, t => t.MapFrom(y => y.PolicyId))
            //  .ForMember(d => d.ClaimHeaderId, t => t.MapFrom(y => y.ClaimHeaderId))
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ForMember(d => d.Policy, t => t.Ignore())
            //  .ForMember(d => d.Customer, t => t.Ignore())
            //  .ForMember(d => d.ClaimHeader, t => t.Ignore());
            //CreateMap<ClaimProcessing, CreateAndEditClaimProcessing>();
            //#endregion


            //#region ClaimProssecingStage 
            //CreateMap<ClaimProcessingStage, DisplayClaimProcessingStage>()
            //  .ForMember(d => d.RepliesProcessingStages, t => t.Ignore())
            // .ForMember(d => d.ParentClaimStage, t => t.Ignore());
            //CreateMap<CreateAndEditClaimProcessingStage, ClaimProcessingStage>()
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.ParentClaimStage, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore());
            //CreateMap<ClaimProcessingStage, CreateAndEditClaimProcessingStage>();
            //#endregion
            //#region PatientStaff
            //CreateMap<Staff_Patient_Association, DisplayStaff_Patient_Association>()
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditStaff_Patient_Association, Staff_Patient_Association>()
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ReverseMap();

            //#endregion

            //#region Appointment
            //CreateMap<Appointment, DisplayAppointment>()
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditAppointment, Appointment>()
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.Color, t => t.MapFrom(y => y.EventStatus.ToString()))
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ReverseMap();
            //#endregion


            //#region PatientRecord
            //CreateMap<Patient_Record, DisplayPatientRecord>()
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditPatientRecord, Patient_Record>()
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ReverseMap();

            //#endregion
            //#region BookingNotes
            //CreateMap<Booking_Note, DisplayBookingNote>()
            // .ForMember(d => d.Customer, t => t.Ignore())
            // .ForMember(d => d.Booking, t => t.Ignore())            
            // .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            // .ForMember(d => d.BookingId, t => t.MapFrom(y => y.BookingId));

            //CreateMap<CreateAndEditBookingNote, Booking_Note>()
            //  .ForMember(d => d.BookingId, t => t.MapFrom(y => y.BookingId))            
            //  .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            //  .ForMember(d => d.Customer, t => t.Ignore())
            //  .ForMember(d => d.Booking, t => t.Ignore())              
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ReverseMap();

            //#endregion

            #region Address

            CreateMap<Address, EditAddress>()
                  .ForMember(d => d.TeacherId, t => t.Ignore());                  
            CreateMap<EditAddress, Address>().ConstructUsing(s => new Address(s.Number_street!, s.City!, s.Zipcode!, s.State_province_county!, s.Country!));
            #endregion
        }
    }
}
