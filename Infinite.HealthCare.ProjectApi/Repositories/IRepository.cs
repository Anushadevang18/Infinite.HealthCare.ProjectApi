using Infinite.HealthCare.ProjectApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infinite.HealthCare.ProjectApi.Repositories
{
    public interface IGetRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> GetById(int id);

    }
    public interface IRepository<T> where T : class
    {
        Task Create(T obj);
        Task<T> Update(int id, T obj);
        Task<T> Delete(int id);
    }

    public interface IDoctorRepository
    {

        Task<IEnumerable<Specialization>> GetSpecializations();

    }


}
