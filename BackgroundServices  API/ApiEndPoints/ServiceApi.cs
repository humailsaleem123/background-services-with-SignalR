using BackgroundServices.Interface;
using Microsoft.AspNetCore.SignalR;

namespace BackgroundServices.ApiEndPoints
{
    public static class ServiceApi
    {


        public static void MapProductEndpoints(this WebApplication app)
        {

            app.MapPost("/start-background-service", (RunService runService, IHubContext<MessageHub, IMessageHubClient> _messagehub) =>
            {
                // Set the messages array before starting the background service
                string[] messages = new string[] { "Message 1", "Message 2", "Message 3" };

                List<string> offers = new List<string>();
                offers.Add("20% Off on IPhone 12");
                offers.Add("15% Off on HP Pavillion");
                offers.Add("25% Off on Samsung Smart TV");
                _messagehub.Clients.All.SendOffersToUser(offers);
                //new TimeLoggerService(messages);
                runService.StartLogging(messages);


                return Results.Ok("Background Service Executing");
            });


        }



    }
}
