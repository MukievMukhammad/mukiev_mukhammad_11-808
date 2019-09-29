using System;

public class HtmlDownloader
{
    // Скачивает html по url и парсит по тегам <a></a>, 
    // извлекая ссылки на другие html страницы по атрибуту href=""
    // Далее скачивает эти страницы
    public static void DowloadHtlmPages(string url)
    {
        string htmlCode;
        using (WebClient client = new WebClient())
        {
            htmlCode = client.DownloadString(url);
            var pages = HrefParseHtml(htmlCode);
            pages.Add(url);
            var count = 0;
            foreach (var page in pages)
            {
                client.DownloadFile(page, count + ".html");
                count++;
            }
        }
    }

    // Парсит html по тегу <a></a> и атрибуду href=""
    private static List<string> HrefParseHtml(string htmlCode)
    {
        var pages = new List<string>();
        bool aTag = false;
        for (int i = 0; i < htmlCode.Length - 3; i++)
        {
            if (!aTag && htmlCode.Substring(i, 3) == "<a ")
            {
                aTag = true;
                i += 2;
                continue;
            }
            if (aTag && htmlCode.Substring(i, 5) == "href=")
            {
                aTag = false;
                int index = i + 6;
                while (htmlCode[index] != '"')
                    index++;
                pages.Add(htmlCode.Substring(i + 6, index - i - 6));
                i = index;
            }
        }
        return pages;
    }
}
