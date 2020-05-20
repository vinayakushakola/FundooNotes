using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer.Models
{
    public class CollaboratorInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("UserInfo")]
        public int UserID { get; set; }

        [ForeignKey("UserNotesInfo")]
        public int NoteID { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; } 

        public DateTime ModifiedDate { get; set; }
    }
}
