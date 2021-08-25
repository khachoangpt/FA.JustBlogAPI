using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FA.JustBlogAPI.WebMVC.ViewModels
{
    public class CategoryViewModel : BaseViewModel
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
    }
}