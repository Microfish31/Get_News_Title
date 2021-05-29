using HtmlAgilityPack;
using System;
using System.Net;

namespace Web_Scrapying
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //建立htmlweb
                HtmlWeb webClient = new HtmlWeb();
                //處理C# 連線 HTTPS 網站發生驗證失敗導致基礎連接已關閉
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                HtmlDocument doc = webClient.Load("https://www.chinatimes.com/newspapers/2601?chdtv"); //載入網址資料
                HtmlNodeCollection list = doc.DocumentNode.SelectNodes("/html/body/div[2]/div/div[2]/div/section/ul/li"); //抓取Xpath資料

                HtmlDocument udndoc = webClient.Load("https://udn.com/news/breaknews/1"); //載入網址資料
                HtmlNodeCollection udnlist = udndoc.DocumentNode.SelectNodes("//*[@id='breaknews']/div[1]/div"); //抓取Xpath資料

                Console.WriteLine("< 中國時報 >\n");
                for (int i = 0; i < list.Count ; i++)
                {
                    var get1 = list[i].SelectSingleNode("div/div/div[2]/div/time/span[1]");
                    var get2 = list[i].SelectSingleNode("div/div/div[2]/div/time/span[2]");
                    var get3 = list[i].SelectSingleNode("div/div/div[2]/h3");

                    if (get1 != null && get2 != null)
                    {
                        string time = get1.InnerText;
                        string date = get2.InnerText;
                        string title = get3.InnerText;
                        Console.WriteLine(title);
                        Console.WriteLine(time + " " + date + "\n");
                    }
                }

                Console.WriteLine("\n< udn news >\n");
                for (int i = 0; i < udnlist.Count ; i++)
                {
                    var get1 = udnlist[i].SelectSingleNode("div[2]/h2/a");
                    var get2 = udnlist[i].SelectSingleNode("div[2]/div/time");
                   
                    if (get1 != null)
                    {
                        string title = get1.InnerText;
                        string time = get2.InnerText;
                        Console.WriteLine(title + "\n" + time + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR=" + ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
