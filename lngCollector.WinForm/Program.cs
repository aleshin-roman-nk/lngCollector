using lngCollector.Services;
using lngCollector.Services.sqliteDb;
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
    }
}