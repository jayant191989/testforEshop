using AutoMapper;
using HR_Management.Models;
using HR_Management.Web.DAL;
using HR_Management.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.BLL
{
    public class CompanyBLL
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<CompanyViewModel> GetCompanyies()
        {
            var companyies = unitOfWork.CompanyRepository.Get();
            var viewModel = Mapper.Map<IEnumerable<CompanyViewModel>>(companyies);
            //  Log4NetHelper.Log("Hello !!!", LogLevel.INFO, "Department", 0, User.Identity.Name, null);
            return viewModel;
        }

        public CompanyViewModel GetCompanyById(Guid? id)
        {
            Company company = unitOfWork.CompanyRepository.GetByID(id);
            CompanyViewModel viewModel = Mapper.Map<CompanyViewModel>(company);
            return viewModel;
        }

        public void SaveCompany(CompanyViewModel viewModel)
        {
            Company company = Mapper.Map<Company>(viewModel);
            unitOfWork.CompanyRepository.Insert(company);
            unitOfWork.Save();
            // Log4NetHelper.Log(String.Format("Deparment {0} created", viewModel.Id), LogLevel.INFO, "Department", viewModel.Id, User.Identity.Name, null);
        }
        public void EditCompany(CompanyViewModel viewModel)
        {
            Company company = Mapper.Map<Company>(viewModel);
            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.Save();
            //   Log4NetHelper.Log(String.Format("Deparment {0} created", viewModel.Id), LogLevel.INFO, "Department", viewModel.Id, User.Identity.Name, null);
        }

        public void DeleteCompany(Guid id)
        {
            Company company = unitOfWork.CompanyRepository.GetByID(id);
            unitOfWork.CompanyRepository.Delete(company);
            unitOfWork.Save();
        }
    }
}