using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace PZ_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        //konstruktor klasy
        public Login()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

        //Utworzenie obiektu rejestracja
        Rejestracja Rejestracja = new Rejestracja();

       //Obsługa linku rejestracja
        private void RejestracjaAction(object sender, RoutedEventArgs e)
        {
            //wyświetlenie formularza rejestracji
            Rejestracja.Show();
                     
            //zamknięcie bieżącego okna
            this.Close();
        }

        //Obsługa przycisku zaloguj
        private void ZalogujAction(object sender, RoutedEventArgs e)
        {
            // walidacja pola email czy zawiera jakąś wartośc
            if (textBoxEmail.Text.Length == 0)
            {
                //wyświetlenie komunikatu błęd 
                errormessage.Text = "Wypełnij pole adres e-mail.";

                //ustawienie kursora na polu email
                textBoxEmail.Focus();
            }
            // walidacja pola email czy adres wygląda na adres e-mail
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {

                //Wyświetlenie komunikatu będu
                errormessage.Text = "Podaj prawidłowy adres e-mail.";

                // zaznaczenie tekstu zawierającego błąd na formularzu
                textBoxEmail.Select(0, textBoxEmail.Text.Length);

                //ustawienie kursora na polu email
                textBoxEmail.Focus();
            }
            //sprawdzenie czy pole hasło jest wypełnione
            if (passwordBox1.Password.Length == 0)
            {
                //Wyświetlenie komunikatu będu
                errormessage.Text = "Podaj hasło.";
                
                //ustawienie kursora na polu hasło
                textBoxEmail.Focus();
            }
            else
            {
                // przypisanie wartości pól formularza do zmiennych
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;

                // wywołanie metody konwertującej i haszującej hasło
                password = DB.HasloDoBase64(password);

                try
                {
                   
                    // nawiązanie połączenia z bazą danych
                    SqlConnection con = DB.GetConnection();

                    // Zapytanie SQL
                    SqlCommand cmd = new SqlCommand("Select * from [dbo].[PZUsers] where email='" + email + "'  and password='" + password + "'", con);
                    
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    
                    //Jeśli podane dane są prawidłowe (zwrócona wartość z bazy >0)
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        //ustawienie userId
                        string userId = dataSet.Tables[0].Rows[0]["userid"].ToString();

                        //Wywołanie ekranu dostępnego po zalogowaniu
                        Zalogowany zalogowany = new Zalogowany(userId);
                        zalogowany.Show();

                        //zamknięcie okna Login
                        Close();
                    }
                    else
                    {
                        //Wyświetlenie komunikatu będu
                        errormessage.Text = "Podano nieprawidłowe dane do logowania";
                    }

                    //Zamknięcie połączenia z DB
                    con.Close();
                }
                // przechwycenie błędu SQL
                catch (SqlException ex)
                {
                    // wyświetlenie wiadomości z błędem
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        MessageBox.Show("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }

                }


            }
        }

        //obsługa przycisku anuluj
        private void AnulujAction(object sender, RoutedEventArgs e)
        {
           //zamknięcie aplikacji
            Application.Current.Shutdown();
        }

    }

}
