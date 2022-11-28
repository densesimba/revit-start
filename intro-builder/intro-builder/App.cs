#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using eConFaire.RevitBuilder.Intro.UI;
using System;
using System.Collections.Generic;

#endregion

namespace eConFaire.RevitBuilder.Intro
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            RevitBuilderUI.AddUiButtons(app);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
