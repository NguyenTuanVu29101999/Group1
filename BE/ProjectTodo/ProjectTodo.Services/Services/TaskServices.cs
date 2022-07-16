using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectTodo.DTOs.Models;
using ProjectTodo.DTOs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTodo.Services.Services
{
    public interface ITaskServices
    {
        Task<List<TaskModel>> GetAllCustomers();
        Task<bool> CheckExistingCodeAsync(int id = default);
        Task<TaskModel> GetByIdAsync(int id);
        Task<ResponseModel> InsertAsync(TaskModel model);
        Task<ResponseModel> EditAsync(TaskModel model);
        Task<ResponseModel> DeleteAsync(int id);
    }
    public class TaskServices : ITaskServices
    {
        private readonly ToDoContext _context;
        public TaskServices(ToDoContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckExistingCodeAsync(int id = 0)
        {
            try
            {
                TaskModel task  = await _context.TaskModel.FirstOrDefaultAsync(m => m.TaskId != id);

                return task != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel> DeleteAsync(int id)
        {
            try
            {
                TaskModel entity = await _context.TaskModel.FindAsync(id);
                if (entity == null)
                {
                    await _context.SaveChangesAsync();
                    return new ResponseModel() { Message = "Không tìm thấy dữ liệu cần xóa", StatusCode = StatusCodes.Status404NotFound, Status = false };
                }

                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return new ResponseModel() { Message = "Xóa thành công", StatusCode = StatusCodes.Status200OK, Status = true };
            }
            catch
            {
                await _context.SaveChangesAsync();
                return new ResponseModel() { Message = "Xóa thất bại", StatusCode = StatusCodes.Status400BadRequest, Status = false };
            }
        }

        public async Task<ResponseModel> EditAsync(TaskModel model)
        {
            try
            {
                TaskModel entity = await _context.TaskModel.FindAsync(model.TaskId);
                if (entity == null)
                {
                    await _context.SaveChangesAsync();
                    return new ResponseModel() { Message = "Không tìm thấy dữ liệu", StatusCode = StatusCodes.Status404NotFound };
                }

                entity.CreateDate = model.CreateDate;

                _context.Entry(entity).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();

                return new ResponseModel() { Message = "Sửa thành công", StatusCode = StatusCodes.Status200OK };
            }
            catch
            {
                await _context.SaveChangesAsync();
                return new ResponseModel() { Message = "Sửa thất bại", StatusCode = StatusCodes.Status400BadRequest };
            }
        }

        public async Task<List<TaskModel>> GetAllCustomers()
        {
            try
            {
                List<TaskModel> tasks = await _context.TaskModel.ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TaskModel> GetByIdAsync(int id)
        {
            try
            {
                TaskModel taskModel = await _context.TaskModel.FirstOrDefaultAsync(m => m.TaskId == id );
                return taskModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel> InsertAsync(TaskModel model)
        {
            try
            {
                var customer = new TaskModel()
                {
                    TaskName = model.TaskName,
                    CreateDate = DateTime.Now
                };

                await _context.AddAsync(customer);
                await _context.SaveChangesAsync();
                return new ResponseModel() { Message = "Thêm thành công", StatusCode = StatusCodes.Status200OK, Status = true };
            }
            catch
            {
                await _context.SaveChangesAsync();
                return new ResponseModel() { Message = "Xóa thành công", StatusCode = StatusCodes.Status400BadRequest, Status = false };
            }
        }
    }
}
