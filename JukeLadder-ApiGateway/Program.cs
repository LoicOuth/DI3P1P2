using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json");

builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://jukeladder.sulin.ga", "http://jukeladder.sulin.ga")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("MyAllowedOrigins");

app.UseRouting();

app.UseOcelot();

app.Run();
