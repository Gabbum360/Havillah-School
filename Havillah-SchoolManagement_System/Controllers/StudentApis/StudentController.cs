using ApplicationServices.Features.Student.Dto;
using ApplicationServices.Interfaces.StudentInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Havillah_SchoolManagement_System.Controllers.StudentApis
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/v1/api/controller")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudent _student; //Presentation Layer now depends on Application layer.
        public StudentController(ILogger<StudentController> logger, IStudent student)
        {
            _logger = logger;
            _student = student; 
        }

        [HttpPost("/v1/register-student",Name = "Student Registration")]
        public async Task<Unit> Post([FromBody] StudentDto request)
        {
            try
            {
                var result = await _student.Register(request.Firstname, request.Lastname, request.Sex, request.Age, request.StudentNo, request.Country);
                if (result == null)
                {
                    _logger.LogError("request invalid");
                }
                //return Ok();
            }
            catch (Exception e)
            {
                _logger.LogInformation(StatusCodes.Status500InternalServerError, message: "unable to complete", e);
            }
            return Unit.Value;
        }

        [HttpGet("/get-student/{id}")]
        public async Task<StudentDto> Get(Guid id)
        {
            try
            {
                var result = await _student.GetStudent(id);
                if (result == null)
                {
                    _logger.LogError("Does not exist!");
                }
                return result;
            }
            catch (Exception e)
            {
                _logger.LogInformation(StatusCodes.Status500InternalServerError, message: "unable to complete", e);
                //throw;
            }
            return new StudentDto();   
        }

        [HttpGet("/getAllStudents")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _student.GetStudents();
                if(result == null)
                {
                    _logger.LogError("error!");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(StatusCodes.Status500InternalServerError, message: "unable to complete", e);
                //throw;
            }
            return Ok();
        }

        [HttpDelete("/removeData/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _student.RemoveStudent(id);
                if (result == null)
                {
                    _logger.LogError("error!");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(StatusCodes.Status500InternalServerError, message: "unable to complete", e);
                //throw;
            }
            return Ok();
        }

        [HttpPatch("/editOnlyOneData/{studentNo}")]
        public async Task<IActionResult> Update(string studentNo)
        {
            try
            {
                var result = await _student.UpdateProfile(studentNo);
                if (result == null)
                {
                    _logger.LogError("error!");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(StatusCodes.Status500InternalServerError, message: "unable to complete", e);
                //throw;
            }
            return Ok();
        }
    }
}
