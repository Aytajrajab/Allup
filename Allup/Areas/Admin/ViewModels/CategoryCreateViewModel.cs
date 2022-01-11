using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Allup.Areas.Admin.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public bool IsMain { get; set; }
        public int ParentId { get; set; }
    }
}
