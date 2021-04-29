using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmailSendingFunctionApp.Database
{
    [Table("Associate_Birthday_Wishes_Inputs")]
    public class Associate_Birthday_Wishes_Inputs
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        public int AssociateId { get; set; }
        public int BirthdayPersonId { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public string BirthdayMessage { get; set; }
    }
}
