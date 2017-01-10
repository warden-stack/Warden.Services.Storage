using Warden.Common.Nancy;

namespace Warden.Services.Storage.Modules
{
    public abstract class ModuleBase : ApiModuleBase
    {
        protected ModuleBase(bool requireAuthentication = true) 
            : this(string.Empty, requireAuthentication) { }

        protected ModuleBase(string modulePath, bool requireAuthentication = true) 
            : base(modulePath) 
        { 
            if (requireAuthentication)
            {
                // this.RequiresAuthentication();
            }
        }
    }
}