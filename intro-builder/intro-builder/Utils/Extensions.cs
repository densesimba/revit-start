using Autodesk.Revit.DB;
using System;
using System.Linq;

namespace eConFaire.RevitBuilder.Intro.Utils
{
    public static class Extensions
    {
        public static FamilySymbol GetFamilySymbolByType(this Document document, string symbolType)
        {
            // LINQ to find the window's & door's FamilySymbol by its Type.
            try
            {
                FamilySymbol familySymbol = new FilteredElementCollector(document)
                    .OfClass(typeof(FamilySymbol))
                    .Cast<FamilySymbol>()
                    .Where(fs => fs.Name == symbolType)
                    .FirstOrDefault();

                return familySymbol;
            }
            catch (Exception ex)
            {
                // TaskDialog.Show("Lipsă familie", $"Familia {symbolName} nu există! Vă rugăm importați-o manual!");

                return null;
            }
        }

    }
}

