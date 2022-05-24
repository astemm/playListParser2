using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using plParser.Commands;
using plParser.Models;

namespace plParser.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int num;
        private string  input;
        private string message;
        private PlayList playList;
        private ObservableCollection<Song> songs;

        public MainWindowViewModel()
        {
            OnClickMe = new RelayCommand(OnExecuteButtonClickEvent, o => true);
        }

        public ICommand OnClickMe { get; set; }

        public string Input { 
            get {return  input; }
            set {this.RaiseAndSetIfChanged(ref input, value);}
        }

        public string Message { 
            get {return  message; }
            set {this.RaiseAndSetIfChanged(ref message, value);}
        }

        public PlayList PlayList { 
            get {return  playList; }
            set {this.RaiseAndSetIfChanged(ref playList, value);}
        }

        public ObservableCollection<Song> Songs { 
            get {return  songs; }
            set {this.RaiseAndSetIfChanged(ref songs, value);}
        }

        private void OnExecuteButtonClickEvent(object parameter)
        {
            Console.WriteLine(Input);
            if (String.IsNullOrEmpty(Input)) { Message="PlayList Url is empty";
            return;}
            try {
            HtmlParser parser=new HtmlParser(Input);
            Songs=new ObservableCollection<Song>(parser.ParseSongs());
            PlayList=parser.ParsePlayList();
            num++;
            message = "You displayed playlist";
            Message=message+num;
            }

            catch (Exception ex) {
                Message="Error:the url entered is incorrect or connection is absent";
            }
        }

    }
}
