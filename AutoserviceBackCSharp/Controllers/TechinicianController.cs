using Microsoft.AspNetCore.Mvc;
using AutoserviceBackCSharp.Models;
using AutoserviceBackCSharp.Validation;

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
        [HttpGet("{id}")]
        public Technician GetTechnician(int id)
        {
            return _context.Technicians.SingleOrDefault(techi => techi.Id == id)!;
        }

        [HttpPost]
        public ActionResult PostTechnician(string? name, string? phone, string? specialization, DateTime? startWork, DateTime? startWorkInCompany)
        {
            
            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' });
            if ((name == null || name.Length == 0) || (phone == null || phone.Length == 0)||(specialization==null||specialization.Length==0))
            {
                return BadRequest(
                    new { message = "Имя или номер или специализация техника не могут быть пустыми" }
                );
            }

            

            if (!name.All(x => char.IsLetter(x)))
            {
                return BadRequest("Имя техника может содержать только буквы");
            }
            if(validator.IsValid(specialization)==false)
            {
                return BadRequest("Специализация техника не может содержать такие символы");
            }

            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(phone, "UA");
                if (!phoneNumberUtil.IsValidNumber(phoneNumber))
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Номер телефона должен быть корректным");
            }

            var newTechnician = new Technician() { Name = name, Phone = phone, Specialization=specialization, StartWork = DateOnly.FromDateTime((DateTime)startWork), StartWorkInCompany = DateOnly.FromDateTime((DateTime)startWorkInCompany) };
            _context.Technicians.Add(newTechnician);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostTechnician), new { newTechnician = newTechnician, message = "Техник успешно создан" });
        }

        [HttpPatch("{id}")]
        public ActionResult<Technician> UpdateTechnician(int id, string? name, string? phone, string? specialization, DateTime? startWork, DateTime? startWorkInCompany)
        {
            var updTechnician = _context.Technicians.SingleOrDefault(techi => techi.Id == id);

            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' });
            if ((name == null || name.Length == 0) || (phone == null || phone.Length == 0) || (specialization == null || specialization.Length == 0))
            {
                return BadRequest(
                    new { message = "Имя или номер или специализация техника не могут быть пустыми" }
                );
            }



            if (!name.All(x => char.IsLetter(x)))
            {
                return BadRequest("Имя техника может содержать только буквы");
            }
            if (validator.IsValid(specialization) == false)
            {
                return BadRequest("Специализация техника не может содержать такие символы");
            }

            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(phone, "UA");
                if (!phoneNumberUtil.IsValidNumber(phoneNumber))
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Номер телефона должен быть корректным");
            }
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
                return Ok(new { updTechnician = updTechnician, message = "Техник успешно обновлен" });
            }
            return NotFound(new { message = "Техник не найден" }); 
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTechnician(int id)
        {
            var technician = _context.Technicians.SingleOrDefault(techi => techi.Id == id);

            if (technician != null)
            {
                _context.Remove(technician);
                _context.SaveChanges();
                return Ok(new { message = "Техник успешно ликвидирован" });
            }

            return  NotFound(new { message = "Техник не найден" }); ;
        }
    }
}
