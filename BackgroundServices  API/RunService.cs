using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Http.HttpResults;
using BackgroundServices;

public class RunService : BackgroundService
{
    public static bool runService;
    private string[] messages { get; set; } // Array to store messages
    public string msg;
    private readonly ILogger<RunService> _logger;


    public RunService(ILogger<RunService> _logger)
    {
        this._logger = _logger;
    }



    public void StartLogging(string[] message)
    {
        this.messages = message;
        runService = true;

    }


    public void StopLogging()
    {
        runService = false;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            if (messages != null)
            {
                ManageUser();

            }
            await Task.Delay(1000, stoppingToken);
        }
    }

    public void ManageUser()
    {
        Console.WriteLine($"Nr. of users {messages}");
        foreach (var user in messages)
        {
            _logger.LogInformation($"USER YEA RHA {user}");

            Console.WriteLine($"{user}");
        }
    }



}



