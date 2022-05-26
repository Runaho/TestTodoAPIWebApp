using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;
using test_WebApp.Models;
using test_WebApp.Services;

namespace test_WebApp.Pages
{
    public class AddModel : PageModel
    {
        [BindProperty, Required]
        public Todo NewTodo { get; set; }
        public TodoAPI TodoAPI { get; }

        public AddModel(TodoAPI todoAPI)
        {
            NewTodo = new Todo();
            TodoAPI = todoAPI;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool insertStatus = await TodoAPI.InsertTodoAsync(NewTodo);

            if (insertStatus)
            {
                return Redirect("/");
            }

            return Page();
        }
    }
}
