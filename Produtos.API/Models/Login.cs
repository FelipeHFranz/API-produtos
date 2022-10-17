using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Produtos.API.Models
{
    public partial class Login
    {
        [Required]
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [Column("senha")]
        [StringLength(50)]
        public string Senha { get; set; }
    }
    public partial class Senha
    {
        [Required]
        [Column("senha")]
        [StringLength(50)]
        public string SenhaAtual { get; set; }

        [Required]
        [Column("senha")]
        [StringLength(50)]
        public string SenhaNova { get; set; }
    }
}
