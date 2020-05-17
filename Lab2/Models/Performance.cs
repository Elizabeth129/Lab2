using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Lab2.Models
{
    public class Performance
    {
        public Performance()
        {
            PerformanceActorLinker = new HashSet<PerformanceActorLinker>();
        }

        public int Id { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
      
        public string Name { get; set; }
       
        public int TypeOfPerformanceId { get; set; }
    
        public int TheaterId { get; set; }
        [Display(Name = "Тип вистави")]
        public virtual TypeOfPerformanceCollection TypeOfPerformance { get; set; }
        [Display(Name = "Театр")]
        public virtual Theater Theater { get; set; }
        public virtual ICollection<PerformanceActorLinker> PerformanceActorLinker { get; set; }
    }
}
