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
        [Column("AssociateID")]
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
        [Column("Password")]
        [Required]
        public string Password { get; set; }
        [Column("AssociateEmailId")]
        [Required]
        [StringLength(500)]
        public string Email { get; set; }
        [Column("Designation")]
        [Required]
        [StringLength(100)]
        public string Designation { get; set; }
        [Column("BirthDate")]
        [Required]
        [StringLength(100)]
        public DateTimeOffset DOB { get; set; }
        [Column("ImageUrl")]
        [Required]
        public string ImageUrl { get; set; }
        [Column("Role")]
        [Required]
        [StringLength(100)]
        public string Role { get; set; }
    }
}
