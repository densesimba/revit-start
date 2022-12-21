using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace eConFaire.RevitBuilder.Intro.Model
{
    public class Room
    {
        public int index { get; set; }
        public float LocationX { get; set; }
        public float LocationY { get; set; }
        public decimal LengthX { get; set; }
        public decimal LengthY { get; set; }
        public string Name { get; set; }
        public bool Terasa { get; set; }

        public List<Wall> walls = new List<Wall>();
        public List<Wall> finishesInterior = new List<Wall>();
        public List<Wall> finishesExterior = new List<Wall>();
        public List<Floor> floors = new List<Floor>();
        public List<Ceiling> ceilings = new List<Ceiling>();
        public List<FamilyInstance> doors = new List<FamilyInstance>();
        public List<FamilyInstance> windows = new List<FamilyInstance>();
        //Terasa
        public List<Wall> TerasaWallsInterior = new List<Wall>();
        public List<Wall> TerasaWallsExterior = new List<Wall>();
        public List<Wall> ExteriorWalls = new List<Wall>();

        public List<Wall> GetDefaultWalls()
        {


            return default;
        }
    }
}
