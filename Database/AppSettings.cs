using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmailSendingFunctionApp.Database
{
    [Table("AppSettings")]
    public class AppSettings
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }      
        public string Key { get; set; }    
        public string Value { get; set; }
    }
}
