using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpRegisterWithImgApi.Data;
using EmpRegisterWithImgApi.Model;
using Microsoft.AspNetCore.Routing.Constraints;

namespace EmpRegisterWithImgApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpModel>>> GetempModels()
        {
            return await _context.empModels
                .Select(x => new EmpModel()
                {
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.EmployeeName,
                    EmployeeOccupation = x.EmployeeOccupation,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}",Request.Scheme,Request.Host,Request.PathBase,x.ImageName)
                })
                .ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpModel>> GetEmpModel(int id)
        {
            var empModel = await _context.empModels.FindAsync(id);

            if (empModel == null)
            {
                return NotFound();
            }

            return empModel;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutEmpModel(int Id, EmpModel empModel)
        {
            if (Id != empModel.EmployeeId)
            {
                return BadRequest();
            }

            if (empModel.ImageFile != null)
            {
                empModel.ImageName = await SaveImage(empModel.ImageFile); // Save image and update ImageName
            }

            _context.Entry(empModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpModelExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpModel>> PostEmpModel([FromForm]EmpModel empModel)
        {
            if (empModel.ImageFile == null)
            {
                return BadRequest("Image file cannot be null.");
            }

            empModel.ImageName = await SaveImage(empModel.ImageFile);
            _context.empModels.Add(empModel);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

       

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpModel(int id)
        {
            var empModel = await _context.empModels.FindAsync(id);
            if (empModel == null)
            {
                return NotFound();
            }

            _context.empModels.Remove(empModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpModelExists(int id)
        {
            return _context.empModels.Any(e => e.EmployeeId == id);
        }
       
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName+DateTime.Now.ToString("yymmssff")+Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}
