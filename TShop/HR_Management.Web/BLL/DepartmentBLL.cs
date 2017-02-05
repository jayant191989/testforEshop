using AutoMapper;
using HR_Management.Models;
using HR_Management.Web.DAL;
using HR_Management.Web.Helpers;
using HR_Management.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace HR_Management.Web.BLL
{
    public class DepartmentBLL : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<DepartmentViewModel> GetDepartments()
        {
            var departments = unitOfWork.DepartmentRepository.Get();
            var viewModel = Mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            //  Log4NetHelper.Log("Hello !!!", LogLevel.INFO, "Department", 0, User.Identity.Name, null);
            return viewModel;
        }

        public void SaveDepartment(DepartmentViewModel viewModel)
        {
            Department department = ViewModelToModel(viewModel);
            department.Id = Guid.NewGuid();
            department.CompanyId = CompanyCookie.CompId;
            unitOfWork.DepartmentRepository.Insert(department);
            unitOfWork.Save();
            // Log4NetHelper.Log(String.Format("Deparment {0} created", viewModel.Id), LogLevel.INFO, "Department", viewModel.Id, User.Identity.Name, null);

        }
        public void EditDepartment(DepartmentViewModel viewModel)
        {
            Department department = Mapper.Map<Department>(viewModel);
            unitOfWork.DepartmentRepository.Update(department);
            unitOfWork.Save();
            //   Log4NetHelper.Log(String.Format("Deparment {0} created", viewModel.Id), LogLevel.INFO, "Department", viewModel.Id, User.Identity.Name, null);
        }
        public DepartmentViewModel GetDepartmentById(Guid? id)
        {
            Department department = unitOfWork.DepartmentRepository.GetByID(id);
            DepartmentViewModel viewModel = Mapper.Map<DepartmentViewModel>(department);
            return viewModel;
        }


        public void DeleteDepartment(Guid id)
        {
            Department department = unitOfWork.DepartmentRepository.GetByID(id);
            unitOfWork.DepartmentRepository.Delete(department);
            unitOfWork.Save();
        }
        public DepartmentViewModel ModelToViewModel(Department department)
        {
            DepartmentViewModel viewModel = Mapper.Map<DepartmentViewModel>(department);
            return viewModel;
        }

        public Department ViewModelToModel(DepartmentViewModel viewModel)
        {
            Department department = Mapper.Map<Department>(viewModel);
            return department;
        }

    }
}