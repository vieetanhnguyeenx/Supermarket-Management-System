using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTOs
{
    public class CategoryDTOCreateRequest
    {
        [Required, StringLength(40)]
        public string CategoryName { get; set; } = null!;
        [Required, StringLength(100)]
        public string? Description { get; set; }
    }

    public class CategoryDTOResponse
    {
        public int CategoryID { get; set; }
        [Required, StringLength(40)]
        public string CategoryName { get; set; } = null!;
        [Required, StringLength(100)]
        public string? Description { get; set; }
        public bool Discontinued { get; set; }

    }
    public class CategoryDTOPUT
    {
        public int CategoryID { get; set; }
        [Required, StringLength(40)]
        public string CategoryName { get; set; } = null!;
        [Required, StringLength(100)]
        public string? Description { get; set; }
    }
}
