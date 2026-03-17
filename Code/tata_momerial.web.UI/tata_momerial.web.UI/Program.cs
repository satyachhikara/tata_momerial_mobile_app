using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using tata_momerial.web.Common;
using tata_momerial.web.Common.Cookie;
using Serilog;
using Serilog.Events;
using System.Globalization;


IConfiguration Configuration;
    
var builder = WebApplication.CreateBuilder(args);

var cultureinfo = new CultureInfo("en-IN");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultureinfo);
    options.SupportedCultures = new List<CultureInfo> { cultureinfo };
    options.SupportedUICultures = new List<CultureInfo> { cultureinfo };
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.ConsentCookieValue = "true";
});

//Increase max body length
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue; // if don't set default value is: 128 MB
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appSettings.json",
    optional: true, reloadOnChange: true);

Configuration = configurationBuilder.Build();

builder.Services.AddLogging();

builder.Logging.AddSerilog();

Log.Logger = new LoggerConfiguration()
     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
     .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
     .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
    .WriteTo.File(Path.Combine("Serilogs\\", "Log-.txt"),
    rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100000,
    shared: true)
    .CreateLogger();
builder.Configuration.AddConfiguration(Configuration);


builder.Services.AddTransient<IMailService, MailService>();

// Register the IOptions object
builder.Services.Configure<ApplicationConfiguration>(Configuration.GetSection(nameof(ApplicationConfiguration)));

//Explicitly register the settings object by delegating to the IOptions object so that it can be accessed globally via AppServicesHelper.
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptionsMonitor<ApplicationConfiguration>>().CurrentValue);


// have to add this in order to access HttpContext from our own services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.Configure<MailSettings>(Configuration.GetSection(nameof(MailSettings)));

builder.Services.AddMemoryCache();

builder.Services.AddDataProtection();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddMvcCore();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.IOTimeout = TimeSpan.FromMinutes(20);

});

builder.Services.AddControllersWithViews(x => x.SuppressAsyncSuffixInActionNames = false).AddRazorRuntimeCompilation();


var app = builder.Build();



AppServicesHelper.ServiceProvider = builder.Services.BuildServiceProvider();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

}
app.UseHttpsRedirection();

app.UseRequestLocalization();

app.UseStaticFiles();
app.UseSession();
app.UseCors(c =>
{
    c.AllowAnyHeader().AllowAnyOrigin();
});

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Use(next => context =>
{
    context.Response.OnStarting(() =>
    {
        context.Response.Headers.Remove("Server");
        context.Response.Headers.Remove("X-Powered-By");
        context.Response.Headers.Remove("X-AspNet-Version");
        context.Response.Headers.Remove("X-AspNetMvc-Version");
        context.Response.Headers.Remove("X-XSS-Protection");
        context.Response.Headers.Remove("X-Frame-Options");
        context.Response.Headers.Remove("X-Content-Type-Options");
        context.Response.Headers.Append("X-XSS-Protection", "1;mode=block");
        context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        return Task.CompletedTask;
    });
    return next(context);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
