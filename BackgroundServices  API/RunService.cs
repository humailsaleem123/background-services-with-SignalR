using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Http.HttpResults;
using BackgroundServices;
using BackgroundServices.Interface;

public class RunService : BackgroundService
{
    public static bool runService;
    private string[] messages { get; set; } // Array to store messages
    private List<string> offers { get; set; }
    public string msg;
    private readonly ILogger<RunService> _logger;
    private IHubContext<MessageHub, IMessageHubClient> _messageHub;


    public RunService(ILogger<RunService> _logger, IHubContext<MessageHub, IMessageHubClient> _messageHub)
    {
        this._logger = _logger;
        this._messageHub = _messageHub;
    }



    public void StartLogging(List<string> offers)
    {
        this.offers = offers;
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
            if (offers != null)
            {
                ManageUser();

            }
            await Task.Delay(1000, stoppingToken);
        }
    }

    public void ManageUser()
    {
        Console.WriteLine($"Nr. of users {offers}");
        _messageHub.Clients.All.SendOffersToUser(offers);
        foreach (var offer in offers)
        {
            _logger.LogInformation($"USER YEA RHA {offer}");

        }
    }



}



