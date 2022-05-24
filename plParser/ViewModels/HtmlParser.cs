using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Drawing;
using HtmlAgilityPack;
using Avalonia.Media.Imaging;
using System.Linq;
using plParser.Models;

namespace plParser.ViewModels
{
    public class HtmlParser
    {
        
        private IList<Song> Songs {get;set;}
        private HtmlDocument HtmlDoc {get;set;}
        private string URL {get;set;}

        public HtmlParser(string url) {
            Songs=new List<Song>();
            URL=url;
            HtmlWeb web = new HtmlWeb();
            HtmlDoc = web.Load(URL);
        }
    

        public PlayList ParsePlayList() {
             byte[] imageData=null;
             System.Drawing.Bitmap image=null;
             Avalonia.Media.Imaging.Bitmap avatara=null;
             string playListUrl = HtmlDoc.DocumentNode.SelectSingleNode(@"//div[@class='playAllSons']/article/div/section/img[@class='img']").Attributes["src"].Value;
             string playListName = HtmlDoc.DocumentNode.SelectSingleNode("//div[@class='playAllSons']/nav/a[@class='current']").InnerHtml;
             
             using (WebClient webClient = new WebClient()){
	         imageData = webClient.DownloadData(playListUrl);
             }

             using (MemoryStream mem = new MemoryStream(imageData)) 
             {
                avatara=new Avalonia.Media.Imaging.Bitmap(mem); 
             } 
             var playList=new PlayList () { Name="Playlist: "+playListName,
             Avatar=image, Avatara=avatara
             };
             Console.WriteLine(avatara.Size+playListName);
             return playList;
        }

        public IEnumerable<Song> ParseSongs() {
          IEnumerable<HtmlNode> nodes=HtmlDoc.DocumentNode.SelectNodes(
          @"//div[@class='playAllSons']/article/ol/li");
          int count=(nodes as ICollection<HtmlNode>).Count;
          Console.WriteLine("BBB {0}", count);
          foreach (HtmlNode node in nodes) {
              string id = node.SelectSingleNode(".//div[@class='serialNum']").InnerHtml;
              string name = node.SelectSingleNode(".//a[@class='songName']").InnerHtml;
              string artist = node.SelectSingleNode(".//a[@class='artistName']").InnerHtml;
              string time = node.SelectSingleNode("./time").InnerHtml;
              Console.WriteLine(id+" "+name+" "+artist+" "+time);
              var song=new Song () { Id=Int32.Parse(id), Name=name, Artist=artist,
              Duration=TimeSpan.ParseExact(time,"mm\\:ss", null)};
              Songs.Add(song);
          } ////
            foreach (Song song in Songs) {
                 Console.WriteLine(song);
            }     
              return Songs; 
        }

    }
}