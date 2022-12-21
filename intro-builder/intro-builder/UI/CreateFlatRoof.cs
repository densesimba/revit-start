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
    public class CreateFlatRoof : IExternalCommand
    {
        FormWalls formWalls = new FormWalls();
        Room newRoom = new Room();

        CreateFloor floor = new CreateFloor();
        CreateWalls walls = new CreateWalls();
        CreateFinish finishes = new CreateFinish();
        CreateRoof createRoof = new CreateRoof();

        FormRoof terasaForm = new FormRoof();
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;

           terasaForm.ShowDialog();
           terasaForm.numOverhang.Visible = false;


            using (Transaction tr = new Transaction(document, "Creare terasa"))
            {
                tr.Start();

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

                Helpers.CreateLevel(document, "Terasa");

                BuildFlatRoof(document);

                tr.Commit();
            }
            return Result.Succeeded;
        }

        private void TerasaForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public bool BuildFlatRoof(Document document)
        {

            string floorName = "0 Terasa necirculabila";

            double wallWidth = (double)(Helpers.GetWallTypeByFamilyType(document, "INT_G_GK3 2xRB+ISO+CW/UW 75/600+2xRB_125 EI60")?.Width);
            double convertedValueInt = UnitConverter.FeetToMm(wallWidth) / 2;
            double convertedValueInterior = UnitConverter.FeetToMm(wallWidth + 0.16) / 2;

            //<350 nu se poate
            if (terasaForm.numTerasaInaltime.Value < 349)
            {
                TaskDialog.Show("da", " valoarea prea mica");
                terasaForm.numTerasaInaltime.Value = 350;
            }
            double WallHeight = UnitConverter.MmToFeet((double)terasaForm.numTerasaInaltime.Value);


            string levelNamee = "Terasa";

            //Creare Floor Terasa
            var floorTerasa = floor.CreateFloord(document,
                   convertedValueInt,
                   (double)CreateWalls.inputX - convertedValueInt,
                   convertedValueInt,
                   convertedValueInt,
                   (double)CreateWalls.inputX - convertedValueInt,
                   convertedValueInt,
                   (double)CreateWalls.inputY - convertedValueInt,
                   (double)CreateWalls.inputY - convertedValueInt, floorName, levelNamee);

            //Creare pereti Base
            //var firstWall = walls.CreateWall(document, (double)CreateWalls.inputX, 0, 0, 0, levelNamee, WallHeight,false);
            //var secondWall = walls.CreateWall(document, 0, 0, 0, (double)CreateWalls.inputY, levelNamee, WallHeight,false);
            //var thirdWall = walls.CreateWall(document, 0, (double)CreateWalls.inputY, (double)CreateWalls.inputX, (double)CreateWalls.inputY, levelNamee, WallHeight,false);
            //var fourthdWall = walls.CreateWall(document, (double)CreateWalls.inputX, (double)CreateWalls.inputY, (double)CreateWalls.inputX, 0, levelNamee, WallHeight,false);

            Parameter heightParam = floorTerasa.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
            double newHeightTerasa = UnitConverter.MmToFeet(320);
            heightParam.Set(newHeightTerasa);

            double floorThickness = floorTerasa.get_Parameter(BuiltInParameter.FLOOR_ATTR_THICKNESS_PARAM).AsDouble();
            double finisajInteriorHeight = WallHeight - floorThickness + UnitConverter.MmToFeet(6);

            // Creare Finish Interior
            ////var firstIntFinish = finishes.CreateFinishh(document,
            ////    (double)CreateWalls.inputX,
            ////    convertedValueInterior,
            ////    0,
            ////    convertedValueInterior,
            ////    CreateFinish.finisajExterior, levelNamee,
            ////    finisajInteriorHeight);
            ////firstIntFinish.Flip();
            ////App.joinPair.ToList().ElementAt(0).FinishIdTerasa.Add(firstIntFinish.UniqueId);
           

            ////var secondIntFinish = finishes.CreateFinishh(document,
            ////    convertedValueInterior,
            ////    0,
            ////    convertedValueInterior,
            ////    (double)CreateWalls.inputY,
            ////   CreateFinish.finisajExterior, levelNamee, finisajInteriorHeight);
            ////secondIntFinish.Flip();
            
            ////App.joinPair.ToList().ElementAt(1).FinishIdTerasa.Add(secondIntFinish.UniqueId);

            ////var thirdIntFinish = finishes.CreateFinishh(document,
            ////    convertedValueInterior,
            ////    (double)CreateWalls.inputY - convertedValueInterior,
            ////    (double)CreateWalls.inputX,
            ////    (double)CreateWalls.inputY - convertedValueInterior,
            ////   CreateFinish.finisajExterior, levelNamee, finisajInteriorHeight);
            ////thirdIntFinish.Flip();
            
            ////App.joinPair.ToList().ElementAt(2).FinishIdTerasa.Add(thirdIntFinish.UniqueId);

            ////var fourthIntFinish = finishes.CreateFinishh(document,
            ////   (double)CreateWalls.inputX - convertedValueInterior,
            ////   (double)CreateWalls.inputY,
            ////   (double)CreateWalls.inputX - convertedValueInterior,
            ////   convertedValueInterior, CreateFinish.finisajExterior, levelNamee, finisajInteriorHeight);
            ////fourthIntFinish.Flip();
            
          //  App.joinPair.ToList().ElementAt(3).FinishIdTerasa.Add(fourthIntFinish.UniqueId);

            ////Parameter firstH = firstIntFinish.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
            ////Parameter secondH = secondIntFinish.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
            ////Parameter thirdH = thirdIntFinish.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
            ////Parameter fourthH = fourthIntFinish.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
            ////firstH.Set(UnitConverter.MmToFeet(newHeightTerasa + UnitConverter.FeetToMm(floorThickness) - 10));
            ////secondH.Set(UnitConverter.MmToFeet(newHeightTerasa + UnitConverter.FeetToMm(floorThickness) - 10));
            ////thirdH.Set(UnitConverter.MmToFeet(newHeightTerasa + UnitConverter.FeetToMm(floorThickness) - 10));
            ////fourthH.Set(UnitConverter.MmToFeet(newHeightTerasa + UnitConverter.FeetToMm(floorThickness) - 10));

            ////// Creare Finish Exterior

            ////var firstExtFinish = finishes.CreateFinishh(document,
            ////    convertedValueInterior + (double)CreateWalls.inputX,
            ////    -convertedValueInterior,
            ////    -convertedValueInterior,
            ////    -convertedValueInterior,
            ////    CreateFinish.finisajExterior, levelNamee, WallHeight);
            
            ////App.joinPair.ToList().ElementAt(0).FinishIdTerasa.Add(firstExtFinish.UniqueId);

            ////var secondExtFinish = finishes.CreateFinishh(document,
            ////      -convertedValueInterior,
            ////      -convertedValueInterior,
            ////      -convertedValueInterior,
            ////      (double)CreateWalls.inputY + convertedValueInterior,
            ////     CreateFinish.finisajExterior, levelNamee, WallHeight);
            
            ////App.joinPair.ToList().ElementAt(1).FinishIdTerasa.Add(secondExtFinish.UniqueId);

            ////var thirdExtFinish = finishes.CreateFinishh(document,
            ////     -convertedValueInterior,
            ////     (double)CreateWalls.inputY + convertedValueInterior,
            ////     (double)CreateWalls.inputX + convertedValueInterior,
            ////     (double)CreateWalls.inputY + convertedValueInterior,
            ////   CreateFinish.finisajExterior, levelNamee, WallHeight);
            
            ////App.joinPair.ToList().ElementAt(2).FinishIdTerasa.Add(thirdExtFinish.UniqueId);

            ////var fourthExtFinish = finishes.CreateFinishh(document,
            ////    (double)CreateWalls.inputX + convertedValueInterior,
            ////    (double)CreateWalls.inputY + convertedValueInterior,
            ////    (double)CreateWalls.inputX + convertedValueInterior,
            ////    -convertedValueInterior,
            ////  CreateFinish.finisajExterior, levelNamee, WallHeight);
            
          //  App.joinPair.ToList().ElementAt(3).FinishIdTerasa.Add(fourthExtFinish.UniqueId);

            return true;
        }

        public Floor CreateFloorTerasa(Document document, double stratX, double stopX, double startY, double stopY, double stratX1, double stopX1, double startY1, double stopY1)
        {
            Level level = Helpers.GetLevelByName(document, "Parter");

            ElementId floorType = Helpers.GetFloorTypeByName(document, "0 Terasa necirculabila").Id;

            double height = UnitConverter.MmToFeet(4100);

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

            Floor floor = Floor.Create(document, new List<CurveLoop> { profile }, floorType, level.Id, true, null, 0.0);
            Parameter heightParam = floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
            // double wallWidth = UnitConverter.MmToFeet((double)(Helpers.GetWallTypeByFamilyType(document, "0 Terasa necirculabila")?.Width));
            heightParam.Set(height + UnitConverter.MmToFeet(220));
            return floor;
        }
    }
}

