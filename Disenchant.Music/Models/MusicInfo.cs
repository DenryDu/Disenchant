﻿using Disenchant.Music.Extensions;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Disenchant.Music.Models
{
    internal class MusicInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        // 构造函数
        public MusicInfo(StorageFile file)
        {
            TagLib.File f = TagLib.File.Create(file.AsAbstraction());
            Path = file.Path;
            Album = f.Tag.Album;
            Title = f.Tag.Title;
            Artists = f.Tag.AlbumArtists.Concat(f.Tag.Performers).ToArray();
            Format = Path.Split(".")[Path.Split('.').Length - 1].ToLower();
            Year = f.Tag.Year;
            Copyright = f.Tag.Copyright;
            Cover = new BitmapImage();
            if (f.Tag.Pictures != null && f.Tag.Pictures.Length != 0)
            {
                byte[] coverBuffer = (byte[])(f.Tag.Pictures[0].Data.Data);
                using (var stream = new MemoryStream(coverBuffer))
                {
                    stream.Seek(0, SeekOrigin.Begin); var s2 = new MemoryStream(); stream.CopyTo(s2); s2.Position = 0;
                    Cover.DecodePixelHeight = 100; Cover.DecodePixelWidth = 100;
                    Cover.SetSource(s2.AsRandomAccessStream()); s2.Dispose();
                }
            }
            //Genre = f.Tag.Genres;
            Lyric = f.Tag.Lyrics;
            SampleRate = f.Properties.AudioSampleRate;
            Channels = f.Properties.AudioChannels;
            BitRate = f.Properties.AudioBitrate;
            //f.Properties.Codecs;
            Duration = f.Properties.Duration;
        }

        public MusicInfo(string path)
        {
            TagLib.File f = TagLib.File.Create(path);
            Path = path;
            Album = f.Tag.Album;
            Title = f.Tag.Title;
            Artists = f.Tag.AlbumArtists.Concat(f.Tag.Performers).ToArray();
            Format = Path.Split(".")[Path.Split('.').Length - 1].ToLower();
            Year = f.Tag.Year;
            Copyright = f.Tag.Copyright;
            Cover = new BitmapImage();
            if (f.Tag.Pictures != null && f.Tag.Pictures.Length != 0)
            {
                byte[] coverBuffer = (byte[])(f.Tag.Pictures[0].Data.Data);
                using (var stream = new MemoryStream(coverBuffer))
                {
                    stream.Seek(0, SeekOrigin.Begin); var s2 = new MemoryStream(); stream.CopyTo(s2); s2.Position = 0;
                    Cover.DecodePixelHeight = 100; Cover.DecodePixelWidth = 100;
                    Cover.SetSource(s2.AsRandomAccessStream()); s2.Dispose();
                }
            }
            //Genre = f.Tag.Genres;
            Lyric = f.Tag.Lyrics;
            SampleRate = f.Properties.AudioSampleRate;
            Channels = f.Properties.AudioChannels;
            BitRate = f.Properties.AudioBitrate;
            //f.Properties.Codecs;
            Duration = f.Properties.Duration;
        }

        /*
        public MusicInfo(MusicInfo music)
        {
            Path = music.Path;
            Album = music.Album;
            Title = music.Title;
            Artists = music.Artists;
            Format = music.Format;
            Year = music.Year;
            Copyright = music.Copyright;
            Cover = new BitmapImage();
            if (f.Tag.Pictures != null && music.Cover.Length != 0)
            {
                byte[] coverBuffer = (byte[])(f.Tag.Pictures[0].Data.Data);
                using (var stream = new MemoryStream(coverBuffer))
                {
                    stream.Seek(0, SeekOrigin.Begin); var s2 = new MemoryStream(); stream.CopyTo(s2); s2.Position = 0;
                    Cover.DecodePixelHeight = 100; Cover.DecodePixelWidth = 100;
                    Cover.SetSource(s2.AsRandomAccessStream()); s2.Dispose();
                }
            }
            //Genre = f.Tag.Genres;
            Lyric = f.Tag.Lyrics;
            SampleRate = f.Properties.AudioSampleRate;
            Channels = f.Properties.AudioChannels;
            BitRate = f.Properties.AudioBitrate;
            //f.Properties.Codecs;
            Duration = f.Properties.Duration;
        }
        */

        public MusicInfo()
        {
            Cover = new BitmapImage();
        }

        /// <summary>
        /// 私有属性
        /// </summary>

        // 歌曲文件名（路径）
        private string _path;
        public string Path { get { return _path; } set { _path = value; OnPropertyChanged(nameof(Path)); } }

        // 专辑名
        private string _album;
        public string Album { get { return _album; } set { _album = value; OnPropertyChanged(nameof(_album)); } }

        // 歌名
        private string _title;
        public string Title { get { return _title; } set { _title = value; OnPropertyChanged(nameof(_title)); } }

        // 艺术家
        // albumArtists;
        // performers;
        private string[] _artists;
        public string[] Artists
        {
            get { return _artists; }
            set
            {
                if (value.Length > 0)
                {
                    string[] tempArtists = new string[0];
                    foreach (string i in value)
                    {
                        if (i.Contains("、"))
                        {
                            tempArtists = tempArtists.Concat(i.Split('、')).ToArray();
                        }
                        else if (i.Contains(","))
                        {
                            tempArtists = tempArtists.Concat(i.Split(',')).ToArray();
                        }
                        else if (i.Contains("，"))
                        {
                            tempArtists = tempArtists.Concat(i.Split('，')).ToArray();
                        }
                        else
                        {
                            tempArtists = tempArtists.Concat(i.Split("")).ToArray();
                        }
                    }
                    _artists = tempArtists.Distinct().ToArray();
                }
                else
                {
                    _artists = new string[0];
                }

                ArtistsAndAlbumStr = GetArtistsAndAlbum();
                OnPropertyChanged(nameof(Artists));
            }
        }
        private string _artistsAndAlbumStr;
        public string ArtistsAndAlbumStr { get { return _artistsAndAlbumStr; } set { _artistsAndAlbumStr = value; OnPropertyChanged(nameof(ArtistsAndAlbumStr)); } }
        public string GetArtists()
        {
            string str = "";
            foreach (string artist in _artists)
            {
                str += artist;
                str += ",";
            }
            return str.Substring(0, str.Length - 1);
        }
        public string GetArtistsAndAlbum()
        {
            string str = "";
            foreach (string artist in _artists)
            {
                str += artist;
                str += ",";
            }
            return str.Substring(0, str.Length - 1) + " - " + Album;
        }

        // 时长
        private TimeSpan _duration;
        public TimeSpan Duration { get { return _duration; } set { _duration = value; OnPropertyChanged(nameof(Duration)); } }

        // 发行年份
        private uint _year;
        public uint Year { get { return _year; } set { _year = value; OnPropertyChanged(nameof(Year)); } }

        // 格式
        private string _format;
        public string Format { get { return _format; } set { _format = value; OnPropertyChanged(nameof(Format)); } }

        // 版权
        private string _copyright;
        public string Copyright { get { return _copyright; } set { _copyright = value; OnPropertyChanged(nameof(Copyright)); } }

        // 封面
        private BitmapImage _cover;
        public BitmapImage Cover { get { return _cover; } set { _cover = value; OnPropertyChanged(nameof(Cover)); } }

        // 采样率
        private int _sampleRate;
        public int SampleRate { get { return _sampleRate; } set { _sampleRate = value; OnPropertyChanged(nameof(SampleRate)); } }

        // 比特率
        private int _bitRate;
        public int BitRate { get { return _bitRate; } set { _bitRate = value; OnPropertyChanged(nameof(BitRate)); } }

        // Channels
        private int _channels;
        public int Channels { get { return _channels; } set { _channels = value; OnPropertyChanged(nameof(Channels)); } }

        // 歌词
        private string _lyric;
        public string Lyric { get { return _lyric; } set { _lyric = value; OnPropertyChanged(nameof(Lyric)); } }
    }
}
