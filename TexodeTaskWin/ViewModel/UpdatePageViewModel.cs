using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TexodeTaskWin.Model;
using TexodeTaskWin.Service;
using TexodeTaskWin.Service.Model;
using TexodeTaskWin.View;
using TexodeTaskWin.ViewModel.Command;

namespace TexodeTaskWin.ViewModel
{
    /// <summary>
    /// ViewModel of updating page.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class UpdatePageViewModel : INotifyPropertyChanged
    {
        private readonly ICardService _cardService;
        private MainWindow _mainWindow;

        private Card card;
        private string errorMassage;

        private RelayCommand addPhotoCommand;
        private RelayCommand cancelCommand;
        private RelayCommand saveCommand;

        /// <summary>
        /// Gets or sets the card.
        /// </summary>
        /// <value>
        /// The card.
        /// </value>
        public Card Card
        {
            get => card;
            set
            {
                card = value;
                OnPropertyChanged("Card");
            }
        }

        /// <summary>
        /// Gets or sets the error massage.
        /// </summary>
        /// <value>
        /// The error massage.
        /// </value>
        public string ErrorMassage
        {
            get => errorMassage;
            set
            {
                errorMassage = value;
                OnPropertyChanged("ErrorMassage");
            }
        }

        /// <summary>
        /// Gets the add photo command.
        /// </summary>
        /// <value>
        /// The add photo command.
        /// </value>
        public RelayCommand AddPhotoCommand
        {
            get
            {
                return addPhotoCommand ?? (addPhotoCommand = new RelayCommand(obj =>
                {
                    ErrorMassage = string.Empty;
                    Card.Photo = LoadImage();
                }));
            }
        }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        /// <value>
        /// The cancel command.
        /// </value>
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

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(obj =>
                {
                    if (string.IsNullOrWhiteSpace(Card.Name) || Card.Photo is null)
                    {
                        ErrorMassage = "*Заполните все поля";
                        return;
                    }

                    _ = Task.Run(() => _cardService.UpdateCardAsync(MapCardModel(card))).Result;
                    _mainWindow.frmScreen.Navigate(new MainPage(_cardService, _mainWindow));
                }));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePageViewModel"/> class.
        /// </summary>
        /// <param name="cardService">The card service.</param>
        /// <param name="mainWindow">The main window.</param>
        /// <param name="id">The identifier.</param>
        public UpdatePageViewModel(ICardService cardService, MainWindow mainWindow, int id)
        {
            _cardService = cardService;
            _mainWindow = mainWindow;

            var cardModel = Task.Run(() => cardService.GetCardAsync(id)).Result;
            Card = new Card()
            {
                Id = cardModel.Id,
                Name = cardModel.Name,
                Photo = ConvertImage(cardModel.Photo)
            };
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="prop">The property.</param>
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
            dialog.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                var file = dialog.OpenFile();

                var buffer = new byte[16 * 1024];
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
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        private CardModel MapCardModel(Card card)
            => new CardModel()
            {
                Id = card.Id,
                Name = card.Name,
                Photo = ConvertImageToArray(card.Photo),
            };
    }
}
