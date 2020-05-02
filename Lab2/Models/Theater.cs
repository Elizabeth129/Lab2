using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Lab2.Models
{
    public class Theater
    {
        public Theater()
        {
            Performance = new HashSet<Performance>();
        }

        public int Id { get; set; }
        public int DirectorId { get; set; }
        [Display(Name = "Адресса")]
        public string Address { get; set; }
        [Display(Name = "Дата заснування")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [DataType(DataType.Date)]    
        public DateTime DateOfStartWork { get; set; }
        
        [Display(Name = "Директор")]

        public virtual Director Director { get; set; }
        public virtual ICollection<Performance> Performance { get; set; }
    }
}
