﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Models
{
    public class Director
    {
        public Director()
        {
            Theater = new HashSet<Theater>();
        }

        public int Id { get; set; }

        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
     
        public string Name { get; set; }
        [Display(Name = "Прізвище")]
      
        public string Surname { get; set; }
        [Display(Name = "Дата народження")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Персональний номер")]
      
       // [Remote(action: "CheckNumber", controller: "Professors", ErrorMessage = "Персональний номер уже використовується")]
        public int PersonalNumber { get; set; }
        [Display(Name = "Театр")]

        public virtual ICollection<Theater> Theater { get; set; }
    }
}
