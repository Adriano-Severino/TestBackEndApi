namespace TestBackEndApi.Helpers
{
    public static class CustomFormatAttribute
    {
        public static string RemoveCharacterString(string input)
        {
            return string.Concat(input.Where(char.IsNumber));
         
        }
    }
}
