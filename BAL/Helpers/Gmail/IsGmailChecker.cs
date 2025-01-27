namespace BAL.Helpers.Gmail
{
    public static class IsGmailChecker
    {
        public static bool IsGmail(string gmail)
        {
            return gmail.EndsWith("@gmail.com");
        }
    }
}
