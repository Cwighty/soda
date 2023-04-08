using SodaShared.Mappers;
using StoreApp.Data;
using Stripe;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(Environment.GetEnvironmentVariable("VS_TUNNEL_URL"));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

StripeConfiguration.ApiKey = builder.Configuration["StripeSecretKey"] 
    ?? throw new Exception("Stripe Secret Key is missing, add it to your user secrets.");
var supabaseURL = builder.Configuration["SupabaseURL"];
var anonKey = builder.Configuration["SupabaseAnonKey"];
var serviceRoleKey = builder.Configuration["SupabaseServiceRoleKey"] 
    ?? throw new Exception("Service Role Key is missing, add it to your user secrets.");
var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true,
};
builder.Services.AddSingleton(new Supabase.Client(supabaseURL, serviceRoleKey, options));

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
