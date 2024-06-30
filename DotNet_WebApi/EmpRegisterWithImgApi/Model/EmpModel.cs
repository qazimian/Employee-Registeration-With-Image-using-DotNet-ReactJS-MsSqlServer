using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmpRegisterWithImgApi.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace EmpRegisterWithImgApi.Model
{
    public class EmpModel
    {
        [Key]
        public int EmployeeId { get; set; }
  
        public string? EmployeeName { get; set; }
       
        public string? EmployeeOccupation { get; set; }
     
        public string? ImageName { get; set; }
        
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        public string? ImageSrc { get; set; }
    }
}
