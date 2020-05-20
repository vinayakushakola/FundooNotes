using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommonLayer.RequestModels
{
    public class UserNoteRequest
    {
        public string Title { set; get; }

        public string Description { set; get; }

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Enter Valid Color Code")]
        public string Color { set; get; }

        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "Enter a Valid URL")]
        public string Image { set; get; }

        [DefaultValue(false)]
        public bool Pin { set; get; }

        [DefaultValue(false)]
        public bool Archived { set; get; }

        [DefaultValue(false)]
        public bool Trash { set; get; }

        public DateTime? Reminder { get; set; }

        public List<NotesLabelRequest> Label { set; get; }

        public List<CollaboratorRequest> Collaborators { get; set; }
    }
}
