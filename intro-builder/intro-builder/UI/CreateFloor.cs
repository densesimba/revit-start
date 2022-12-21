using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using eConFaire.RevitBuilder.Intro.Model;
using eConFaire.RevitBuilder.Intro.Utils;
using System.Collections.Generic;
using System.Linq;

namespace eConFaire.RevitBuilder.Intro.UI
{
    [Transaction(TransactionMode.Manual)]

    public class CreateFloor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            Room newRoom = new Room();

            string floorName = "INT_G_Gresie Antiderap. 1.2 cm + Hidro + sapa5.8 cm";
            string levelName = "Parter";

            using (Transaction tr = new Transaction(document, "Creare finisaje"))
            {
                tr.Start();
                double wallWidth = (double)(Helpers.GetWallTypeByFamilyType(document, "INT_G_GK3 2xRB+ISO+CW/UW 75/600+2xRB_125 EI60")?.Width);
                double convertedValueInt = UnitConverter.FeetToMm(wallWidth - 0.005) / 2;

                var floor = CreateFloord(document,
                      convertedValueInt,
                      (double)CreateWalls.inputX - convertedValueInt,
                      convertedValueInt,
                      convertedValueInt,
                      (double)CreateWalls.inputX - convertedValueInt,
                      convertedValueInt,
                      (double)CreateWalls.inputY - convertedValueInt,
                      (double)CreateWalls.inputY - convertedValueInt, floorName,levelName);

                var lastRoomKey = App.rooms.Keys.Max();

                newRoom.floors.Add(floor);
                App.rooms[lastRoomKey].floors = newRoom.floors;

                tr.Commit();
            }
            return Result.Succeeded;
        }
        public Floor CreateFloord(Document document, double stratX, double stopX, double startY, double stopY, double stratX1, double stopX1, double startY1, double stopY1, string floorName,string levelName)
        {
            Level level = Helpers.GetLevelByName(document, levelName);
            ElementId floorType = Helpers.GetFloorTypeByName(document, floorName).Id;
            // Build a floor profile for the floor creation
            XYZ first = new XYZ(UnitConverter.MmToFeet(stratX), UnitConverter.MmToFeet(startY), 0);
            XYZ second = new XYZ(UnitConverter.MmToFeet(stopX), UnitConverter.MmToFeet(stopY), 0);
            XYZ third = new XYZ(UnitConverter.MmToFeet(stratX1), UnitConverter.MmToFeet(startY1), 0);
            XYZ fourth = new XYZ(UnitConverter.MmToFeet(stopX1), UnitConverter.MmToFeet(stopY1), 0);

            CurveLoop profile = new CurveLoop();
            profile.Append(Line.CreateBound(first, second));
            profile.Append(Line.CreateBound(second, third));
            profile.Append(Line.CreateBound(third, fourth));
            profile.Append(Line.CreateBound(fourth, first));

            return Floor.Create(document, new List<CurveLoop> { profile }, floorType, level.Id, true, null, 0.0);
        }
    }
}

