using System.Web.Mvc;

namespace Library.Frontend.Host
{
    public class VersionedViewEngine : RazorViewEngine
    {
        public VersionedViewEngine()
        {
            var librarySettings = DependencyResolver.Current.GetService<ILibrarySettings>();

            AreaViewLocationFormats = new[]
                                      {
                                          "~/Areas/{2}/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".cshtml", 
                                          "~/Areas/{2}/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".vbhtml",
                                          "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                                          "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                                          "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                          "~/Areas/{2}/Views/Shared/{0}.vbhtml"
                                      };

            AreaMasterLocationFormats = new[]
                                        {
                                            "~/Areas/{2}/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".cshtml", 
                                            "~/Areas/{2}/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".vbhtml",
                                            "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                                            "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                                            "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                            "~/Areas/{2}/Views/Shared/{0}.vbhtml"
                                        };

            AreaPartialViewLocationFormats = new[]
                                             {
                                                 "~/Areas/{2}/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".cshtml",
                                                 "~/Areas/{2}/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".vbhtml",
                                                 "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                                 "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                                                 "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                                 "~/Areas/{2}/Views/Shared/{0}.vbhtml"
                                             };

            ViewLocationFormats = new[]
                                  {
                                      "~/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".cshtml", 
                                      "~/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".vbhtml",
                                      "~/Views/{1}/{0}.cshtml", 
                                      "~/Views/{1}/{0}.vbhtml",
                                      "~/Views/Shared/{0}.cshtml", 
                                      "~/Views/Shared/{0}.vbhtml"
                                  };

            MasterLocationFormats = new[]
                                    {
                                        "~/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".cshtml", 
                                        "~/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".vbhtml",
                                        "~/Views/{1}/{0}.cshtml", 
                                        "~/Views/{1}/{0}.vbhtml",
                                        "~/Views/Shared/{0}.cshtml", 
                                        "~/Views/Shared/{0}.vbhtml"
                                    };

            PartialViewLocationFormats = new[]
                                         {
                                             "~/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".cshtml", 
                                             "~/Views/{1}/{0}_v" + librarySettings.VersionNumber + ".vbhtml",
                                             "~/Views/{1}/{0}.cshtml", 
                                             "~/Views/{1}/{0}.vbhtml",
                                             "~/Views/Shared/{0}.cshtml", 
                                             "~/Views/Shared/{0}.vbhtml"
                                         };
        }
    }
}