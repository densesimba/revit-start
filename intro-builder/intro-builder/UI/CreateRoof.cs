using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using eConFaire.RevitBuilder.Intro.Model;
using eConFaire.RevitBuilder.Intro.Utils;
using System;

namespace eConFaire.RevitBuilder.Intro.UI
{
    [Transaction(TransactionMode.Manual)]

    public class CreateRoof : IExternalCommand
    {
        FormRoof frmRoof = new FormRoof();
        FormWalls formWalls = new FormWalls();
        public static Transaction tr;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;

            frmRoof.ShowDialog();
            frmRoof.numTerasaInaltime.Visible = false;

            using (Transaction tr = new Transaction(document, "Creare sarpanta"))
            {
                tr.Start();
                Autodesk.Revit.ApplicationServices.Application application = document.Application;

                ElementId levelRoof = Helpers.GetLevelByName(document, "Roof")?.Id;
                ElementId levelTerasa = Helpers.GetLevelByName(document, "Terasa")?.Id;

                if (!(levelRoof is null))
                {
                    document.Delete(levelRoof);
                }
                if (!(levelTerasa is null))
                {
                    document.Delete(levelTerasa);
                }
                Helpers.CreateLevel(document, "Roof");

                CreateRoofs(document);

                tr.Commit();
            }

            return Result.Succeeded;
        }


        public FootPrintRoof CreateRoofs(Document document)
        {
            Level levelId = Helpers.GetLevelByName(document, "Roof");
            RoofType roofType = Helpers.GetRoofByName(document, "GRESIE ANTIDERAPANTA ACOPERIS");
            Autodesk.Revit.ApplicationServices.Application application = document.Application;

            CurveArray footprint = application.Create.NewCurveArray();

            int index = 1;


            foreach (var line in App.wallLines.Values)
            {
                double startX = 0;
                double stopX = 0;
                double startY = 0;
                double stopY = 0;

                if (index == 1)
                {
                    startX = UnitConverter.MmToFeet(line.StartX + (double)frmRoof.numOverhang.Value);
                    stopX = UnitConverter.MmToFeet(line.StopX - (double)frmRoof.numOverhang.Value);
                    startY = UnitConverter.MmToFeet(line.StartY - (double)frmRoof.numOverhang.Value);
                    stopY = UnitConverter.MmToFeet(line.StopY - (double)frmRoof.numOverhang.Value);
                }
                if (index == 2)
                {
                    startX = UnitConverter.MmToFeet(line.StartX - (double)frmRoof.numOverhang.Value);
                    stopX = UnitConverter.MmToFeet(line.StopX - (double)frmRoof.numOverhang.Value);
                    startY = UnitConverter.MmToFeet(line.StartY - (double)frmRoof.numOverhang.Value);
                    stopY = UnitConverter.MmToFeet(line.StopY + (double)frmRoof.numOverhang.Value);
                }
                if (index == 3)
                {
                    startX = UnitConverter.MmToFeet(line.StartX - (double)frmRoof.numOverhang.Value);
                    stopX = UnitConverter.MmToFeet(line.StopX + (double)frmRoof.numOverhang.Value);
                    startY = UnitConverter.MmToFeet(line.StartY + (double)frmRoof.numOverhang.Value);
                    stopY = UnitConverter.MmToFeet(line.StopY + (double)frmRoof.numOverhang.Value);
                }
                if (index == 4)
                {
                    startX = UnitConverter.MmToFeet(line.StartX + (double)frmRoof.numOverhang.Value);
                    stopX = UnitConverter.MmToFeet(line.StopX + (double)frmRoof.numOverhang.Value);
                    startY = UnitConverter.MmToFeet(line.StartY + (double)frmRoof.numOverhang.Value);
                    stopY = UnitConverter.MmToFeet(line.StopY - (double)frmRoof.numOverhang.Value);
                }
                index++;
                Line currentLine = Line.CreateBound(new XYZ(startX, startY, 0), new XYZ(stopX, stopY, 0));
                footprint.Append(currentLine);
            }

            ModelCurveArray footPrintToModelCurveMapping = new ModelCurveArray();
            FootPrintRoof footprintRoof = document.Create.NewFootPrintRoof(footprint,
                levelId,
                roofType,
                out footPrintToModelCurveMapping);

            ModelCurveArrayIterator iterator = footPrintToModelCurveMapping.ForwardIterator();

            iterator.Reset();

            while (iterator.MoveNext())
            {
                ModelCurve modelCurve = iterator.Current as ModelCurve;
                footprintRoof.set_DefinesSlope(modelCurve, true);
            }
 
            return footprintRoof;

        }
    }
}

