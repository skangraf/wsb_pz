using System.Windows;
using System.Windows.Input;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace PZ_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 

    public partial class Rejestracja : Window
    {


        public Rejestracja()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        // akcja na kliknięcie przycisku reset na forumarzu
        private void ZresetujForm(object sender, RoutedEventArgs e)
        {

            // wywyołanie metody reset()
            Reset();
        }

        // obsługa przycisku anuluj
        private void AnulujForm(object sender, RoutedEventArgs e)
        {
            //wywołanie okna Login
            DB.ShowLogin();

            //zamknięcie bieżącego okna
            Close();
        }

        // Metoda czyszcząca pola formularza
        public void Reset()
        {
            // przypisanie pustych wartości do pól formularza
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        //obsługa przycisku wyślij
        private void WyslijForm(object sender, RoutedEventArgs e)
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
                
                //sprawdzenie czy pole hasło jest wypełnione
                if (passwordBox1.Password.Length == 0)
                {
                    //Wyświetlenie komunikatu będu
                    errormessage.Text = "Wypełnij pole hasło.";
                }
                //sprawdzenie czy pole potwierdź hasło jest wypełnione
                else if (passwordBoxConfirm.Password.Length == 0)
                {
                    //Wyświetlenie komunikatu będu
                    errormessage.Text = "Wypełnij pole powtórz hasło.";
                    
                    //ustawienie kursora na polu hasło
                    passwordBoxConfirm.Focus();
                }
                //Sprawdzenie czy hasła są identyczne
                else if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    //Wyświetlenie komunikatu będu
                    errormessage.Text = "Hasła nie są identyczne.";

                    //ustawienie kursora na polu potwierdź hasło
                    passwordBoxConfirm.Focus();
                }
                else
                {
                    // wywołanie metody konwertującej i haszującej hasło
                    password = DB.HasloDoBase64(password);
                   
                    // Likwidacja komunikatu błędu
                    errormessage.Text = "";

                    // próba zapisu danych użytkownia do bazy, w przypadku niepowodzenia wyświetla komunikat błędu
                    try
                    {
                        // nawiązanie połączenia z bazą danych
                        SqlConnection con = DB.GetConnection();

                        // Zapytanie SQL
                        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PZUsers] ([userid],[email],[password],[imie],[nazwisko]) VALUES (newid(), '" + email + "', '" + password + "', '" + firstname + "', '" + lastname + "')", con)
                        {
                            CommandType = CommandType.Text
                        };

                        //Wykonanie zapytania SQL
                        cmd.ExecuteNonQuery();

                        // zamknięcie połączenia z bazą
                        con.Close();

                        //Wyświetlenie komunikatu o powodzeniu
                        MessageBox.Show("Proces rejestracji przebiegł pomyślnie. Prosimy o zalogowanie się");
                        
                        //Zresetowanie pól formularza
                        Reset();

                        //Wywołanie ekranuy zalogowania
                        Login login = new Login();
                        login.Show();

                        //zamknięcie bieżącego okna
                        Close();

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

        // obsługa linku zaloguj
        private void ZalogujForm(object sender, MouseButtonEventArgs e)
        {
            //wywołanie okna Login
            DB.ShowLogin();

            //zamknięcie bieżącego okna
            Close();
        }

    }
}
