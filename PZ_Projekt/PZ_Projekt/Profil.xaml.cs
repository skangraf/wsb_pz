using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;


namespace PZ_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Profil.xaml
    /// </summary>
    public partial class Profil : Window
    {

        private string UserId { get; set; }

        //konstruktor klasy
        public Profil(string userid)
        {
            InitializeComponent();
            this.UserId = userid;
            this.GetUser();
            
        }

        //metoda pobierająca Dane użytkownika z bazy SQL i wypełniająca pola formularza
        private void GetUser()
        {
            try
            {

                // nawiązanie połączenia z bazą danych
                SqlConnection con = DB.GetConnection();
                
                // Zapytanie SQL
                SqlCommand cmd = new SqlCommand("Select * from [dbo].[PZUsers] where userid='" + this.UserId + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                //Jeśli podane dane są prawidłowe (zwrócona wartość z bazy >0)
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    // uzupełnienie pól formularza o dane użytkownika pobrane z bazy SQL
                    textBoxFirstName.AppendText(dataSet.Tables[0].Rows[0]["imie"].ToString());
                    textBoxLastName.AppendText(dataSet.Tables[0].Rows[0]["nazwisko"].ToString());
                    textBoxEmail.AppendText(dataSet.Tables[0].Rows[0]["email"].ToString());

                }
                //Zamknięcie połączenia z bazą danych
                con.Close();
            }
            //Przechwycenie błędu SQL
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

        // Obsługa przycisku anuluj
        private void AnulujAction(object sender, RoutedEventArgs e)
        {
            //Wywołanie Ekranu dla zalogowanych
            Zalogowany zalogowany = new Zalogowany(this.UserId);
            zalogowany.Show();

            //zamknięcie bieżącego okna
            this.Close();
        }

        //Zapis formularza do bazy SQL
        private void ZapiszAction(object sender, RoutedEventArgs e)
        {

            // walidacja pola email czy zawiera jakąś wartość
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
            // akcja jeśli adres email jest poprawny
            else
            {
                // przypisanie wartości pól formularza do zmiennych
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                
                //zmienna query - zapytania SQL
                string query = " ";

                //Jeżeli pole hasło zostało wypełnione i są różne wartości pól hasło oraz powtórz hasło
                if (passwordBoxConfirm.Password.Length > 0 && passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    //Wyświetlenie komunikatu będu
                    errormessage.Text = "Hasła nie są identyczne.";

                    //ustawienie kursora na polu powtórz hasło
                    passwordBoxConfirm.Focus();
                }
                else
                {
                    //jeżeli wypełnione jest pole hasło
                    if (passwordBoxConfirm.Password.Length > 0)
                    {
                        // wywołanie metody konwertującej i haszującej hasło
                        password = DB.HasloDoBase64(password);

                        // wygenerowanie kodu zapytania SQL z modyfikacją hasła użytkownika
                        query = "UPDATE PZUsers SET email = '" + email + "', password = '" + password + "', imie = '" + firstname + "', nazwisko = '" + lastname + "' WHERE userid = '" + this.UserId + "'";

                    }
                    else
                    {
                        // wygenerowanie kodu zapytania SQL bez modyfikacji hasła użytkownika
                        query = "UPDATE PZUsers SET email = '" + email + "', imie = '" + firstname + "', nazwisko = '" + lastname + "' WHERE userid = '"+ this.UserId+"'";
                    }

                    //Wyczzyszenie komunikatu błędu    
                    errormessage.Text = "";

                    // próba zapisu danych użytkownia do bazy, w przypadku niepowodzenia wyświetla komunikat błędu
                    try
                    {
                        // nawiązanie połączenia z bazą danych
                        SqlConnection con = DB.GetConnection();

                        // Utworzenie zapytania SQL
                        SqlCommand cmd = new SqlCommand(query, con)
                        {
                            CommandType = CommandType.Text
                        };

                        //Wykonanie zapytania SQL
                        cmd.ExecuteNonQuery();

                        // zamknięcie połączenia z bazą
                        con.Close();

                        //Wyswietlenie komunikatu o powodzeniu aktualizacji konta
                        MessageBox.Show("Konto zostało zaktualizowane");

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
        }
    }
}
