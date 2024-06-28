using TechnicalAxos_JuanCastrejonDiaz.Platforms.Android;
using Application = Android.App.Application;
using IAppInfo = TechnicalAxos_JuanCastrejonDiaz.Interfaces.IAppInfo;


[assembly: Dependency(typeof(AppInfoAndroid))]
namespace TechnicalAxos_JuanCastrejonDiaz.Platforms.Android
{
    public class AppInfoAndroid : IAppInfo
    {
        public string GetPackageName()
        {
            return Application.Context.PackageName;
        }
    }
}
