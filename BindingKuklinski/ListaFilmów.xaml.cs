using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BindingKuklinski
{
    /// <summary>
    /// Logika interakcji dla klasy ListaFilmów.xaml
    /// </summary>
    public partial class ListaFilmów : Window
    {

        public ObservableCollection<Film> Filmy { get; } = new ObservableCollection<Film>();

        ListBox lista;
        public ListaFilmów()
        {
            DataContext = this;
            InitializeComponent();
            lista = (ListBox)FindName("Lista");
        }

        private void Edytuj(object sender, RoutedEventArgs e)
        {
            Film wybrany = (Film)lista.SelectedItem;
            if (wybrany != null)
                new WidokFilm(wybrany).Show();
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            Film nowy = new Film();
            Filmy.Add(nowy);
            new WidokFilm(nowy).Show();
        }

        private void Usuń(object sender, RoutedEventArgs e)
        {
            Film wybrany = (Film)lista.SelectedItem;
            Filmy.Remove(wybrany);
        }

        private void Importuj(object sender, RoutedEventArgs e)
        {

        }

        private void Eksportuj(object sender, RoutedEventArgs e)
        {

        }

        }
    }