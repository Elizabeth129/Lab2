using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Models
{
    public class TypeOfPerformanceCollection
    {
        public TypeOfPerformanceCollection()
        {
            Performance = new HashSet<Performance>();
        }
        public int Id { get; set; }
        [Display(Name = "Назва типу")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        public string TypeOfPerformanceName { get; set; }
        public virtual ICollection<Performance> Performance { get; set; }
    }
}
