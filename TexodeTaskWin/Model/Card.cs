using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TexodeTaskWin.Model
{
    public class Card : INotifyPropertyChanged
    {
        private bool _isSelected;
        private int id;
        private string name;
        private BitmapImage photo;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        public int Id { 
            get => id; 
            set
            {
                id = value;
                OnPropertyChanged("Id");
            } 
        }
        public string Name { 
            get => name; 
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public BitmapImage Photo { 
            get => photo;
            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
