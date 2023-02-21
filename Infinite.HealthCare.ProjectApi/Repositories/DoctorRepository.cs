using Infinite.HealthCare.ProjectApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.ProjectApi.Repositories
{
    public class DoctorRepository : IRepository<Doctor>,  IGetRepository<DoctorDto>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Doctor obj)
        {
            if (obj != null)
            {
                _context.Doctors.Add(obj);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Doctor> Update(int id, Doctor obj)
        {
            var doctorinDb = await _context.Doctors.FindAsync(id);
            if (doctorinDb != null)
            {
                doctorinDb.DoctorName = obj.DoctorName;
                doctorinDb.Experience = obj.Experience;
                doctorinDb.Qualification = obj.Qualification;
                doctorinDb.SpecializationId = obj.SpecializationId;
                doctorinDb.Specialization = obj.Specialization;
                _context.Doctors.Update(doctorinDb);
                await _context.SaveChangesAsync();
                return doctorinDb;
            }
            return null;
        }

        public async Task<Doctor> Delete(int id)
        {
            var doctorinDb = await _context.Doctors.FindAsync(id);
            if (doctorinDb != null)
            {
                _context.Doctors.Remove(doctorinDb);
                await _context.SaveChangesAsync();
                return doctorinDb;
            }
            return null;
        }


        public IEnumerable<DoctorDto> GetAll()
        {
            var doctors = _context.Doctors.Include(m => m.Specialization).Select(x => new DoctorDto
            {
                Id = x.Id,
                DoctorName = x.DoctorName,
                EmailId= x.EmailId,
                Experience = x.Experience,
                Qualification = x.Qualification,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.Specialization.SpecializationName
            }).ToList();
            return doctors;
        }
    

        public async Task<DoctorDto> GetById(int id)
        {
            
            var doctors = await _context.Doctors.Include(m => m.Specialization).Select(x => new DoctorDto
            {
                Id = x.Id,
                DoctorName = x.DoctorName,
                EmailId = x.EmailId,
                Experience = x.Experience,
                Qualification = x.Qualification,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.Specialization.SpecializationName
            }).ToListAsync();
            var doctor = doctors.FirstOrDefault(x => x.Id == id);
            if (doctor != null)
            {
                return doctor;
            }
            return null;

        }
        public async Task<IEnumerable<Specialization>> GetSpecializations()
        {
            var specializations = await _context.Specializations.ToListAsync();
            return specializations;
        }

    }
}

        
    
    

