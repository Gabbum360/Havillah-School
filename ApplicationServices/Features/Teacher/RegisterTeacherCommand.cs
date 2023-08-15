using ApplicationServices.Common.Enums;
using ApplicationServices.Common.Model;
using ApplicationServices.DataStorage;
using ApplicationServices.Features.Teacher.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Features.Teacher
{
    public class RegisterTeacherCommand : IRequest <Result>
    {
        public AddTeacherDto AddTeacherDto { get; set; }
        /*public Guid Id { get; set; }
        public string? StaffNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public ClassCategory Class { get; set; }*/
    }
    public class RegisterTeacherCommandHandler : IRequestHandler<RegisterTeacherCommand, Result>
    {
        private readonly ILogger<RegisterTeacherCommandHandler> _logger;
        private readonly SMDatabaseContext _context;
        public RegisterTeacherCommandHandler(ILogger<RegisterTeacherCommandHandler> logger, SMDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checkIfExist = await _context.Teachers.FindAsync(request.AddTeacherDto.Id, cancellationToken);
                if (checkIfExist != null)
                {
                    _logger.LogError("data already exist!");
                }
                var teacherId = Guid.NewGuid();
                var result = Entities.Teacher.Factory.Build(teacherId, request.AddTeacherDto.StaffNo, request.AddTeacherDto.Firstname, request.AddTeacherDto.Lastname,
                    request.AddTeacherDto.Age, request.AddTeacherDto.Country).SetAge(request.AddTeacherDto.Age)
                    .SetCountry(request.AddTeacherDto.Country).SetFirstname(request.AddTeacherDto.Firstname).SetLastname(request.AddTeacherDto.Lastname)
                    .SetStaffNo(request.AddTeacherDto.StaffNo).Sex;
                await _context.AddAsync(result);
                var issaved = await _context.SaveChangesAsync();
                if (issaved > 0) return Result.Ok("data added and saved successfully"); return Result.Fail("unable to add or save data");
            }
            catch (Exception)
            {
                return Result.Fail("unable to add or save data");
            }
        }
    }
}
