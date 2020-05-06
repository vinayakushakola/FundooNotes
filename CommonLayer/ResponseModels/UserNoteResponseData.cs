using System.ComponentModel;

namespace CommonLayer.ResponseModels
{
    public class UserNoteResponseData
    {
        public int NoteId { set; get; }

        public string Title { set; get; }

        public string Notes { set; get; }

        public string Color { set; get; }

        public string Image { set; get; }

        [DefaultValue(false)]
        public bool Pin { set; get; }

        [DefaultValue(false)]
        public bool Archived { set; get; }

        [DefaultValue(false)]
        public bool Trash { set; get; }
    }
}
