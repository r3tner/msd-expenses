var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var userId = context.Request.Headers["userId"].ToString();

        if (!string.IsNullOrEmpty(userId))
        {
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            await WebSocketHandler.ConnectNewClient(socket, userId);
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Connection failed: Missing userId.");
        }
    }
    else
    {
        await next();
    }
});

app.Run();
