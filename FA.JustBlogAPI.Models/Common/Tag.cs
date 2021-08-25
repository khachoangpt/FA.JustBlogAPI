using FA.JustBlogAPI.Models.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlogAPI.Models.Common
{
    [Table("Tags", Schema = "common")]
    public class Tag : BaseEntity
    {
        [StringLength(255, MinimumLength = 2, ErrorMessage = "The {0} must between {2} and {1} character.")]
        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Tag Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }

        [StringLength(1024, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        public int Count { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
