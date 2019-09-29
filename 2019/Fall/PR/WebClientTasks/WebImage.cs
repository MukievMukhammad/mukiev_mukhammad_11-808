using System;

public class WebImage
{
    // Скачивает html по url и парсит по тегам <img>, извлекая ссылку на источник изображения
    // Далее скачивает изображения последовательно
    public static void DowloadImagesFrom(string url)
    {
        string htmlCode;
        using (WebClient client = new WebClient())
        {
            htmlCode = client.DownloadString(url);
            var images = ImgParseHtml(htmlCode);
            var count = 0;
            foreach (var page in pages)
            {
            	client.DownloadFile(page, count + ".img");
                count++;
            }
        }
    }

    // Парсит html по тегу <img> и атрибуду src=" "
    private static List<string> ImgParseHtml(string htmlCode)
    {
        var images = new List<string>();
        bool img = false;
        for (int i = 0; i < htmlCode.Length - 5; i++)
        {
            if (!img && htmlCode.Substring(i, 5) == "<img ")
            {
                img = true;
                i += 4;
                continue;
            }
            if (img && htmlCode.Substring(i, 4) == "src=")
            {
                img = false;
                int index = i + 5;
                while (htmlCode[index] != '"')
                    index++;
                images.Add(htmlCode.Substring(i + 5, index - i - 5));
                i = index;
            }
        }
        return images;
    }
}
