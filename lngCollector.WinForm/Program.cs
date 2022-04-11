using lngCollector.Services;
using lngCollector.Services.sqliteDb;
using System.Text;
using Unity;

namespace lngCollector.WinForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            IUnityContainer container = new UnityContainer();

            Form1 frm = new Form1();

            container
                .RegisterInstance<IMainView>(frm)
                .RegisterType<IEWordRepo, EWordRepo>()
                .RegisterType<IAppDataDbFactory, AppDataDbFactory>()
                .RegisterType<IDbConfig, DbConfigSQLite>();

            MainPresenter presenter = container.Resolve<MainPresenter>();

            Application.Run(frm);
        }

        class DbConfigSQLite : IDbConfig
        {
            string _path;
            public string path => _path;
            public DbConfigSQLite()
            {
                var fle = File.ReadAllText(@"lng-config-db.json", Encoding.UTF8);

                fle = fle.Replace("\\", "\\\\");
                //fle = fle.Replace("\\", "/");

                var cfg = Newtonsoft.Json.JsonConvert.DeserializeObject<_cfg>(fle);
                _path = cfg.path;

                Console.WriteLine(cfg.path);
            }
        }

        class _cfg
        {
            public string path { get; set; }
        }
    }
}