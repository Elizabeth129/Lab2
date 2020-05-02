using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class PerformanceActorLinker
    {
        public int Id { get; set; }
        [Display(Name = "Вистава")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        public int PerformanceId { get; set; }
        [Display(Name = "Актор")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        public int ActorId { get; set; }
        [Display(Name = "Вистава")]
        public virtual Performance Performance { get; set; }
        [Display(Name = "Актор")]
        public virtual Actor Actor { get; set; }
    }
}
