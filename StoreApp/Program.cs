using SodaShared.Mappers;
using StoreApp.Data;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

StripeConfiguration.ApiKey = "sk_test_51MtGBEAFXrVhUejTIZLmyGU6SgDXRyN8xGfhrEIBsahMyi3WkuQNAXWn4O1JcxrDKjjZFtBlyRizyUJ6p5PDqurH00J3ekVcYz";
var supabaseURL = "https://dyafwhkcifxogstvfsuc.supabase.co";
var anonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImR5YWZ3aGtjaWZ4b2dzdHZmc3VjIiwicm9sZSI6ImFub24iLCJpYXQiOjE2ODA1Nzk2MjIsImV4cCI6MTk5NjE1NTYyMn0.IjDLttzrFOYyYRGqFodGHjtu6NbjpH7idZRLglovEzE";
builder.Services.AddSingleton(new Supabase.Client(supabaseURL, anonKey));

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
