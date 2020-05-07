using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer.Models
{
    public class UserNotesInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotesId { get; set; }

        [ForeignKey("UserInfo")]
        public int UserId { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public string Color { get; set; }

        public string Image { get; set; }

        [DefaultValue(false)]
        public bool Pin { get; set; }

        [DefaultValue(false)]
        public bool Archived { get; set; }

        [DefaultValue(false)]
        public bool Trash { get; set; }

        public DateTime? Reminder { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
