using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace TilleWPF.Domain
{
    public class NumberFloatValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //    Regex regex = new Regex("(^[0-9]+(.[0-9]{1,2}){0,1})$");
            //    bool algo = regex.IsMatch(value.ToString());
            bool algo = double.TryParse(value.ToString(), out double salida);
            return algo
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "Formato incorrecto. E.g. 123.32");

        }
    }
}