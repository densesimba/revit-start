using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace eConFaire.RevitBuilder.Intro.Utils
{
    public static class Helpers
    {

        public static RoofType GetRoofByName(Document doc, string roofName)
        {
            var roofType = new FilteredElementCollector(doc)
                .OfClass(typeof(RoofType))
                .Cast<RoofType>()
                .FirstOrDefault(l => l.Name.Equals(roofName));

            return roofType;
        }
        public static Level GetLevelByName(Document doc, string levelName)
        {
            var level = new FilteredElementCollector(doc)
                .OfClass(typeof(Level))
                .Cast<Level>()
                .FirstOrDefault(l => l.Name.Equals(levelName));

            return level;
        }

        public static WallType GetWallTypeByFamilyType(Document doc, string wallFamilyType)
        {
            var type = new FilteredElementCollector(doc)
                .OfClass(typeof(WallType))
                .Cast<WallType>()
                .FirstOrDefault(w => w.Name == wallFamilyType);

            if (type == null)
            {
                TaskDialog.Show("Lipsă familie", $"Familia {wallFamilyType} nu există importată în document!");
            }

            return type;
        }

        public static FloorType GetFloorTypeByName(Document doc, string floorName)
        {
            var type = new FilteredElementCollector(doc)
                .OfClass(typeof(FloorType))
                .Cast<FloorType>()
                .FirstOrDefault(f => f.Name == floorName);

            if (type == null)
            {
                TaskDialog.Show("Lipsă familie", $"Familia {floorName} nu există importată în document!");
            }

            return type;
        }

        public static CeilingType GetCeilingTypeByName(Document doc, string CeilingName)
        {
            var type = new FilteredElementCollector(doc)
                .OfClass(typeof(CeilingType))
                .Cast<CeilingType>()
                .FirstOrDefault(f => f.Name == CeilingName);

            if (type == null)
            {
                TaskDialog.Show("Lipsă familie", $"Familia {CeilingName} nu există importată în document!");
            }

            return type;
        }
      public  static Level CreateLevel(Document document,string Name)
        {
            // The elevation to apply to the new level
            double elevation = UnitConverter.MmToFeet(4000);

            // Begin to create a level
            Level level = Level.Create(document, elevation);
            if (null == level)
            {
                throw new Exception("Create a new level failed.");
            }

            // Change the level name

            level.Name = Name;

            return level;
        }
    }
}

