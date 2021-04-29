using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmailSendingFunctionApp.Database
{
    [Table("Associates")]
    public class Associates
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Column("AssociateFirstName")]
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Column("AssociateLastName")]
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Column("AssociateUserName")]
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Column("DOB")]
        [Required]
        [StringLength(100)]
        public DateTimeOffset DOB { get; set; }
        [Column("Role")]
        [Required]
        [StringLength(100)]
        public string Role { get; set; }
    }
}
