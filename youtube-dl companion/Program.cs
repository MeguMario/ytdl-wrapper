using System.IO;
using System.Net;
using System.Net.Http;
using System;
using Newtonsoft.Json.Linq;
using System.Text;

namespace youtube_dl_companion
{
    class Program
    {
        static void Main(string[] args)
        {
            // some ytdl command arg
            string bz = "bestvideo[ext=mp4]+bestaudio[ext=m4a]/best[ext=mp4]/best";
            string ba = "bestaudio[ext=m4a]";
            string ap = "%(playlist)s/%(playlist_index)s - %(title)s.%(ext)s";
            // case switch time
            Console.Write("Enter your selection, 4 to help, 5 to update youtube-dl & ffmpeg (1-5): ");
            sbyte caseSw = Convert.ToSByte(Console.ReadLine());
            switch (caseSw)
            {
                // only one video
                case 1:   
                    {
                        Console.WriteLine("Type Youtube Video URL");
                        string u = Console.ReadLine();
                        string ytdl = $"youtube-dl.exe -f {bz} {u}";
                        System.Diagnostics.Process.Start("CMD.exe", "/C" + ytdl);
                        Console.ReadKey();
                        break;
                    }   
                // audio only
                case 2:
                    {
                        Console.WriteLine("Type Youtube URL");
                        string u = Console.ReadLine();
                        string ytdl = $"youtube-dl.exe -f {ba} {u}";
                        System.Diagnostics.Process.Start("CMD.exe", "/C" + ytdl);
                        Console.ReadKey();
                        break;
                    }
                // download a whole playlist
                case 3:
                    {
                        Console.WriteLine("Type Youtube Playlist");
                        string u = Console.ReadLine();
                        string ytdl = $"youtube-dl.exe -f {ap} {u}";
                        System.Diagnostics.Process.Start("CMD.exe", "/C" + ytdl);
                        Console.ReadKey();
                        break;
                    }
                // the worst help section ever
                case 4:
                    {
                        string ytdlh = $"youtube-dl.exe --version";
                        string ffmpegv = $"ffmpeg -version";
                        Console.WriteLine("Option 1: Download the highest quality version of that video");
                        Console.WriteLine("Option 2: Download the highest audio quality version of that video (m4a format)");
                        Console.WriteLine("Option 3: Download the entire playlist");
                        Console.WriteLine("Option 4: This line of text");
                        Console.WriteLine("youtube-dl ref app version 00, reported youtube-dl version:");
                        System.Diagnostics.Process.Start("CMD.exe", "/C" + ytdlh);
                        break;
                    }
                // a poorly written updater, exploitable as hell
                case 5:
                    {
                        Console.WriteLine("Updating core component:");
                        string sauceffpeg = "https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-full.7z";
                        string sauceytdl = "https://www.github.com/ytdl-org/youtube-dl/releases/latest/download/youtube-dl.exe";
                        WebClient wc = new WebClient();
                        HttpClient client = new HttpClient();
                        string sauceffpegver = "https://www.gyan.dev/ffmpeg/builds/release-version";
                        // this line always returns 404
                        string sauceytdlver = "https://api.github.com/repos/ytdl-org/youtube-dl/releases/latest";
                        string json2 = wc.DownloadString(sauceffpegver);
                        // not usable
                        try
                        {
                            string json = client.GetStringAsync(sauceytdlver).GetAwaiter().GetResult();
                            string newytdlver = $"New version of ytdl: {json}";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        string newffmpegver = $"New version of ffmpeg: {json2}";
                        Console.WriteLine(newffmpegver);
                        wc.DownloadFile(sauceffpeg, "ffmpeg.7z");
                        wc.DownloadFile(sauceytdl, "youtube-dl.exe");
                        System.Diagnostics.Process.Start("cmd.exe", "/C" + "7z.exe" + " e " + " -y " + " ffmpeg.7z " + " ffmpeg.exe " + " -r ");
                        // this will always display before the extraction for some reason
                        Console.WriteLine("Finished updating the core component");
                        Console.ReadKey();
                        File.Delete("ffmpeg.7z");
                        break;
                    }
                //invaild value
                default:
                    {
                        Console.WriteLine("Invaild value!");
                        goto case 4;
                    }
            }
        }
    }
}
