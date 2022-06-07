using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TexodeTaskWin.Model;
using TexodeTaskWin.Service;
using TexodeTaskWin.Service.Model;
using TexodeTaskWin.View;
using TexodeTaskWin.ViewModel.Command;

namespace TexodeTaskWin.ViewModel
{
    /// <summary>
    /// ViewModel for main page.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
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

        /// <summary>
        /// Gets or sets the cards.
        /// </summary>
        /// <value>
        /// The cards.
        /// </value>
        public ObservableCollection<Card> Cards
        {
            get => сards;
            set
            {
                сards = value;
                OnPropertyChanged("Cards");
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
        /// Gets the sort command.
        /// </summary>
        /// <value>
        /// The sort command.
        /// </value>
        public RelayCommand SortCommand
        {
            get
            {
                return sortCommand ?? (sortCommand = new RelayCommand(obj =>
                {
                    ErrorMassage = string.Empty;

                    try
                    {
                        Cards = MapCardsModelToCards(Task.Run(() => _cardService.SortCardsByNameAsync()).Result);
                    }
                    catch (Exception)
                    {
                        var result = MessageBox.Show("Соединение с сервером было прервано, для решения проблемы обратитесь к специалисту.", "Потеря соединения", MessageBoxButton.OK);

                        if (result == MessageBoxResult.OK)
                            _mainWindow.Close();
                    }

                    _mainPage.btnSort.Background = new SolidColorBrush(Color.FromRgb(58, 61, 121));
                }));
            }
        }

        /// <summary>
        /// Gets the un sort command.
        /// </summary>
        /// <value>
        /// The un sort command.
        /// </value>
        public RelayCommand UnSortCommand
        {
            get
            {
                return unSortCommand ?? (unSortCommand = new RelayCommand(obj =>
                  {
                      ErrorMassage = string.Empty;

                      try
                      {
                          Cards = MapCardsModelToCards(Task.Run(() => _cardService.GetAllCardsAsync()).Result);
                      }
                      catch (Exception)
                      {
                          var result = MessageBox.Show("Соединение с сервером было прервано, для решения проблемы обратитесь к специалисту.", "Потеря соединения", MessageBoxButton.OK);

                          if (result == MessageBoxResult.OK)
                              _mainWindow.Close();
                      }

                      _mainPage.btnSort.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                  }));
            }
        }

        /// <summary>
        /// Gets the add view command.
        /// </summary>
        /// <value>
        /// The add view command.
        /// </value>
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

        /// <summary>
        /// Gets the update view command.
        /// </summary>
        /// <value>
        /// The update view command.
        /// </value>
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

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(obj =>
                {
                    var cardsIdForDelete = Cards.Where(card => card.IsSelected).Select(card => card.Id).ToList();

                    try
                    {
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
                    }
                    catch (Exception)
                    {
                        var result = MessageBox.Show("Соединение с сервером было прервано, для решения проблемы обратитесь к специалисту.", "Потеря соединения", MessageBoxButton.OK);

                        if (result == MessageBoxResult.OK)
                            _mainWindow.Close();
                    }

                }));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="cardService">The card service.</param>
        /// <param name="mainPage">The main page.</param>
        /// <param name="mainWindow">The main window.</param>
        public MainPageViewModel(ICardService cardService, MainPage mainPage, MainWindow mainWindow)
        {
            try
            {
                Cards = MapCardsModelToCards(Task.Run(() => cardService.GetAllCardsAsync()).Result);
            }                                
            catch (Exception)
            {
                var result = MessageBox.Show("Соединение с сервером было прервано, для решения проблемы обратитесь к специалисту.", "Потеря соединения", MessageBoxButton.OK);

                if (result == MessageBoxResult.OK)
                    _mainWindow.Close();
            }

            _cardService = cardService;
            _mainPage = mainPage;
            _mainWindow = mainWindow;
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
