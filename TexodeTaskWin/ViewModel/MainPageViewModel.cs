using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TexodeTaskWin.Model;
using TexodeTaskWin.Service;
using TexodeTaskWin.Service.Model;
using TexodeTaskWin.View;
using TexodeTaskWin.ViewModel.Command;

namespace TexodeTaskWin.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Card> сards;
        private string errorMassage;

        private readonly ICardService _cardService;
        private MainPage _mainPage;
        private MainWindow _mainWindow;

        private RelayCommand sortCommand;
        private RelayCommand unSortCommand;
        private RelayCommand addViewCommand;
        private RelayCommand updateViewCommand;
        private RelayCommand deleteCommand;

        public ObservableCollection<Card> Cards
        {
            get => сards;
            set
            {
                сards = value;
                OnPropertyChanged("Cards");
            }
        }

        public string ErrorMassage
        {
            get => errorMassage;
            set
            {
                errorMassage = value;
                OnPropertyChanged("ErrorMassage");
            }
        }

        public RelayCommand SortCommand
        {
            get
            {
                return sortCommand ?? (sortCommand = new RelayCommand(obj =>
                {
                    ErrorMassage = string.Empty;
                    Cards = MapCardsModelToCards(Task.Run(() => _cardService.SortCardsByNameAsync()).Result);
                    _mainPage.btnSort.Background = new SolidColorBrush(Color.FromRgb(58, 61, 121));
                }));
            }
        }

        public RelayCommand UnSortCommand
        {
            get
            {
                return unSortCommand ?? (unSortCommand = new RelayCommand(obj =>
                  {
                      ErrorMassage = string.Empty;
                      Cards = MapCardsModelToCards(Task.Run(() => _cardService.GetAllCardsAsync()).Result);
                      _mainPage.btnSort.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                  }));
            }
        }

        public RelayCommand AddViewCommand
        {
            get
            {
                return addViewCommand ?? (addViewCommand = new RelayCommand(obj =>
                {
                    _mainWindow.frmScreen.Navigate(new AddPage(_cardService, _mainWindow));
                }));
            }
        }

        public RelayCommand UpdateViewCommand
        {
            get
            {
                return updateViewCommand ?? (updateViewCommand = new RelayCommand(obj =>
                {
                    if (сards.Where(card => card.IsSelected).Count() != 1)
                    {
                        ErrorMassage = "*Выберите 1 карточку";
                        return;
                    }

                    _mainWindow.frmScreen.Navigate(new UpdatePage(_cardService, _mainWindow, Cards.Where(card => card.IsSelected).FirstOrDefault().Id));
                }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(obj =>
                {
                    var cardsIdForDelete = Cards.Where(card => card.IsSelected).Select(card => card.Id).ToList();

                    if (cardsIdForDelete.Count() == 0)
                    {
                        ErrorMassage = "*Выберите карточки";
                        return;
                    }
                    else if (cardsIdForDelete.Count() > 1)
                        _ = Task.Run(() => _cardService.DeleteListOFCardsAsync(cardsIdForDelete)).Result;
                    else
                        _ = Task.Run(() => _cardService.DeleteCardAsync(cardsIdForDelete.FirstOrDefault())).Result;

                    Cards = MapCardsModelToCards(Task.Run(() => _cardService.GetAllCardsAsync()).Result);
                }));
            }
        }

        public MainPageViewModel(ICardService cardService, MainPage mainPage, MainWindow mainWindow)
        {
            Cards = MapCardsModelToCards(Task.Run(() => cardService.GetAllCardsAsync()).Result);

            _cardService = cardService;
            _mainPage = mainPage;
            _mainWindow = mainWindow;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private ObservableCollection<Card> MapCardsModelToCards(IEnumerable<CardModel> cards)
            => new ObservableCollection<Card>(cards.Select(card => new Card()
            {
                Id = card.Id,
                Name = card.Name,
                Photo = LoadImage(card.Photo),
            }));

        private static BitmapImage LoadImage(byte[] imageData)
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
    }
}
