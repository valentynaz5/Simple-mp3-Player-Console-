using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Simple_Player
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var playlist = new List<string>{ @"C:\Users\valen\Desktop\Music\Macan\Macan - 2002 18\Macan - Everyday.mp3",
                @"C:\Users\valen\Desktop\Music\Macan\Macan - 2002 18\Macan - Slow Mo.mp3" };

            var playr = new playr(playlist);
            playr.PlaySong();
            Console.WriteLine("Playing your songs..");
            Console.ReadLine();
        }


        public class playr
        {
            private Queue<string> playlist;
            private IWavePlayer player;
            private WaveStream fileWaveStream;

            public playr(List<string> startingPlaylist)
            {
                playlist = new Queue<string>(startingPlaylist);
            }

            public void PlaySong()
            {
                if (playlist.Count < 1)
                {
                    return;
                }

                if (player != null && player.PlaybackState != PlaybackState.Stopped)
                {
                    player.Stop();
                }
                if (fileWaveStream != null)
                {
                    fileWaveStream.Dispose();
                }
                if (player != null)
                {
                    player.Dispose();
                    player = null;
                }

                player = new WaveOutEvent();
                fileWaveStream = new AudioFileReader(playlist.Dequeue());
                player.Init(fileWaveStream);
                player.PlaybackStopped += (sender, evn) => { PlaySong(); };
                player.Play();
            }
        }
    }
}
