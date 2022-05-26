using Newtonsoft.Json;
using System.Configuration;
using System.Text;
using test_WebApp.Models;

namespace test_WebApp.Services
{
    public class TodoAPI
    {
        private string APIUrl { get; }

        public TodoAPI(IConfiguration config)
        {
            APIUrl = config.GetValue<string>("API");
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            using var httpClient = new HttpClient();

            using (var response = await httpClient.GetAsync($"{APIUrl}todoitems"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Todo>>(apiResponse);
            }
        }

        public async Task<Todo?> GetTodoAsync(int id)
        {
            using var httpClient = new HttpClient();
            using (var response = await httpClient.GetAsync($"{APIUrl}todoitems/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Todo>(apiResponse);
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> UpdateTodoAsync(Todo todo)
        {
            using var httpClient = new HttpClient();
            using (var response = await httpClient.PutAsync(
                requestUri: $"{APIUrl}todoitems/{todo.Id}",
                content: ModelToHttpJsonContent(todo)))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return (response.IsSuccessStatusCode);

            }
        }

        public async Task<bool> InsertTodoAsync(Todo todo)
        {
            using var httpClient = new HttpClient();
            using (var response = await httpClient.PostAsync($"{APIUrl}todoitems", ModelToHttpJsonContent(todo)))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// When you need the send some string content the web service
        /// This method can be easly change model to application type json and encoding UTF8 and also converting model to json.
        /// </summary>
        /// <param name="Model">Will be converted JSON</param>
        /// <returns>Http Content which content type json</returns>
        private HttpContent ModelToHttpJsonContent(object Model) => new StringContent(JsonConvert.SerializeObject(Model), Encoding.UTF8, mediaType: "application/json");
    }
}
