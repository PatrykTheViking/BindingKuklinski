using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BindingKuklinski
{
    public class Film : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static Dictionary<string, ICollection<string>> powiązaneWłaściwości = new Dictionary<string, ICollection<string>>()
        {
            ["Tytuł"] = new string[] { "Tytuł" },
            ["Reżyser"] = new string[] {"Reżyser"},
            ["Wydawca"] = new string[] { "Wydawca" },
            ["Nośnik"] = new string[] { "DVD", "VHS", "BlueRay" },
            ["Data Wydania"] = new string[] { "Data Wydania" }
        };

        private void NotyfikujZmianę(
            [CallerMemberName] string nazwaWłaściwości = null,
            HashSet<string> jużZałatwione = null
            )
        {
            if (jużZałatwione == null)
                jużZałatwione = new HashSet<string>();
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nazwaWłaściwości)
                );
            jużZałatwione.Add(nazwaWłaściwości);
            if (powiązaneWłaściwości.ContainsKey(nazwaWłaściwości))
                foreach (
                    string powiązanaWłaściwość
                    in
                    powiązaneWłaściwości[nazwaWłaściwości]
                    )
                    if (jużZałatwione.Contains(powiązanaWłaściwość) == false)
                        NotyfikujZmianę(powiązanaWłaściwość, jużZałatwione);
        }

        private string
            tytuł,
            reżyser,
            wydawca;

        private DateTime
            dataWydania = DateTime.Now;

        private enum
            Nośnik
        {
            dvd,
            vhs,
            blueray
        };

/*        public Nośnik nośnik
        { 
            get => nośnik; 
            set 
            { 
                nośnik = value; 
                NotyfikujZmianę();
            }
        }*/

public String Tytuł
        {
            get => tytuł;
            set
            {
                tytuł = value;
                NotyfikujZmianę();
            }
        }

        public String Reżyser
        {
            get => reżyser;
            set
            {
                reżyser = value;
                NotyfikujZmianę();
            }
        }

        public String Wydawca
        {
            get => wydawca;
            set
            {
                wydawca = value;
                NotyfikujZmianę();
            }
        }
        public DateTime DataWydania
        {
            get => dataWydania;
            set
            {
                dataWydania = value;
                NotyfikujZmianę();
            }
        }



        public string TytułReżyser => $"{tytuł}, Reżyser - {reżyser}";

        public string SkrótSzczegółów => $"{TytułReżyser}, wydawca - {wydawca}, data wydania - {dataWydania}";
    }
}
