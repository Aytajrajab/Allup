using System.Collections.Generic;

namespace Allup.Models.Entity
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsMain { get; set; }
        public Category Parent { get; set; }
        public List<Category> Children { get; set; }
    }
}
