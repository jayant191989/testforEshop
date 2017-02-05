using HR_Management.Models;
using HR_Management.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebSolution2.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<CompanyViewModel, Company>();
            AutoMapper.Mapper.CreateMap<Company, CompanyViewModel>();
            AutoMapper.Mapper.CreateMap<EmployeeViewModel, Contact>();
            AutoMapper.Mapper.CreateMap<Contact, EmployeeViewModel>();
            AutoMapper.Mapper.CreateMap<DepartmentViewModel, Department>();
            AutoMapper.Mapper.CreateMap<Department, DepartmentViewModel>();
            AutoMapper.Mapper.CreateMap<AttendenceViewModel, Attendence>();
            AutoMapper.Mapper.CreateMap<Attendence, AttendenceViewModel>();
            AutoMapper.Mapper.CreateMap<Contact, EmployeeAttendence>();
            AutoMapper.Mapper.CreateMap<Contact, EmployeeAttendenceViewModel>();
            AutoMapper.Mapper.CreateMap<ApplicationForm, ApplicationFormViewModel>();
            AutoMapper.Mapper.CreateMap<ApplicationFormViewModel, ApplicationForm>();
            AutoMapper.Mapper.CreateMap<EmployeeAttendence, EmployeeAttendenceViewModel>();
            AutoMapper.Mapper.CreateMap<EmployeeSalaryDetailViewModel, EmployeeSalaryDetail>();
            AutoMapper.Mapper.CreateMap<EmployeeSalaryDetail, EmployeeSalaryDetailViewModel>();
            AutoMapper.Mapper.CreateMap<Contact, CustomerViewModel>();
            AutoMapper.Mapper.CreateMap<CustomerViewModel, Contact>();
            AutoMapper.Mapper.CreateMap<Membership, MembershipViewModel>();
            AutoMapper.Mapper.CreateMap<MembershipViewModel, Membership>();
            AutoMapper.Mapper.CreateMap<EnrollCustomer, EnrollCustomerViewModel>();
            AutoMapper.Mapper.CreateMap<EnrollCustomerViewModel, EnrollCustomer>();
            AutoMapper.Mapper.CreateMap<Salary, SalaryViewModel>();
            AutoMapper.Mapper.CreateMap<SalaryViewModel, Salary>();
            AutoMapper.Mapper.CreateMap<Contact, EmployeeSalaryByViewModel>();
        }
    }
}