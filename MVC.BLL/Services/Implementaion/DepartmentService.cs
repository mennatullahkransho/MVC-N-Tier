using AutoMapper;
using MVC.BLL.ModelVM.Department;
using MVC.BLL.ModelVM.ResultResponse;
using MVC.DAL.Entities;
using MVC.DAL.Repo.Abstraction;

namespace MVC.BLL.Services.Implementaion
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo _departmentRepo;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepo departmentRepo, IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public Response<int> Create(CreateDepartmentVM createDepartment)
        {
            try
            {
                var dep = new Department(createDepartment.Name, createDepartment.Area,"Menna" );

                var added = _departmentRepo.Add(dep);
                if (!added)
                    return new Response<int>(0, "Could not add department", true);

                return new Response<int>(dep.Id, null, false);
            }
            catch (Exception ex)
            {
                return new Response<int>(0, ex.Message, true);
            }
        }

        public Response<bool> Edit(EditDepartmentVM editDepartment)
        {
            try
            {
                var old = _departmentRepo.GetById(editDepartment.Id);
                if (old == null)
                    return new Response<bool>(false, "Department not found", true);

                var updated = old.Update(editDepartment.Name, editDepartment.Area, editDepartment.UpdatedBy ?? "System");
                if (!updated)
                    return new Response<bool>(false, "Update failed - maybe invalid updatedBy", true);

                var result = _departmentRepo.Edit(old);
                if (!result)
                    return new Response<bool>(false, "Save failed", true);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }
        public Response<bool> ToggleStatus(int Id)
        {
            try
            {
                var result = _departmentRepo.ToggleStatus(Id);
                if (!result)
                    return new Response<bool>(false, "Toggle failed", true);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<List<GetDepartmentVM>> GetAll(bool includeDeleted)
        {
            try
            {
                if (includeDeleted)
                {
                    var all = _departmentRepo.GetAll(null);
                    return new Response<List<GetDepartmentVM>>(_mapper.Map<List<GetDepartmentVM>>(all), null, false);
                }
                else
                {
                    var active = _departmentRepo.GetAll(e => e.IsDeleted == false);
                    return new Response<List<GetDepartmentVM>>(_mapper.Map<List<GetDepartmentVM>>(active), null, false);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<GetDepartmentVM>>(null, ex.Message, true);
            }
        }

        public Response<GetDepartmentVM> GetById(int Id)
        {
            try
            {
                var dep = _departmentRepo.GetById(Id);
                if (dep == null)
                    return new Response<GetDepartmentVM>(null, "Department not found", true);

                var vm = _mapper.Map<GetDepartmentVM>(dep);
                return new Response<GetDepartmentVM>(vm, null, false);
            }
            catch (Exception ex)
            {
                return new Response<GetDepartmentVM>(null, ex.Message, true);
            }
        }

        public Response<List<GetDepartmentVM>> GetActiveDepartments()
        {
            try
            {
                var result = _departmentRepo.GetAll(d => d.IsDeleted == false);
                var map = _mapper.Map<List<GetDepartmentVM>>(result);
                return new Response<List<GetDepartmentVM>>(map, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetDepartmentVM>>(null, ex.Message, true);
            }
        }

        public Response<List<GetDepartmentVM>> GetNotActiveDepartments()
        {
            try
            {
                var result = _departmentRepo.GetAll(d => d.IsDeleted == true);
                var map = _mapper.Map<List<GetDepartmentVM>>(result);
                return new Response<List<GetDepartmentVM>>(map, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetDepartmentVM>>(null, ex.Message, true);
            }
        }
    }
}
