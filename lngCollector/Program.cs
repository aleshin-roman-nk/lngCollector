using lngCollector;
using lngCollector.Services;
using lngCollector.Services.sqliteDb;
using lngCollector.Services.UserAuth;
using System.Security.Claims;
using System.Text;

AppCookieSettings.Name = "lngCollectorAppCookie";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IDbConfig>(new DbConfigSQLiteWeb());
builder.Services.AddScoped<IAppDataDbFactory, AppDataDbFactory>();
builder.Services.AddTransient<IEWordRepo, EWordRepo>();
builder.Services.AddTransient<IMatrixRepo, MatrixRepo>();
builder.Services.AddTransient<IUserAuthRepo, UserAuthRepo>();
builder.Services.AddTransient<ICosmosRepo, CosmosRepo>();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(AppCookieSettings.Name)
    .AddCookie(AppCookieSettings.Name, options =>
    {
        options.Cookie.Name = AppCookieSettings.Name;
        options.LoginPath = new PathString("/users/Login");
    });

builder.Services.AddScoped<IUserInfo>(provider =>
{
    var context = provider.GetService<IHttpContextAccessor>();

    string suid = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (!string.IsNullOrEmpty(suid))
        return new UserInfo("", "", int.Parse(suid));
    else return new UserInfo("", "", -1);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

class DbConfigSQLiteWeb : IDbConfig
{
    string _path;
    string _path_db_file;

    public string path_db_file => _path_db_file;

    public string path => _path;

    public DbConfigSQLiteWeb()
    {
        var fle = File.ReadAllText("lng-config-db.json", Encoding.UTF8);

        //fle = fle.Replace("\\", "\\\\");
        fle = fle.Replace("\\", "/");

        var cfg = Newtonsoft.Json.JsonConvert.DeserializeObject<_cfg>(fle);
        _path = cfg.path;
        _path_db_file = cfg.path_db_file;

        Console.WriteLine(cfg.path);
    }
}

class _cfg
{
    public string path { get; set; }
    public string path_db_file { get; set; }
}
