using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using DataLibrary;
using Newtonsoft.Json;
using Task = DataLibrary.Task;
using ATask = System.Threading.Tasks.Task;
using System.Net.Http.Headers;

namespace ToDoListUI.APIAccess.DAOs
{
    internal class TaskDao
    {
        private HttpClient client;

        public TaskDao(HttpClient client)
        { 
            this.client = client;
        }
        public async Task<Task> CreateTask(Task task, int id)
        {
            var taskJson = JsonConvert.SerializeObject(task);
            var buffer = System.Text.Encoding.UTF8.GetBytes(taskJson);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage resp = await client.PostAsync(URIs.CreateUserURI(), byteContent).ConfigureAwait(false);
            var respString = resp.Content.ReadAsStringAsync().Result;
            Task retTask = JsonConvert.DeserializeObject<Task>(respString);
           
            return retTask;
        }

        public async Task<bool> DeleteTask(int Id)
        {
            var resp = await client.DeleteAsync(URIs.DeleteTaskUri(Id)).ConfigureAwait(false);
            string respString = resp.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<bool>(respString);
        }

        public Task GetTaskById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Task>> GetTasksByUserId(int Id)
        {
            var resp = await client.GetAsync(URIs.TasksGetByUserId + Id).ConfigureAwait(false);
            var respString = resp.Content.ReadAsStringAsync().Result;

            List<Task> tasks = JsonConvert.DeserializeObject<List<Task>>(respString);

            return tasks;
        }

        public async Task<bool> UpdateTask(Task task)
        {
            string taskString = JsonConvert.SerializeObject(task);
            var content = new StringContent(taskString, Encoding.UTF8, "application/json");

            var resp = await client.PutAsync(URIs.TasksUpdate, content).ConfigureAwait(false);
            var respString = resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<bool>(respString.Result);
        }
    }
}
