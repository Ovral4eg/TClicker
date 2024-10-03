using Assets.Clicker.Scripts.Utils.Enums;
using System.Drawing;

namespace Assets.Clicker.Scripts.Utils
{
    public static class StringHelper
    {
        public static string GetColorById(ColorId id)
        {
            string result = null;
            switch(id)
            {
                case ColorId.purple: result = "#750075"; break;
                case ColorId.red: result = "#750000"; break;

                case ColorId.black:
                default: result = "#000000" ; break;

            }
            return result;
        }
    }
}
