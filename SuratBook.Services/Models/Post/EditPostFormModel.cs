﻿namespace SuratBook.Services.Models.Post
{
    using System.ComponentModel.DataAnnotations;

    using static SuratBook.Data.Constants.Constants;
    using static SuratBook.Data.Constants.ErrorMessages;

    public class EditPostFormModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(PostMaxLength, MinimumLength = PostMinLength, ErrorMessage = PostErrorMessage)]
        public string Description { get; set; } = null!;
    }
}
