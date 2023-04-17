using Microsoft.AspNetCore.Mvc;
using AutoserviceBackCSharp.Models;
using AutoserviceBackCSharp.Validation;

namespace AutoserviceBackCSharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TechnicianController : ControllerBase
    {
        private readonly ILogger<TechnicianController> _logger;
        private readonly PracticedbContext _context;


        public TechnicianController(ILogger<TechnicianController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Technician> GetTechnicians()
        {
            return _context.Technicians.ToArray();
        }
        [HttpGet("{id}")]
        public ActionResult<Technician> GetTechnician(int id)
        {
            var technician= _context.Technicians.SingleOrDefault(techi => techi.Id == id)!;
            if (technician == null)
            {
                return NotFound(new { message = "Техник не найден" });
            }
            return Ok(technician);
        }

        [HttpPost]
        public ActionResult PostTechnician(string name, string phone, string specialization, DateTime? startWork, DateTime? startWorkInCompany)
        {
            
            
            if(string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Имя техника не может быть пустым");
            }
            
            if(string.IsNullOrWhiteSpace(specialization))
            {
                return BadRequest("Специализация техника не может быть пустым");
            }


            if (name != null && !name.All(x => char.IsLetter(x)))
            {
                return BadRequest("Имя техника может содержать только буквы");
            }

            if (specialization != null && !specialization.All(x => char.IsLetter(x)))
            {
                return BadRequest("Специализация техника может содержать только буквы");
            }

            if (name != null && (name.Length > 32 || name.Length < 3))
            {
                return BadRequest("Имя техника не может быть такой длинны");
            }
            if (specialization != null && (specialization.Length > 32 || specialization.Length < 3))
            {
                return BadRequest("Специализация техника не может быть такой длинны");
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
                catch (Exception)
                {
                    return BadRequest("Номер телефона должен быть корректным");
                }

            var newTechnician = new Technician() { Name = name, Phone = phone, Specialization= specialization };
            if (startWork.HasValue)
            {
                newTechnician.StartWork = DateOnly.FromDateTime((DateTime)startWork);
            }
            if (startWorkInCompany.HasValue)
            {
                newTechnician.StartWorkInCompany = DateOnly.FromDateTime((DateTime)startWorkInCompany);
            }
            _context.Technicians.Add(newTechnician);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostTechnician), new { newTechnician = newTechnician, message = "Техник успешно создан" });
        }

        [HttpPatch("{id}")]
        public ActionResult<Technician> UpdateTechnician(int id, string? name, string? phone, string? specialization, DateTime? startWork, DateTime? startWorkInCompany)
        {
            var updTechnician = _context.Technicians.SingleOrDefault(techi => techi.Id == id);

            if (name != null && !name.All(x => char.IsLetter(x)))
            {
                return BadRequest("Имя техника может содержать только буквы");
            }

            if (specialization != null && !specialization.All(x => char.IsLetter(x)))
            {
                return BadRequest("Специализация техника может содержать только буквы");
            }

            if (name!=null&&(name.Length > 32 ||  name.Length < 3))
            {
                return BadRequest("Имя техника не может быть такой длинны");
            }
            if (specialization != null && (specialization.Length > 32 || specialization.Length < 3))
            {
                return BadRequest("Специализация техника не может быть такой длинны");
            }

            if (phone != null)
            {
                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                try
                {
                    var phoneNumber = phoneNumberUtil.Parse(phone, "UA");
                    if (!phoneNumberUtil.IsValidNumber(phoneNumber))
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    return BadRequest("Номер телефона должен быть корректным");
                }
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

            return  NotFound(new { message = "Техник не найден" }); 
        }
    }
}
