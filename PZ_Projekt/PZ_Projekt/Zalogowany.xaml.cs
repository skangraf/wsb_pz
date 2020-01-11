using System.Windows;


namespace PZ_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Zalogowany.xaml
    /// </summary>
    public partial class Zalogowany : Window
    {
        // Pole user metoda get
        private string User { set;  get; }

        //konstruktor klasy
        public Zalogowany(string user)
        {
            InitializeComponent();
            
            this.User = user;
        }

        //Obsługa przycisku wyloguj 
        private void WylogujAction(object sender, RoutedEventArgs e)
        {
            //wywołanie okna Login
            DB.ShowLogin();

            //zamknięcie bieżącego okna
            Close();
        }
        private void EdytujAction(object sender, RoutedEventArgs e)
        {
            //wywołanie okna Profil
            Profil profil = new Profil(this.User);
            profil.Show();


            //zamknięcie bieżącego okna
            this.Close();
        }
    }
}
