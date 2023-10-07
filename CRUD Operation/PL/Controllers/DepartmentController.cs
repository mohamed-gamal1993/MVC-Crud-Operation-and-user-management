using AutoMapper;
using BLL.Interfaces;
using BLL.Repositories;
using DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
    {
       
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper  )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue) 
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var departments = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(await unitOfWork.departmentRepository.GetAll());
                return View(departments);
            }
            else
            {
                var departments = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(await unitOfWork.departmentRepository.SearchName(SearchValue));
                return View(departments);
            }
            
        }
        public IActionResult Create()
        {
            return  View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentViewModel )
        {
            if(ModelState.IsValid)
            {
              var department=mapper.Map<DepartmentViewModel,Department>(departmentViewModel);
                await unitOfWork.departmentRepository.Create(department);
                return RedirectToAction("Index");
            }
            return View(departmentViewModel);
        }
        public async Task<IActionResult> Details([FromRoute]int? Id,string viewName="Details")
        {
            if (Id == null)
                return NotFound();
            var department=await unitOfWork.departmentRepository.Get(Id);
            if (department== null)
                return NotFound();
            var departmentVM = mapper.Map<Department, DepartmentViewModel>(department);
            return View(viewName,departmentVM);
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            return await Details(Id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int?Id,DepartmentViewModel departmentViewModel)
        {
            if (Id != departmentViewModel.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var department=mapper.Map<DepartmentViewModel,Department>(departmentViewModel);
                   await  unitOfWork.departmentRepository.Update(department);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(departmentViewModel);
                }
            }return View(departmentViewModel);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            return await Details(Id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int?Id,DepartmentViewModel departmentViewModel)
        {
            if (Id != departmentViewModel.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var department = mapper.Map<DepartmentViewModel, Department>(departmentViewModel);
                   await unitOfWork.departmentRepository.Delete(department);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {

                    return View(departmentViewModel);
                }
            }
            return View(departmentViewModel);
        }
    }
}
