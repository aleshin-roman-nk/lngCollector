// See https://aka.ms/new-console-template for more information
using Autofac;
using lngCollector.Services;
using lngCollector.Tools;
using System.Text;

var builder = new ContainerBuilder();

builder.RegisterType<AppDataDbFactory>().As<IAppDataDbFactory>();
builder.RegisterType<DbConfigSQLiteWeb>().As<IDbConfig>();
builder.RegisterType<EWordRepo>().As<IEWordRepo>();

var container = builder.Build();

var repo = container.Resolve<IEWordRepo>();

//LoggerObj.Write(repo.Get(6));
LoggerObj.Write(repo.UpdateWeights());

Console.WriteLine("OK");
Console.ReadLine();

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

        Console.WriteLine(_path);
    }
}

class _cfg
{
    public string path { get; set; }
}