using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CommonLayer.ResponseModels
{
    public class NoteCollabResponseData
    {
        public int NoteId { set; get; }

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

        public DateTime? Reminder { set; get; }

        public List<LabelResponseData> Labels { set; get; }

        public List<CollaboratorResponseData> Collaborators { set; get; }
    }
}
