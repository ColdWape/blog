using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class BloggerModel
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }

    }
    public class RegisterModel
    {
        [Required]
        public string Mail { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Pass { get; set; }

    }
    public class LoginModel
    {
        [Required]
        public string Mail { get; set; }

        [Required]
        public string Pass { get; set; }

    }
}
