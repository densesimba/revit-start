#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using eConFaire.RevitBuilder.Intro.Model;
using eConFaire.RevitBuilder.Intro.UI;
using eConFaire.RevitBuilder.Intro.Utils;
using System.Collections.Generic;
#endregion

namespace eConFaire.RevitBuilder.Intro
{
    class App : IExternalApplication
    {
        public static List<JoinPair> joinPair = new List<JoinPair>();
        public static Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        public static Dictionary<int,InputLine> wallLines = new Dictionary<int, InputLine>();
        public static List<Wall> exteriorOutlineWalls = new List<Wall>();


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
