namespace Pluralize
{
	public static class PluralizeTask
	{
        public static string PluralizeRubles(int count)
        {
            // Напишите функцию склонения слова "рублей" в зависимости от предшествующего числительного count.
            if (count % 100 >= 5 && count % 100 <= 20)
                return  "рублей";
            else
            {
                if (count % 10 >= 2 && count % 10 <= 4)
                    return "рубля";
                else
                {
                    if ((count % 10 >= 5 && count % 10 <= 9) || count % 10 == 0)
                        return "рублей";
                    else return "рубль";
                }
            }
        }
	}
}