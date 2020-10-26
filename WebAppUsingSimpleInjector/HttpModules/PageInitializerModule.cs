using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using WebAppUsingSimpleInjector.HttpModules;

[assembly: PreApplicationStartMethod(typeof(PageInitializerModule), "Initialize")]
namespace WebAppUsingSimpleInjector.HttpModules
{
    
    public class PageInitializerModule : IHttpModule
    {
        public static void Initialize()
        {
            DynamicModuleUtility.RegisterModule(typeof(PageInitializerModule));
        }

        public void Init(HttpApplication app)
        {
            app.PreRequestHandlerExecute += (sender, e) =>
            {
                var handler = app.Context.CurrentHandler;
                if (handler != null)
                {
                    var name = handler.GetType().Assembly.FullName;
                    if (!name.StartsWith("System.Web") && !name.StartsWith("Microsoft"))
                    {
                        Global.InitializeHandler(handler);
                    }
                }
            };
        }

        public void Dispose()
        {
        }
    }
}