using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CommonLayer.RequestModels
{
    public class UserNoteRequest
    {
        public string Title { set; get; }

        public string Description { set; get; }

        public string Color { set; get; }

        public string Image { set; get; }

        [DefaultValue(false)]
        public bool Pin { set; get; }

        [DefaultValue(false)]
        public bool Archived { set; get; }

        [DefaultValue(false)]
        public bool Trash { set; get; }

        public DateTime? Reminder { get; set; }

        public List<NotesLabelRequest> Label { set; get; }

    }
}
