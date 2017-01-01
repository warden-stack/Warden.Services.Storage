using Nancy;

namespace Warden.Services.Storage.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("", args => "Welcome to the Warden.Services.Storage API!");
        }
    }
}