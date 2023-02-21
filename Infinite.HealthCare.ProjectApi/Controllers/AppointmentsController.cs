using Infinite.HealthCare.ProjectApi.Models;
using Infinite.HealthCare.ProjectApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infinite.HealthCare.ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IRepository<Appointment> _repository;
        private readonly IGetRepository<Appointment> _getRepository;

        public AppointmentsController(IRepository<Appointment> repository, IGetRepository<Appointment> getRepository)
        {
            _repository = repository;
            _getRepository = getRepository;
        }
        [HttpGet("GetAllAppointments")]
        public IEnumerable<Appointment> GetAppointments()
        {
            return _getRepository.GetAll();
        }

        [HttpGet]
        [Route("GetAppointmentsById/{id}", Name = "GetAppointmentsById")]

        public async Task<ActionResult> GetAppointmentById(int id)
        {
            var appointment = await _getRepository.GetById(id);
            if (appointment != null)
            {
                return Ok(appointment);

            }
            return NotFound();

        }
        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(appointment);
            return CreatedAtRoute("GetAppointmentsById", new { id = appointment.Id }, appointment);
        }


        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, appointment);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Appointment not found");
        }

        [HttpDelete("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Appointment not found");
        }

    }
}
