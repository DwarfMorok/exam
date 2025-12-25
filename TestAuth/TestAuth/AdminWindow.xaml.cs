using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace MedicalLab
{
    public partial class AdminWindow : Window
    {
        SqlConnection conn;

        public AdminWindow(SqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
            RefreshEmployees();
            RefreshOrders();
        }

        void RefreshEmployees(object sender = null, RoutedEventArgs e = null)
        {
            var cmd = new SqlCommand(
                @"SELECT e.id, e.full_name, e.login, er.role_name, 
                  e.login_attempts, e.is_blocked
                  FROM employees e
                  JOIN employee_roles er ON e.role_id = er.id",
                conn);

            var table = new System.Data.DataTable();
            new SqlDataAdapter(cmd).Fill(table);
            gridEmployees.ItemsSource = table.DefaultView;
        }

        void RefreshOrders(object sender = null, RoutedEventArgs e = null)
        {
            var cmd = new SqlCommand(
                @"SELECT o.order_number, p.full_name, o.order_date, o.total_amount
                  FROM orders o
                  JOIN patients p ON o.patient_id = p.id",
                conn);

            var table = new System.Data.DataTable();
            new SqlDataAdapter(cmd).Fill(table);
            gridOrders.ItemsSource = table.DefaultView;
        }
    }
}