using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ClassLibrary.Core
{
    static class Downloader
    {
        private string filePath = null;
        private string fileUrl = null;
        private string pageUrl;
        private HtmlDocument htmlDocument;
        private HtmlNodeCollection nodes;
        private WebClient webClient;

        //todo: 
        //изменить путь к файлу на диске
        //изменить тип скачиваемого файла
        //изменить способ поиска ссылок
        //добавить логирование
        void GetFromGks() //скачиваение .ttl файла со страницы по ссылке, для каждого ресурса подразумевается отдельный метод
        {
            filePath = "d:/file.ttl";
            pageUrl = "http://www.gks.ru/opendata/dataset/7708234640-bdboo2014";
            try
            {
                htmlDocument = new HtmlWeb().Load(pageUrl); //загрузка страницы
                nodes = htmlDocument.DocumentNode.SelectNodes("//a[@class='heading']"); //поиск ссылок с class='heading' на странице
                foreach (var node in nodes)
                    foreach (var atr in node.Attributes)
                        if (atr.Name == "href" && atr.Value.ToLower().Contains(".ttl")) //поиск ссылки на требуемый файл на странице
                            fileUrl = atr.Value;
                webClient.DownloadFile(fileUrl, filePath); //загрузка файла на диск по адресу filePath
            }
            catch (Exception) { }
        }
    }
}
