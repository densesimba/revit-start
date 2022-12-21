using Autodesk.Revit.DB;

namespace eConFaire.RevitBuilder.Intro.Utils
{
    public static class UnitConverter
    {
        /// <summary>
        ///     Convert units from Millimeters to Feets.
        /// </summary>
        /// 
        /// <param name="value">
        ///     The value to be converted (in Millimeters).
        /// </param>
        /// 
        /// <returns>
        ///     The value converted to Feets.
        /// </returns>
        public static double MmToFeet(double value)
        {
            return UnitUtils.Convert(value, UnitTypeId.Millimeters, UnitTypeId.Feet);
        }

        /// <summary>
        ///     Convert units from Millimeters to Feets.
        /// </summary>
        /// 
        /// <param name="value">
        ///     The value to be converted (in Millimeters).
        /// </param>
        /// 
        /// <returns>
        ///     The value converted to Feets.
        /// </returns>
        public static double FeetToMm(double value)
        {
            return UnitUtils.Convert(value, UnitTypeId.Feet, UnitTypeId.Millimeters);
        }
    }
}

