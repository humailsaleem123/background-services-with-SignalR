using BackgroundServices;
using BackgroundServices.ApiEndPoints;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<RunService>();
builder.Services.AddSingleton<IHostedService, RunService>(
           serviceProvider => serviceProvider.GetService<RunService>());


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
        builder.SetIsOriginAllowed((hosts) => true);

    });
});



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.MapHub<MessageHub>("/start-background-service");

ServiceApi.MapProductEndpoints(app);

app.Run();
    