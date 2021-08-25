using FA.JustBlogAPI.Models.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlogAPI.Models.Common
{
    [Table("Categories", Schema = "common")]
    public class Category : BaseEntity
    {
        [StringLength(255, MinimumLength = 2, ErrorMessage = "The {0} must between {2} and {1} character.")]
        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }

        [StringLength(1024, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
