using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using eConFaire.RevitBuilder.Intro.Model;
using eConFaire.RevitBuilder.Intro.Utils;
using System.Diagnostics;
using System.Linq;

namespace eConFaire.RevitBuilder.Intro.UI
{
    [Transaction(TransactionMode.Manual)]
    public class CreateFinish : IExternalCommand
    {
        public static string finisajInterior = "INT_F_PLA_LAV-MDF plinta";
        public static string finisajExterior = "EXT_INSexp-5cm";
        public decimal inputX;
        public decimal inputY;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            Room newRoom = new Room();


            using (Transaction tr = new Transaction(document, "Creare finisaje"))
            {
                tr.Start();
                double wallWidth = (double)(Helpers.GetWallTypeByFamilyType(document, "INT_G_GK3 2xRB+ISO+CW/UW 75/600+2xRB_125 EI60")?.Width);
                double convertedValueExt = UnitConverter.FeetToMm(wallWidth + 0.16) / 2;
                double convertedValueInt = UnitConverter.FeetToMm(wallWidth + 0.028) / 2;
                string LevelName = "Parter";
                double wallHeight = UnitConverter.MmToFeet(4000);

                for (int i = 0; i < CreateWalls.roomsNumber; i++)
                {
                    inputX = App.rooms[i].LengthX;
                    inputY = App.rooms[i].LengthY;

                    //finish interior
                    var firstIntFinish = CreateFinishh(document,
                     (double)inputX,
                     convertedValueInt,
                     0,
                     convertedValueInt,
                     finisajInterior, LevelName, wallHeight, i);

                 //   App.joinPair.ToList().ElementAt(0).FinishesId.Add(firstIntFinish.UniqueId);

                    firstIntFinish.Flip();
                    newRoom.finishesInterior.Add(firstIntFinish);

                    var secondIntFinish = CreateFinishh(document,
                        convertedValueInt,
                        0,
                        convertedValueInt,
                        (double)inputY,
                        finisajInterior, LevelName, wallHeight, i);


                 //   App.joinPair.ToList().ElementAt(1).FinishesId.Add(secondIntFinish.UniqueId);
                    secondIntFinish.Flip();
                    newRoom.finishesInterior.Add(secondIntFinish);

                    var thirdIntFinish = CreateFinishh(document,
                        convertedValueInt,
                        (double)inputY - convertedValueInt,
                        (double)inputX,
                        (double)inputY - convertedValueInt,
                        finisajInterior, LevelName, wallHeight, i);

                 //   App.joinPair.ToList().ElementAt(2).FinishesId.Add(thirdIntFinish.UniqueId);
                    thirdIntFinish.Flip();
                    newRoom.finishesInterior.Add(thirdIntFinish);

                    var fourthIntFinish = CreateFinishh(document,
                        (double)inputX - convertedValueInt,
                        (double)inputY,
                        (double)inputX - convertedValueInt,
                        convertedValueInt, 
                        finisajInterior, LevelName, wallHeight, i);

                 //   App.joinPair.ToList().ElementAt(3).FinishesId.Add(fourthIntFinish.UniqueId);
                    fourthIntFinish.Flip();
                    newRoom.finishesInterior.Add(fourthIntFinish);
                    App.rooms[i].finishesInterior = newRoom.finishesInterior;
                }

                for (int i = 0; i < CreateWalls.roomsNumber; i++)
                {
                    inputX = App.rooms[i].LengthX;
                    inputY = App.rooms[i].LengthY;

                    //finish exterior
                    var firstExtFinish = CreateFinishh(document,
                        convertedValueExt + (double)inputX,
                        -convertedValueExt,
                        -convertedValueExt,
                        -convertedValueExt,
                        finisajExterior, LevelName, wallHeight, i);
                 //   App.joinPair.ToList().ElementAt(0).FinishesId.Add(firstExtFinish.UniqueId);
                    newRoom.finishesExterior.Add(firstExtFinish);

                    if (i == 0)
                    {
                        var secondExtFinish = CreateFinishh(document,
                        -convertedValueExt,
                        -convertedValueExt,
                        -convertedValueExt,
                        (double)inputY + convertedValueExt,
                        finisajExterior, LevelName, wallHeight, i);
                  //      App.joinPair.ToList().ElementAt(1).FinishesId.Add(secondExtFinish.UniqueId);
                        newRoom.finishesExterior.Add(secondExtFinish);

                    }

                    var thirdExtFinish = CreateFinishh(document,
                    -convertedValueExt,
                    (double)inputY + convertedValueExt,
                    (double)inputX + convertedValueExt,
                    (double)inputY + convertedValueExt,
                    finisajExterior, LevelName, wallHeight, i);

                   // App.joinPair.ToList().ElementAt(2).FinishesId.Add(thirdExtFinish.UniqueId);
                    newRoom.finishesExterior.Add(thirdExtFinish);

                    if (i == CreateWalls.roomsNumber - 1)
                    {

                        var fourthExtFinish = CreateFinishh(document,
                        (double)inputX + convertedValueExt,
                        (double)inputY + convertedValueExt,
                        (double)inputX + convertedValueExt,
                        -convertedValueExt,
                        finisajExterior, LevelName, wallHeight, i);

                       // App.joinPair.ToList().ElementAt(3).FinishesId.Add(fourthExtFinish.UniqueId);
                        newRoom.finishesExterior.Add(fourthExtFinish);
                        // var lastRoomKey = App.rooms.Keys.Max();
                        App.rooms[i].finishesExterior = newRoom.finishesExterior;
                    }

                }

                tr.Commit();
            }
            return Result.Succeeded;
        }



        public Wall CreateFinishh(Document document,
            double startX,
            double startY,
            double stopX,
            double stopY,
            string walltype,
            string wallLevel,

            double wallHeight,
            int roomNumber,
            double startZ = 0,
            double stopZ = 4000
            )
        {
            var levelId = Helpers.GetLevelByName(document, wallLevel).Id;
            ElementId wallTypeId = Helpers.GetWallTypeByFamilyType(document, walltype)?.Id;
            XYZ start;
            XYZ stop;

            if (CreateWalls.roomsNumber != 1)
            {
                

                double wallWidth = (double)(Helpers.GetWallTypeByFamilyType(document, "INT_G_GK3 2xRB+ISO+CW/UW 75/600+2xRB_125 EI60")?.Width);
                start = new XYZ(
                              UnitConverter.MmToFeet(startX + (double)inputX * roomNumber + wallWidth / 2),
                              UnitConverter.MmToFeet(startY),
                              0);

                stop = new XYZ(
                   UnitConverter.MmToFeet(stopX + (double)inputX * roomNumber + wallWidth / 2),
                   UnitConverter.MmToFeet(stopY),
                   0);

            }
            else
            {
                start = new XYZ(
                 UnitConverter.MmToFeet(startX),
                 UnitConverter.MmToFeet(startY),
                 0);

                stop = new XYZ(
                   UnitConverter.MmToFeet(stopX),
                   UnitConverter.MmToFeet(stopY),
                   0);

            }


            Line line = Line.CreateBound(start, stop);

            Wall wall = Wall.Create(document,
                line,
                wallTypeId,
                levelId,
              wallHeight,
                0,
                false,
                true
                );

            return wall;
        }
    }
}

