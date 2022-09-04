namespace TestBackEndApi.Helpers.Extension
{
    public static class CalculateOfAge
    {
        private static int CalculateOfAgeProvider(this DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }
        public static bool IsOfAge(this DateTime dateOfBirth)
        {
            var valor = CalculateOfAgeProvider(dateOfBirth);
            if (valor <= 17)
                return true;
            
            return false;
        }
    }
}
