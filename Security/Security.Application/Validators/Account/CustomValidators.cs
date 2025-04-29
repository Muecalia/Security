namespace Security.Application.Validators.Account
{
    public static class CustomValidators
    {
        public static bool IsValidDate(string inputDate)
        {
            return DateTime.TryParse(inputDate, out var outPutDate) && outPutDate.Date > DateTime.Now.Date;
        }
    }
}
