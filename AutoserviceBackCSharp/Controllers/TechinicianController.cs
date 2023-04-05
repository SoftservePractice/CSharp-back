using Microsoft.AspNetCore.Mvc;
using AutoserviceBackCSharp.Models;

namespace AutoserviceBackCSharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TechinicianController : ControllerBase
    {
        private readonly ILogger<TechinicianController> _logger;
        private readonly PracticedbContext _context;

        public TechinicianController(ILogger<TechinicianController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Technician> GetTechnicians()
        {
            return _context.Technicians;
        }
        [HttpGet("~/[controller]/{id}")]
        public Technician GetTechnician(int id)
        {
            return _context.Technicians.SingleOrDefault(techi => techi.Id == id)!;
        }

        [HttpPost]
        public Technician PostTechnician(string name, string phone, string specialization, DateTime startWork, DateTime startWorkInCompany)
        {
            var newTechnician = new Technician() { Name = name, Phone = phone, Specialization=specialization, StartWork = DateOnly.FromDateTime(startWork), StartWorkInCompany = DateOnly.FromDateTime(startWorkInCompany) };
            _context.Technicians.Add(newTechnician);
            _context.SaveChanges();
            return newTechnician;
        }

        [HttpPatch("~/[controller]/{id}")]
        public bool UpdateTechnician(int id, string? name, string? phone, string? specialization, DateTime? startWork, DateTime? startWorkInCompany)
        {
            var updTechnician = _context.Technicians.SingleOrDefault(techi => techi.Id == id);
            if (updTechnician != null)
            {
                updTechnician.Name = name ?? updTechnician.Name;
                updTechnician.Phone = phone ?? updTechnician.Phone;
                updTechnician.Specialization = specialization ?? updTechnician.Specialization;
                if (startWork.HasValue)
                {
                    updTechnician.StartWork = DateOnly.FromDateTime(startWork.Value);
                }
                if (startWorkInCompany.HasValue)
                {
                    updTechnician.StartWorkInCompany = DateOnly.FromDateTime(startWorkInCompany.Value);
                }
                
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpDelete("~/[controller]/{id}")]
        public bool DeleteTechnician(int id)
        {
            var technician = _context.Technicians.SingleOrDefault(techi => techi.Id == id);

            if (technician != null)
            {
                _context.Remove(technician);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
