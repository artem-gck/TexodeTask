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
    /// <summary>
    /// Card model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class Card : INotifyPropertyChanged
    {
        private bool _isSelected;
        private int id;
        private string name;
        private BitmapImage photo;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { 
            get => id; 
            set
            {
                id = value;
                OnPropertyChanged("Id");
            } 
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { 
            get => name; 
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public BitmapImage Photo { 
            get => photo;
            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
