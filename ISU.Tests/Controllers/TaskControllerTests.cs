using System.Net.Http.Json;
using System.Net;
using Entities.Models;
using WebApi.IntegrationTests.Factories;
using Repository;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.IntegrationTests.Controllers
{
    public class TaskControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
 
        public TaskControllerTests(CustomWebApplicationFactory<Program> factory)
        {    
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetTasks_ReturnsAllTasks()
        {
            var response = await _client.GetAsync("/api/Task");

            response.EnsureSuccessStatusCode(); 
            var tasks = await response.Content.ReadFromJsonAsync<IEnumerable<TaskItem>>();
            Assert.NotNull(tasks);
            Assert.Single(tasks); 
        }

        [Fact]
        public async Task GetTaskById_ReturnsCorrectTask()
        {
            var response = await _client.GetAsync("/api/Task/1");

            response.EnsureSuccessStatusCode();
            var task = await response.Content.ReadFromJsonAsync<TaskItem>();
            Assert.NotNull(task);
            Assert.Equal(1, task!.TaskId);
        }

        [Fact]
        public async Task GetTaskById_ReturnsNotFoundForInvalidId()
        {
            var response = await _client.GetAsync("/api/Task/99");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
