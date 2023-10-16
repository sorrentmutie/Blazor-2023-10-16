using HelloCore;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<IWelcome, Welcome>();
//builder.Services.AddSingleton<IClock, RealClock>();

//builder.Services.AddTransient<IWelcome, Welcome>();
//builder.Services.AddTransient<IClock, RealClock>();

builder.Services.AddScoped<IWelcome, Welcome>();
builder.Services.AddScoped<IClock, RealClock>();


//builder.Services.AddSingleton<IWelcome, WelcomeBis>();



//var clock = new FakeClock();
//var welcome = new Welcome(clock);

var app = builder.Build();

//app.UseWelcomePage();
app.UseStaticFiles();

//app.MapGet("/", (IEnumerable<IWelcome> services) => services.FirstOrDefault()?.SayHello());
app.MapGet("/", (IWelcome service) => service.SayHello());

app.Use( async(context,next) =>
{
    app.Logger.LogInformation("Middleware 1");
    await next.Invoke();
});

app.Use(async (context, next) =>
{
    if(context.Request.Path.StartsWithSegments("/prova"))
    {
        app.Logger.LogInformation("Middleware 2 Prova");
        await context.Response.WriteAsync("Prova");
    }
    else
    {
        app.Logger.LogInformation("Middleware 2");
        await next.Invoke();
    }

    //app.Logger.LogInformation("Middleware 2");
    //await next.Invoke();
});

//app.Run( async context => {
//    app.Logger.LogCritical("Messaggio da Prove di delegati");
//    await context.Response.WriteAsync("Prove di delegati");
//});

app.Run();
