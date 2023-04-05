using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using StoreApp.Data;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

StripeConfiguration.ApiKey = "sk_test_51MtGBEAFXrVhUejTIZLmyGU6SgDXRyN8xGfhrEIBsahMyi3WkuQNAXWn4O1JcxrDKjjZFtBlyRizyUJ6p5PDqurH00J3ekVcYz";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();



app.UseRouting();
app.UseStaticFiles();
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
