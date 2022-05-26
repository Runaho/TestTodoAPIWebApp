using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using test_WebApp.Models;
using test_WebApp.Services;

namespace test_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Todo> Todos { get; set; }
        private TodoAPI TodoAPI { get; }

        public IndexModel(TodoAPI todoAPI)
        {
            TodoAPI = todoAPI;
        }
        public async Task OnGetAsync()
        {
           Todos = await TodoAPI.GetTodosAsync();
        }
    }
}