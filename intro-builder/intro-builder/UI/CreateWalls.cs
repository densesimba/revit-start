using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using eConFaire.RevitBuilder.Intro.Utils;
using System.Diagnostics;
using eConFaire.RevitBuilder.Intro.Model;
using System.Linq;

namespace eConFaire.RevitBuilder.Intro.UI
{
    [Transaction(TransactionMode.Manual)]
    public class CreateWalls : IExternalCommand
    {
        FormRoof formRoof = new FormRoof();
        FormWalls createWallForm = new FormWalls();
        RoomsForm roomsForm = new RoomsForm();
        public static decimal inputX = 0;
        public static decimal inputY = 0;
        public static string hostwallWindow;
        public static string hostwallDoor;
        public static bool Terasa;
        public int wallNumber;
        public static int roomsNumber;
        public static double LastWall;

        XYZ stop;
        XYZ start;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            Room newRoom = new Room();

            _ = createWallForm.ShowDialog();

            roomsNumber = (int)createWallForm.numRoomsNumber.Value;

            string levelNamee = "Parter";
            //var overhang = (double)formRoof.numOverhang.Value;
            double wallheight = UnitConverter.MmToFeet(4_000);
            //JoinPair firstJoinPair = new JoinPair
            //{
            //    WallId = firstWall.UniqueId
            //};
            // hostwallWindow = firstWall.UniqueId;
            using (Transaction tr = new Transaction(document, "Create wall"))
            {
                tr.Start();

                for (int i = 1; i < roomsNumber + 1; i++)
                {
                    inputX = App.rooms[i].LengthX;
                    inputY = App.rooms[i].LengthY;

                    var firstWall = CreateWall(document, (double)inputX, 0, 0, 0, levelNamee, wallheight, i);
                    App.rooms[i].walls.Add(firstWall);
                    JoinPairs(firstWall);


                    if (i == 0)
                    {
                        var secondWall = CreateWall(document, 0, 0, 0, (double)inputY, levelNamee, wallheight, i);
                        App.rooms[i].walls.Add(secondWall);

                        JoinPairs(secondWall);
                    }
                    else if (i > 0 && App.rooms[i - 1].LengthY < App.rooms[i].LengthY)
                    {
                        var wall = App.rooms[i - 1].walls.Last();
                        LocationCurve locationCurve = wall.Location as LocationCurve;
                        double lengthOfFirstwall = UnitConverter.FeetToMm(locationCurve.Curve.Length);

                        //TODO : verifica daca shouldAddWall trebuie sa fie true sau false

                        var secondWall = CreateWall(document, 0, 0, 0, (double)inputY - lengthOfFirstwall, levelNamee, wallheight, i, shouldAddWall: true, shouldAddOnYAxis: false);
                        App.rooms[i].walls.Add(secondWall);
                    }

                    var thirdWall = CreateWall(document, 0, (double)inputY, (double)inputX, (double)inputY, levelNamee, wallheight, i);
                    App.rooms[i].walls.Add(thirdWall);
                    JoinPairs(thirdWall);

                    var fourthdWall = CreateWall(document, (double)inputX, (double)inputY, (double)inputX, 0, levelNamee, wallheight, i);
                    App.rooms[i].walls.Add(fourthdWall);
                    JoinPairs(fourthdWall);


                }

                tr.Commit();
            }
            return Result.Succeeded;
        }
        private void Btn_createWall_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void JoinPairs(Wall wallName)
        {
            JoinPair firstJoinPair = new JoinPair
            {
                WallId = wallName.UniqueId
            };
            App.joinPair.Add(firstJoinPair);

        }

        public Wall CreateWall(Document document,
            double startX,
            double startY,
            double stopX,
            double stopY,
            string levelName,
            double wallHeight,
            int roomCounter,
            bool shouldAddWall = true,
            bool shouldAddOnYAxis = true,
            double startZ = 0,
            double stopZ = 4000)
        {
            var levelId = Helpers.GetLevelByName(document, levelName).Id;
            ElementId wallTypeId = Helpers.GetWallTypeByFamilyType(document, "INT_G_GK3 2xRB+ISO+CW/UW 75/600+2xRB_125 EI60")?.Id;
            double wallWidth = (double)(Helpers.GetWallTypeByFamilyType(document, "INT_G_GK3 2xRB+ISO+CW/UW 75/600+2xRB_125 EI60"))?.Width;


            if (roomCounter != 0)
            {
                if (shouldAddOnYAxis == false)
                {
                    start = new XYZ(
                        UnitConverter.MmToFeet(startX + ((double)inputX * roomCounter) + wallWidth / 2),
                        UnitConverter.MmToFeet(startY + (double)App.rooms[roomCounter - 1].LengthY),
                        0);

                    stop = new XYZ(
                       UnitConverter.MmToFeet(stopX + ((double)inputX * roomCounter) + wallWidth / 2),
                       UnitConverter.MmToFeet(stopY + (double)App.rooms[roomCounter - 1].LengthY),
                       0);
                }
                else
                {
                    //
                    start = new XYZ(
                  UnitConverter.MmToFeet(startX + ((double)App.rooms[roomCounter - 1].LengthX * roomCounter) + wallWidth / 2),
                  UnitConverter.MmToFeet(startY),
                  0);

                    stop = new XYZ(
                       UnitConverter.MmToFeet(stopX + ((double)App.rooms[roomCounter - 1].LengthX * roomCounter) + wallWidth / 2),
                       UnitConverter.MmToFeet(stopY),
                       0);
                }
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

            InputLine inputLine = new InputLine
            {
                StartX = (int)startX,
                StartY = (int)startY,
                StopX = (int)stopX,
                StopY = (int)stopY
            };

            Line line = Line.CreateBound(start, stop);



            if (shouldAddWall)
            {
                if (App.wallLines.Keys.Count == 0)
                {
                    wallNumber = 1;
                }
                else
                {
                    wallNumber = App.wallLines.Keys.Max() + 1;
                }
                App.wallLines.Add(wallNumber, inputLine);
            }


            Wall wall = Wall.Create(document,
                line,
                wallTypeId,
                levelId,
             wallHeight,
                0,
                false,
                true);

            return wall;
        }
    }
}

