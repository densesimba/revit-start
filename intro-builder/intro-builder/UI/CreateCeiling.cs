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
    class CreateCeiling : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            Room newRoom = new Room();

            using (Transaction tr = new Transaction(document, "Creare tavan"))
            {
                tr.Start();

                double wallWidth = (double)(Helpers.GetWallTypeByFamilyType(document, "INT_G_GK3 2xRB+ISO+CW/UW 75/600+2xRB_125 EI60")?.Width);
                double convertedValueInt = UnitConverter.FeetToMm(wallWidth + 0.05) / 2;

                var ceiling = CreateCeilings(document,
                      convertedValueInt, (double)CreateWalls.inputX - convertedValueInt,
                      convertedValueInt, convertedValueInt,
                      (double)CreateWalls.inputX - convertedValueInt, convertedValueInt,
                      (double)CreateWalls.inputY - convertedValueInt, (double)CreateWalls.inputY - convertedValueInt);

                var lastRoomKey = App.rooms.Keys.Max();
                newRoom.ceilings.Add(ceiling);
                App.rooms[lastRoomKey].ceilings = newRoom.ceilings;

                tr.Commit();
            }
            return Result.Succeeded;
        }
        public Ceiling CreateCeilings(Document document, double stratX, double stopX, double startY, double stopY, double stratX1, double stopX1, double startY1, double stopY1)
        {
            Level level = Helpers.GetLevelByName(document, "Parter");
            ElementId cielingType = Helpers.GetCeilingTypeByName(document, "Plafon casetat 600x600").Id;

            double height = UnitConverter.MmToFeet(3850);

            XYZ first = new XYZ(UnitConverter.MmToFeet(stratX), UnitConverter.MmToFeet(startY), 0);
            XYZ second = new XYZ(UnitConverter.MmToFeet(stopX), UnitConverter.MmToFeet(stopY), 0);
            XYZ third = new XYZ(UnitConverter.MmToFeet(stratX1), UnitConverter.MmToFeet(startY1), 0);
            XYZ fourth = new XYZ(UnitConverter.MmToFeet(stopX1), UnitConverter.MmToFeet(stopY1), 0);

            CurveLoop profile = new CurveLoop();
            profile.Append(Line.CreateBound(first, second));
            profile.Append(Line.CreateBound(second, third));
            profile.Append(Line.CreateBound(third, fourth));
            profile.Append(Line.CreateBound(fourth, first));

            Ceiling ceiling = Ceiling.Create(document, new List<CurveLoop> { profile }, cielingType, level.Id);

            Parameter heightParam = ceiling.get_Parameter(BuiltInParameter.CEILING_HEIGHTABOVELEVEL_PARAM);
            heightParam.Set(height);

            return ceiling;
        }
    }
}
