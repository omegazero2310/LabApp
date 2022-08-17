namespace WebApiLab.Exts
{
    public static class IsDigit
    {
        public static bool IsDigitOnlyString(this string input)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
