using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectTodo.DTOs.Models;
using ProjectTodo.DTOs.RequestModel;
using ProjectTodo.DTOs.ViewModel;
using ProjectTodo.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTodo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITaskServices _taskServices;
        public HomeController(IConfiguration configuration, ITaskServices taskServices)
        {
            _configuration = configuration;
            _taskServices = taskServices;

        }

        [HttpPost]
        [Route("task/insert")]
        public async Task<IActionResult> Insert(TaskModel entity)
        {
            try
            {
                if (await _taskServices.CheckExistingCodeAsync(entity.TaskId))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        Message = string.Format("Dữ liệu này đã tồn tại"
                        , entity.TaskId)
                    });
                }

                ResponseModel response = await _taskServices.InsertAsync(entity);

                Response.StatusCode = response.StatusCode;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, ex.Message });
            }
        }


        /// <summary>
        /// delete item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("task/delete/{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            try
            {
                ResponseModel response = await _taskServices.DeleteAsync(id);

                Response.StatusCode = response.StatusCode;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, ex.Message });
            }
        }


        /// <summary>
        /// edit customer item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("task/edit")]
        public async Task<IActionResult> EditCustomerAsync(TaskModel entity)
        {
            try
            {
                if (await _taskServices.CheckExistingCodeAsync(entity.TaskId))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        Message = string.Format("Dữ liệu này đã tồn tại"
                        , entity.TaskId)
                    });
                }

                ResponseModel response = await _taskServices.EditAsync(entity);

                Response.StatusCode = response.StatusCode;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, ex.Message });
            }
        }


        /// <summary>
        /// get all customer
        /// </summary>
        /// <returns></returns>
        [HttpGet("task")]
        public async Task<IActionResult> GetAllCustomerAsync()
        {
            try
            {
                List<TaskModel> lstTask = (await _taskServices.GetAllCustomers()).ToList();

                return Ok(lstTask);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Staus = false, ex.Message });
            }
        }

    }
}
