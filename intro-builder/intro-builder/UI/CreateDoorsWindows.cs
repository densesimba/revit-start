using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using eConFaire.RevitBuilder.Intro.Model;
using eConFaire.RevitBuilder.Intro.Utils;
using System.Linq;

namespace eConFaire.RevitBuilder.Intro.UI
{
    [Transaction(TransactionMode.Manual)]
    public class CreateDoorsWindows : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            Room newRoom = new Room();

            using (Transaction tr = new Transaction(document, "Creare uși/ferestre"))
            {
                tr.Start();

                var wallIdHostWindow = document.GetElement(CreateWalls.hostwallWindow);
                var wallIdHostDoor = document.GetElement(CreateWalls.hostwallDoor);

                var window = CreateWindowsAndDoors(document, wallIdHostWindow, "1000X1400 FE01");
                var door = CreateWindowsAndDoors(document, wallIdHostDoor, "850 x 2100mm UL3");

                newRoom.doors.Add(door);
                newRoom.windows.Add(window);
                var lastRoomKey = App.rooms.Keys.Max();

                App.rooms[lastRoomKey].doors = newRoom.doors;
                App.rooms[lastRoomKey].windows = newRoom.windows;

                tr.Commit();

            }
            return Result.Succeeded;
        }
        public FamilyInstance CreateWindowsAndDoors(Document document, Element hostwall, string familyName)
        {
            FamilySymbol familySymbol = Extensions.GetFamilySymbolByType(document, familyName);
            Level level = Helpers.GetLevelByName(document, "Parter");
            var lengthX = UnitConverter.MmToFeet((double)CreateWalls.inputX / 2);
            var lengthY = UnitConverter.MmToFeet((double)CreateWalls.inputY / 2);
            double heightWindow = UnitConverter.MmToFeet(1000);
            XYZ location;

            if (familySymbol.Category.Name.ToString() == "Windows")
            {
                location = new XYZ(lengthY, lengthX, heightWindow);
            }
            else
            {
                location = new XYZ(lengthY, lengthX, 0);
            }
            if (!familySymbol.IsActive)
            {
                familySymbol.Activate();
                document.Regenerate();
            }
            FamilyInstance instance = document.Create.NewFamilyInstance(location,
                familySymbol,
                hostwall,
                level,
                StructuralType.NonStructural);

            return instance;
        }
    }
}

