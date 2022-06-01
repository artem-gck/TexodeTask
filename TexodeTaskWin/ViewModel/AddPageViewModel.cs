using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TexodeTaskWin.Model;
using TexodeTaskWin.Service;
using TexodeTaskWin.Service.Model;
using TexodeTaskWin.View;
using TexodeTaskWin.ViewModel.Command;

namespace TexodeTaskWin.ViewModel
{
    public class AddPageViewModel : INotifyPropertyChanged
    {
        private readonly ICardService _cardService;
        private MainWindow _mainWindow;

        private Card card;

        private RelayCommand addPhotoCommand;
        private RelayCommand cancelCommand;
        private RelayCommand saveCommand;

        public Card Card
        {
            get => card;
            set 
            {
                card = value;
                OnPropertyChanged("Card");
            }
        }

        public RelayCommand AddPhotoCommand
        {
            get
            {
                return addPhotoCommand ?? (addPhotoCommand = new RelayCommand(obj =>
                {
                    Card.Photo = LoadImage();
                }));
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(obj =>
                {
                    _mainWindow.frmScreen.Navigate(new MainPage(_cardService, _mainWindow));
                }));
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(obj =>
                {
                    _ = Task.Run(() => _cardService.AddCardAsync(MapCardModel(card))).Result;
                }));
            }
        }

        public AddPageViewModel(ICardService cardService, MainWindow mainWindow)
        {
            _cardService = cardService;
            _mainWindow = mainWindow;

            card = new Card();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private static BitmapImage LoadImage()
        {
            byte[] data = null;
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Text documents (.jpg)|*.jpg";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                var file = dialog.OpenFile();
                
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = file.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    data = ms.ToArray();
                }
            }

            return ConvertImage(data);
        }

        private static BitmapImage ConvertImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private static byte[] ConvertImageToArray(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        private CardModel MapCardModel(Card card)
            => new CardModel()
                {
                    Name = card.Name,
                    Photo = ConvertImageToArray(card.Photo),
                };
    }
}
