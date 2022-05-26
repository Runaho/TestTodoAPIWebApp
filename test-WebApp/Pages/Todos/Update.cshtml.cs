using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;
using test_WebApp.Models;
using test_WebApp.Services;

namespace test_WebApp.Pages.Todos
{
    public class UpdateModel : PageModel
    {
        [BindProperty, Required]
        public Todo _Todo { get; set; }
        public TodoAPI TodoAPI { get; }

        public UpdateModel(IConfiguration configuration, TodoAPI todoAPI)
        {
            _Todo = new Todo();
            TodoAPI = todoAPI;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (id != 0)
            {
                var todo = await TodoAPI.GetTodoAsync(id);
                if (todo != null)
                {
                    _Todo = todo;
                    return Page();
                }
            }

            return Redirect("Add");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool UpdateStatus = await TodoAPI.UpdateTodoAsync(_Todo);

            if (UpdateStatus)
                return Redirect("/");

            return Page();
        }
    }
}
