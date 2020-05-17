using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer.Models
{
    public class CollaboratorInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecieverID { get; set; }

        [ForeignKey("UserInfo")]
        public int UserID { get; set; }

        [ForeignKey("UserNotesInfo")]
        public int NoteID { get; set; }
    }
}
