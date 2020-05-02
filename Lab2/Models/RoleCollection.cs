using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Models
{
    public class RoleCollection
    {
        public RoleCollection()
        {
            Actor = new HashSet<Actor>();
        }

        public int Id { get; set; }
        [Display(Name = "Назва ролі")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        public string RoleName { get; set; }

        public virtual ICollection<Actor> Actor { get; set; }
    }
}
