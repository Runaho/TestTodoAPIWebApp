using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace test_WebApp.Models
{
    [BindProperties(SupportsGet = true)]
    public class Todo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Must be named."), MinLength(6, ErrorMessage = "Min lenght must be 6.")]
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
