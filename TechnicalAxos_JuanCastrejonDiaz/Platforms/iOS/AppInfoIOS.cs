using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAxos_JuanCastrejonDiaz.Platforms.iOS;
using IAppInfo = TechnicalAxos_JuanCastrejonDiaz.Interfaces.IAppInfo;

[assembly: Dependency(typeof(AppInfoIOS))]
namespace TechnicalAxos_JuanCastrejonDiaz.Platforms.iOS
{
    internal class AppInfoIOS: IAppInfo
    {
        public string GetPackageName()
        {
            return NSBundle.MainBundle.BundleIdentifier;
        }
    }
}
