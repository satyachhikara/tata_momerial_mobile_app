using Microsoft.AspNetCore.DataProtection;
using tata_momerial.web.common;
using System.Globalization;

namespace tata_momerial.web.ui.Business
{
    public class CommonUtilityClass
    {
        private readonly IDataProtector _protector;

        public CommonUtilityClass(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector(GetType().FullName);
        }

        public string Encrypt(string plaintext)
        {
            return _protector.Protect(plaintext);
        }

        public string Decrypt(string encryptedText)
        {
            return _protector.Unprotect(encryptedText);
        }


        public static string GetRequestToken()
        {
            string token = GetRequestVerificataionToken();

            if (AppServicesHelper.ApplicationConfiguration.enabledcustomantiforgerytoken
                && AppServicesHelper.HttpContextAccessor != null
                && AppServicesHelper.HttpContextAccessor.HttpContext != null
                && AppServicesHelper.HttpContextAccessor.HttpContext.Session != null)
            {
                AppServicesHelper.HttpContextAccessor.HttpContext.Session.Set(token, token);
            }

            string result = "<input id=\"RequestVerificationToken\" name=\"RequestVerificationToken\" type=\"hidden\" value=\"" + token + "\" />";
            return result;
        }

        private static string GetRequestVerificataionToken()
        {
            Random objRandom = new Random();
            string token = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()
                    + DateTime.Now.Millisecond.ToString() + DateTime.Now.Ticks.ToString() + objRandom.Next(DateTime.Now.Millisecond).ToString();

            return SHA512(token);
        }

        public static string SHA512(string sPassword)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(sPassword);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

        public static string GetUniqueQuestionID()
        {
            return "q" + Guid.NewGuid().ToString();
        }

        public static string GetMonthName(int month)
        {
            return CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName
                (month);
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        public static List<int> GetFiveYears()
        {
            List<int> lstYears = new List<int>();
            int currentYear = 2025;
            while (currentYear <= System.DateTime.Now.Year + 2)
            {
                lstYears.Add(currentYear);
                currentYear++;
            }

            return lstYears;
        }
    }
}
