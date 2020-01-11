using System;
using System.Text;
using System.Windows;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace PZ_Projekt
{
    class DB
    {

        public static SqlConnection GetConnection()
        {

            //utworzenie zmiennej con i przypisanie jej wartości NULL
            SqlConnection con = null;

            /*
             * utworzenie zmiennej strCon i przypisanie jej danych do połączenia z DB
             * Data Source= NAZWA_SERWERA_SQL
             * Initial Catalog= NAZWA_BAZY_SQL
             * User ID= UZYTKOWNIK
             * Password = HASŁO
             */

            string strCon = "Data Source=DESKTOP-NL1D0PO\\SQLEXPRESS;Initial Catalog=PZ_Projekt;User ID=PZ_Projekt;Password=Start123";
            
            //Próa nawiązania połączenia z DB
            try
            {
                con = new SqlConnection(strCon);
                con.Open();
            }
            //PRzechwycenie wyjątku - błąd łączenia z DB
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    MessageBox.Show("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }

            }
            
            return con;
        }

        //Metoda haszowania hasła
        public static string HasloDoBase64(string haslo)
        {
            //kodowanie hasła do sekwencj bajtów
            byte[] bytes = Encoding.Unicode.GetBytes(haslo);

            //tworzenie skrótu SHA1 hasła
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            
            // zwrócenie hasła z hashowaengo i enkodowanego do base64
            return Convert.ToBase64String(inArray);
        }

        public static void ShowLogin()
        {
            //wywołanie okna Login
            Login login = new Login();
            login.Show();

        }
    }
}
