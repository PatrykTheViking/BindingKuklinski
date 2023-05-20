using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
using Exel = Microsoft.Office.Interop.Excel;
using ListBox = System.Windows.Controls.ListBox;

namespace BindingKuklinski
{
    /// <summary>
    /// Logika interakcji dla klasy ListaFilmów.xaml
    /// </summary>
    public partial class ListaFilmów : System.Windows.Window
    {

        public ObservableCollection<Film> Filmy { get; } = new ObservableCollection<Film>();

        System.Windows.Controls.ListBox lista;
        List<Film> Films = new List<Film>();
        string plik = $@"c:\Users\{Environment.UserName}\Desktop\filmy_export";

        public ListaFilmów()
        {
            DataContext = this;
            InitializeComponent();
            lista = (ListBox)FindName("Lista");
        }

        private void OpenFile()
        {
            try
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = true;
                Workbooks books = excelApp.Workbooks;
                Workbook sheets = books.Open(plik);
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

            // ********* Żeby użyć funkcji exportuj Microsoft VS musi zostać uruchomione jako Administrator!! ******* // 

            Films.Add(new Film());
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = app.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet mySheet = (Worksheet)wb.Worksheets.get_Item(1);
            
            mySheet.Cells[1, 1] = "Tytuł";
            mySheet.Cells[1, 1].font.Bold = true;

            mySheet.Cells[1, 2] = "Reżyser";
            mySheet.Cells[1, 2].font.Bold = true;

            mySheet.Cells[1, 3] = "Wydawca";
            mySheet.Cells[1, 3].font.Bold = true;

            mySheet.Cells[1, 4] = "Data Wydania";
            mySheet.Cells[1, 4].font.Bold = true;

            mySheet.Columns[1].ColumnWidth = 15;
            mySheet.Columns[2].ColumnWidth = 15;
            mySheet.Columns[3].ColumnWidth = 15;
            mySheet.Columns[4].ColumnWidth = 15;

            int i = 2;
            foreach(Film f in Films)
            {
                mySheet.Cells[i, 1] = f.Tytuł;
                mySheet.Cells[i, 2] = f.Reżyser;
                mySheet.Cells[i, 3] = f.Wydawca;
                mySheet.Cells[i, 4] = f.DataWydania;
                i++;
            }

            wb.SaveAs(plik, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, XlSaveAsAccessMode.xlExclusive, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            MessageBoxResult result = MessageBox.Show("Czy chcesz otworzyć plik?", "Otwieram plik...", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result.Equals(MessageBoxResult.Yes))
            {
                OpenFile();
            } 
            else
            {
                MessageBox.Show("Twój plik został zapisany na pulpicie", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            wb.Close(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            app.Quit();

            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Marshal.ReleaseComObject(wb);
            Marshal.ReleaseComObject(mySheet);
            Marshal.ReleaseComObject(app);

            this.Close();
        }
    }
 }