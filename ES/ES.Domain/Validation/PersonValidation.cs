using System.Text.RegularExpressions;

namespace ES.Domain.Validation
{
    public static class PersonValidation
    {
        public static string ValidateMandatoryFields(this string field, string fieldName)
        {
            return string.IsNullOrWhiteSpace(field)? 
                throw new Exception($"The field {fieldName} cannot be null, empty or empty spaces. Please fill in with a valid string."):
                field;
        }

        public static string ValidatePassportNumber(this string passport)
        {
            if(passport.Length!=9)
                throw new Exception($"The passport number ({passport}) length must be 9 and not {passport.Length}.");

            var firstChar = passport.Substring(0,1).ToLower();
            if(firstChar != "p" && firstChar !="l")
                throw new Exception($"The passport first character  ({firstChar}) has to be p, P, L or l");

            var secondChar = passport.Substring(1,1);
            if( !Regex.IsMatch(secondChar, @"^[a-zA-Z]+$") )
                throw new Exception( $"The passport second character  ({secondChar}) needs to be a letter");
                
            var lastpart = passport.Substring(2);
            if (!Regex.IsMatch(lastpart, @"^[0-9]+$") )
                throw new Exception( $"The passport last 7 characters ({lastpart}) needs to be only numbers");
            
            return passport;
        }
    }
}