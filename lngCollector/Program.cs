using lngCollector.Services;
using lngCollector.Services.sqliteDb;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddSingleton<IEWordRepo, EWordRepo>();
builder.Services.AddTransient<IEWordRepo, EWordRepo>();
builder.Services.AddTransient<IMatrixRepo, MatrixRepo>();


builder.Services.AddScoped<IAppDataDbFactory, AppDataDbFactory>();

builder.Services.AddSingleton<IDbConfig>(new DbConfigSQLiteWeb());

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = true;
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();

class DbConfigSQLiteWeb : IDbConfig
{
    string _path;

    public string path => _path;
    public DbConfigSQLiteWeb()
    {
        var fle = File.ReadAllText("lng-config-db.json", Encoding.UTF8);

        //fle = fle.Replace("\\", "\\\\");
        fle = fle.Replace("\\", "/");

        var cfg = Newtonsoft.Json.JsonConvert.DeserializeObject<_cfg>(fle);
        _path = cfg.path;

        Console.WriteLine(cfg.path);
    }
}

class _cfg
{
    public string path { get; set; }
}
