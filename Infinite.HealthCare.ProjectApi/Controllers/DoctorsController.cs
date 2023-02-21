using Infinite.HealthCare.ProjectApi.Models;
using Infinite.HealthCare.ProjectApi.Repositories;
//using Infinite.HealthCare.ProjectApi.Models;
//uing Infinite.HealthCare.ProjectApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        private readonly IDoctorRepository _doctorRepository;

        private readonly IGetRepository<DoctorDto> _doctorDtoRepository;

        public DoctorsController(IRepository<Doctor> repository,  IGetRepository<DoctorDto> doctorDtoRepository, IDoctorRepository doctorRepository)
        {
            _repository = repository;
            _doctorDtoRepository = doctorDtoRepository;
            _doctorRepository = doctorRepository;
        }
        [HttpGet("GetAllDoctors")]
        public IEnumerable<DoctorDto> GetAllDoctors()
        {
            return _doctorDtoRepository.GetAll();
        }

        [HttpGet]
        [Route("GetDoctorsById/{id}", Name = "GetDoctorsById")]
        public async Task<ActionResult> GetDoctorById(int id)
        {
            var doctor = await _doctorDtoRepository.GetById(id);
            if (doctor != null)
            {
                return Ok(doctor);

            }
            return NotFound();

        }
       [Authorize(Roles ="Admin, Doctor")]
        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(doctor);
            return CreatedAtRoute("GetDoctorsById", new { id = doctor.Id }, doctor);
        }
        [Authorize(Roles ="Admin, Doctor")]
        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, doctor);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Doctor not found");
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Doctor not found");
        }

        //get Specialization

        [HttpGet("GetSpecializations")]

        public async Task<IActionResult> GetSpecializations()
        {
            var specializations = await _doctorRepository.GetSpecializations();
            return Ok(specializations);
        }


    }


}



