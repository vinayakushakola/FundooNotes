﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer.Models
{
    public class NotesLabel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [ForeignKey("UserNotesInfo")]
        public int NotesId { set; get; }

        [ForeignKey("LabelInfo")]
        public int LabelId { set; get; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { set; get; }

        public DateTime ModifiedDate { set; get; }
    }
}