using System.Data.SqlClient;
using System.Windows;

namespace MedicalLab
{
    public partial class MainWindow : Window
    {
        SqlConnection conn;

        public MainWindow()
        {
            InitializeComponent(); // Это должно быть первым!

            try
            {
                conn = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=MedicalLab;Trusted_Connection=True;");
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Не могу подключиться к БД!");
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string pass = txtPassword.Password;

            var cmd = new SqlCommand(
                "SELECT * FROM employees WHERE login=@l AND password_hash=@p",
                conn);
            cmd.Parameters.AddWithValue("@l", login);
            cmd.Parameters.AddWithValue("@p", pass);

            var r = cmd.ExecuteReader();
            if (r.Read())
            {
                MessageBox.Show("Успешный вход!");
                new AdminWindow(conn).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка входа!");
            }
            r.Close();
        }
    }
}