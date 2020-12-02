using System.IO;
using System.Net;
using System.Net.Http;
using System;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Diagnostics;

namespace youtube_dl_companion
{
    class Program
    {
        static void Main(string[] args)
        {
        Start:
            {
                // some ytdl command arg
                string bz = "bestvideo[ext=mp4]+bestaudio[ext=m4a]/best[ext=mp4]/best";
                string ba = "bestaudio[ext=m4a]";
                string ap = "\"%(playlist)s/%(playlist_index)s - %(title)s.%(ext)s\"";
                // cmd stuff
                string arg = "/C";
                // case switch time
                Console.Write("Enter your selection, 5 to help, 6 to update youtube-dl & ffmpeg (1-6): ");
                sbyte caseSw = Convert.ToSByte(Console.ReadLine());
                switch (caseSw)
                {
                    // only one video
                    case 1:
                        {
                            Console.WriteLine("Type Youtube Video URL");
                            string u = Console.ReadLine();
                            string ytdl = $"youtube-dl.exe -f {bz} {u}";
                            Process.Start("cmd.exe", arg + ytdl).WaitForExit();
                            goto Repeat;
                        }
                    // audio only
                    case 2:
                        {
                            Console.WriteLine("Type Youtube URL");
                            string u = Console.ReadLine();
                            string ytdl = $"youtube-dl.exe -f {ba} {u}";
                            Process.Start("cmd.exe", arg + ytdl).WaitForExit();
                            goto Repeat;
                        }
                    // download a whole playlist
                    case 3:
                        {
                            Console.WriteLine("Type Youtube Playlist");
                            string u = Console.ReadLine();
                            string ytdl = $"youtube-dl.exe -f {bz} -o {ap} {u}";
                            Process.Start("cmd.exe", arg + ytdl).WaitForExit();
                            goto Repeat;
                        }
                    // download playlist as audio-only
                    case 4:
                        {
                            Console.WriteLine("Type Youtube Playlist");
                            string u = Console.ReadLine();
                            string ytdl = $"youtube-dl.exe -f {ba} -o {ap} {u}";
                            Process.Start("cmd.exe", arg + ytdl).WaitForExit();
                            goto Repeat;
                        }
                    // the worst help section ever
                    case 5:
                        {
                            string ytdlh = $"youtube-dl.exe --version";
                            string ffmpegv = $"ffmpeg -version";
                            Console.WriteLine("Option 1: Download the highest quality version of that video");
                            Console.WriteLine("Option 2: Download the highest audio quality version of that video (m4a format)");
                            Console.WriteLine("Option 3: Download the entire playlist");
                            Console.WriteLine("Option 4: Download the entire playlist as audio only (m4a format)");
                            Console.WriteLine("Option 5: This line of text");
                            Console.WriteLine("Option 6: This line of text");
                            Console.WriteLine("youtube-dl ref app version 00, reported youtube-dl version:");
                            Process.Start("cmd.exe", arg + ytdlh).WaitForExit();
                            Console.ReadKey();
                            goto Repeat;
                        }
                    // a poorly written updater, exploitable as hell
                    case 6:
                        {
                            string ytdlh = $"youtube-dl.exe --version";
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
                            Console.Write("Downloading ffmpeg:");
                            wc.DownloadFile(sauceffpeg, "ffmpeg.7z");
                            Console.WriteLine(" done!");
                            Console.Write("Downloading ytdl:");
                            wc.DownloadFile(sauceytdl, "youtube-dl.exe");
                            Console.WriteLine(" done!");
                            Console.WriteLine("Extracting ffmpeg: ");
                            Process.Start("cmd.exe", arg + "7z.exe" + " e " + " -y " + " ffmpeg.7z " + " ffmpeg.exe " + " -r ").WaitForExit();
                            // this will always display before the extraction for some reason
                            Console.WriteLine("Finished updating the core component");
                            Console.WriteLine("Cleaning up!");
                            File.Delete("ffmpeg.7z");
                            goto Repeat;
                        }
                    //invaild value
                    default:
                        {
                            Console.WriteLine("Invaild value!");
                            goto Repeat;
                        }
                }
            }
            Repeat:
                {
                    Console.WriteLine("Do you want do another task? 1 for yes, 2 for no");
                    sbyte caseSwa = Convert.ToSByte(Console.ReadLine());
                    switch (caseSwa)
                        {
                            case 1:
                                {
                                    Console.Clear();
                                    goto Start;
                                }
                            case 2:
                                {
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Invaild value!");
                                    goto Repeat;
                                }
                        }
                }
        }
    }
}
