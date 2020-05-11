using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer.Models
{
    public class LabelInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelID { set; get; }

        [ForeignKey("UserInfo")]
        public int UserID { set; get; }

        [ForeignKey("UserNotesInfo")]
        public int NoteID { set; get; }

        [Required]
        public string LabelName { set; get; }

        public DateTime CreatedDate{ set; get; }

        public DateTime ModifiedDate { set; get; }

    }
}
