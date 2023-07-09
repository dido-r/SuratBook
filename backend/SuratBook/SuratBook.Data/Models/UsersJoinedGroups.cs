﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuratBook.Data.Models
{
    public class UsersJoinedGroups
    {
        [Required]
        [ForeignKey(nameof(SuratUser))]
        public Guid SuratUserId { get; set; }

        public SuratUser SuratUser { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Group))]
        public Guid GrouptId { get; set; }

        public Group Group { get; set; } = null!;
    }
}
