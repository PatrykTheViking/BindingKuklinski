using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindingKuklinski
{
    /// <summary>
    /// Logika interakcji dla klasy WidokFilmy.xaml
    /// </summary>
    public partial class WidokFilm : Window
    {
        public WidokFilm(Film film)
        {
            DataContext = film;
            InitializeComponent();
        }
    }
}
