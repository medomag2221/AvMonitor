﻿using AvMonitor.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AvMonitor.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                string result = (await AgentManager.GetInstance().PostAsync("Task", task)).ToString();
                _logger.LogInformation(result);
            }
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskModel task)
        {
            Console.WriteLine($"{task.Id}");
            Console.WriteLine(await DeleteAsync(new Uri($"https://localhost:7284/Task/{task.Id}")));
            return Redirect("/");
        }

    }
}
