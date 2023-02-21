using Infinite.HealthCare.ProjectApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.ProjectApi.Repositories
{
    public class AppointmentRepository : IRepository<Appointment>, IGetRepository<Appointment>
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Appointment obj)
        {
             if (obj != null)
            {
            _context.Appointmnets.Add(obj);
            await _context.SaveChangesAsync();
            }
        }

        public async Task<Appointment> Delete(int id)
        {

            var appointmentinDb = await _context.Appointmnets.FindAsync(id);
            if (appointmentinDb != null)
            {
                _context.Appointmnets.Remove(appointmentinDb);
                await _context.SaveChangesAsync();
                return appointmentinDb;
            }
            return null;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointmnets.ToList();
        }

        public async Task<Appointment> GetById(int id)
        {
            var appointment = await _context.Appointmnets.FindAsync(id);
            if (appointment != null)
            {
                return appointment;
            }
            return null;
        }

        public async Task<Appointment> Update(int id, Appointment obj)
        {
            var appointmentinDb = await _context.Appointmnets.FindAsync(id);
            if (appointmentinDb != null)
            {
                appointmentinDb.PatientName = obj.PatientName;
                appointmentinDb.Gender = obj.Gender;
                appointmentinDb.Age = obj.Age;
                appointmentinDb.AppointmentDate = obj.AppointmentDate;
                appointmentinDb.DoctorName = obj.DoctorName;
                appointmentinDb.SpecializationOfDoctor = obj.SpecializationOfDoctor;
                // doctorinDb.Specialization = obj.Specialization;
                _context.Appointmnets.Update(appointmentinDb);
                await _context.SaveChangesAsync();
                return appointmentinDb;
            }
            return null;
        }
    }
}
       