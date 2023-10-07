using AutoMapper;
using BLL.Interfaces;
using BLL.Repositories;
using DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Helper;
using PL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
       
       
        private readonly IMapper mapper;

        public IUnitOfWork UnitOfWork { get; set; }

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            
            this.UnitOfWork = unitOfWork; 
          
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var employees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(await UnitOfWork.employeeRepository.GetAll());
                return View(employees);
            }
            else
            {
                var employees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(await UnitOfWork.employeeRepository.SearchName(SearchValue));
                return View(employees);
            }
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments=await UnitOfWork.departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images"); 
                var employee=mapper.Map<EmployeeViewModel,Employee>(employeeVM);
              await UnitOfWork. employeeRepository.Create(employee);
                return RedirectToAction("Index");
            }
            ViewBag.Departments =await UnitOfWork.departmentRepository.GetAll();
            return View(employeeVM);
        }
        public async Task<IActionResult> Details([FromRoute] int? Id, string viewName = "Details")
        {
            if (Id == null)
                return NotFound();
            var Employee =await UnitOfWork.employeeRepository.Get(Id);
            if (Employee == null)
                return NotFound();
            var employeeVM=mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(viewName, employeeVM);
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            ViewBag.Departments =await UnitOfWork.departmentRepository.GetAll();
            return await Details(Id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Edit([FromRoute] int? Id, EmployeeViewModel employeeVM)
        {
            if (Id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                   await UnitOfWork.employeeRepository.Update(employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(employeeVM);
                }
            }
            ViewBag.Departments =await UnitOfWork.departmentRepository.GetAll();
            return View(employeeVM);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            return await Details(Id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? Id, EmployeeViewModel EmployeeVM)
        {
            if (Id != EmployeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var employee = mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                    DocumentSettings.DeleteFile(employee.ImageName, "Images");
                    await  UnitOfWork.employeeRepository.Delete(employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    return View(EmployeeVM);
                }
            }
            return View(EmployeeVM);
        }

    }
}
