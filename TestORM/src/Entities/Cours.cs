using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestORMCodeFirst.Entities
{
    [Table("COURS")]
    public class Cours
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string CodeCours { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        public string NomCours { get; set; }

        public virtual ICollection<InscriptionCours> Inscriptions { get; set; }
    }
}
