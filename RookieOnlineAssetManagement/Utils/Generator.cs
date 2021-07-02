using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Utils
{
    public static class Generator
    {
        public static string GenerateUserName (string firstName, string lastName)
        {
            var result = new StringBuilder();
            var firstLetterListFirstName = firstName.Split(' ');

            if (firstLetterListFirstName.Length > 0)
            {
                result.Append(firstLetterListFirstName[firstLetterListFirstName.Length - 1].ToLower());
            }
            else
            {
                result.Append(firstLetterListFirstName[0].ToLower());
            }
            var firstLetterListLastName = lastName.Split(' ');
            foreach (var firstLetter in firstLetterListLastName)
            {
                result.Append(firstLetter.ToLower()[0]);
            }

            return result.ToString();
        }
        public static string GenerateAssetCode (string categoryId, int increment)
        {
            var result = new StringBuilder(categoryId);
            result.Append(increment.ToString("D6"));
            ;
            return result.ToString();
        }

        public static string GenerateCategoryId (string categoryName)
        {
            var result = new StringBuilder();
            var firstLetterList = categoryName.Split(' ');
            if (firstLetterList.Length == 1)
            {
                return firstLetterList[0].Substring(0, 2).ToUpper();
            }

            foreach (var firstLetter in firstLetterList)
            {
                result.Append(firstLetter.ToUpper()[0]);
            }
            return result.ToString();

        }

        public static string AppendIdToUserName (string userName, int incrementId)
        {
            var result = new StringBuilder(userName);
            result.Append(incrementId);
            return result.ToString();
        }
    }
}
