using App8.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App8.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MediaPlay : Page
    {
        private bool isPlaying = false;

        int onPlay = 0;

        TimeSpan _position;

        DispatcherTimer _timer = new DispatcherTimer();

        private ObservableCollection<Song> listSong;

        internal ObservableCollection<Song> ListSong { get => listSong; set => listSong = value; }

        public MediaPlay()
        {
            this.ListSong = new ObservableCollection<Song>();
            this.ListSong.Add(new Song()
            {
                name = "Không còn gì",
                singer = "Nguyễn Trọng Tài; San Ji; Double X",
                thumbnail = "http://125.212.211.135/~csn/data/cover/96/95464.jpg",
                link = "http://data31.chiasenhac.com/downloads/1961/2/1960160-2697f348/128/HongKong1%20-%20Nguyen%20Trong%20Tai_%20San%20Ji_%20Do%20[128kbps_MP3].mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Người Khác",
                singer = "Phan Mạnh Quỳnh",
                thumbnail = "http://125.212.211.135/~csn/data/cover/94/93502.jpg",
                link = "http://data35.chiasenhac.com/downloads/1946/2/1945523-b5fafafc/128/Nguoi%20Khac%20Piano%20RnB%20Version_%20-%20Phan%20Man%20[128kbps_MP3].mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "When you say nothing at all",
                singer = "Ronan Keating",
                thumbnail = "http://125.212.211.135/~csn/data/cover/18/17825.jpg",
                link = "http://data23.chiasenhac.com/downloads/1236/2/1235647-a49ef9a2/128/When%20You%20Say%20Nothing%20At%20All%20-%20Ronan%20Keat%20[128kbps_MP3].mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Đừng nói tôi điên",
                singer = "Hiền Hồ",
                thumbnail = "http://125.212.211.135/~csn/data/cover/97/96128.jpg",
                link = "http://data31.chiasenhac.com/downloads/1964/1/1963974-0ff6ef6c/128/Dung%20Noi%20Toi%20Dien%20-%20Hien%20Ho%20[128kbps_MP3].mp3"
            });

            this.ListSong.Add(new Song()
            {
                name = "Lạc Trôi",
                singer = "Như Thùy",
                link = "http://data3.chiasenhac.com/downloads/1789/2/1788448-596dfdb5/128/Lac%20Troi%20-%20Nhu%20Thuy%20[128kbps_MP3].mp3",
                thumbnail = "http://125.212.211.135/~csn/data/cover/79/78949.jpg"
            });
            this.ListSong.Add(new Song()
            {
                name = "Như Lời Đồn",
                singer = "Bảo Anh",
                thumbnail = "http://125.212.211.135/~csn/data/cover/96/95739.jpg",
                link = "http://data31.chiasenhac.com/downloads/1963/1/1962185-2ec33485/128/Nhu%20Loi%20Don%20-%20Bao%20Anh%20[128kbps_MP3].mp3"
            });

            this.ListSong.Add(new Song()
            {
                name = "Tình xưa nghĩa cũ",
                singer = "Jimmii Nguyễn ",
                thumbnail = "http://125.212.211.135/~csn/data/cover/6/5132.jpg",
                link = "http://data18.chiasenhac.com/downloads/1077/2/1076487-d948641f/128/Tinh%20Xua%20Nghia%20Cu%20-%20Jimmii%20Nguyen%20[128kbps_MP3].mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Giấc mơ chỉ là giấc mơ",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://i.ytimg.com/vi/J_VuNwxSEi0/maxresdefault.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui945/GiacMoChiLaGiacMoSeeSingShare2-HaAnhTuan-5082049.mp3"
            });
     
            this.InitializeComponent();
            this.VolumeSlider.Value = 100;
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += ticktock;
            _timer.Start();
        }

        private void ticktock(object sender, object e)
        {
            MinDuration.Text = MediaPlayer.Position.Minutes + ":" + MediaPlayer.Position.Seconds;
            Progress.Minimum = 0;
            Progress.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            MaxDuration.Text = MediaPlayer.NaturalDuration.TimeSpan.Minutes + ":" + MediaPlayer.NaturalDuration.TimeSpan.Seconds;
            Progress.Value = MediaPlayer.Position.TotalSeconds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void goHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            Song selectedSong = panel.Tag as Song;
            Debug.WriteLine(ListSong[0].name);
            onPlay = MenuList.SelectedIndex;
            LoadSong(selectedSong);
            PlaySong();
        }
        private void PlaySong()
        {
            MediaPlayer.Play();
            PlayButton.Icon = new SymbolIcon(Symbol.Pause);
            isPlaying = true;
        }
        private void PauseSong()
        {
            MediaPlayer.Pause();
            PlayButton.Icon = new SymbolIcon(Symbol.Play);
            isPlaying = false;
        }
        private void PlayClick(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                PauseSong();
            }
            else
            {
                PlaySong();
            }
        }
        //private void callFormLogin(object sender, RoutedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(FormLogin));
        //}
        private void PlayBack(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            if (onPlay > 0)
            {
                onPlay = onPlay - 1;
            }
            else
            {
                onPlay = ListSong.Count - 1;
            }
            LoadSong(ListSong[onPlay]);
            PlaySong();
            MenuList.SelectedIndex = onPlay;
        }

        private void PlayNext(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            if (onPlay < ListSong.Count - 1)
            {
                onPlay = onPlay + 1;
            }
            else
            {
                onPlay = 0;
            }
            LoadSong(ListSong[onPlay]);
            PlaySong();
            MenuList.SelectedIndex = onPlay;
        }
        private void LoadSong(Entity.Song currentSong)
        {
            this.NowPlaying.Text = "Loading";
            MediaPlayer.Source = new Uri(currentSong.link);
            Debug.WriteLine(MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds);
            this.NowPlaying.Text = currentSong.name + " - " + currentSong.singer;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (MediaPlayer.Source != null && MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                Progress.Minimum = 0;
                Progress.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                Progress.Value = MediaPlayer.Position.TotalSeconds;

            }
        }

        private void VolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider vol = sender as Slider;
            if (vol != null)
            {
                MediaPlayer.Volume = vol.Value / 100;
                this.volume.Text = vol.Value.ToString();
            }
        }

        private void add_Song(object sender, RoutedEventArgs e)
        {
           // this.Frame.Navigate(typeof(SongForm));
        }
    }
}
