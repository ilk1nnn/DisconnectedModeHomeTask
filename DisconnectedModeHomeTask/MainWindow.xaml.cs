using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DisconnectedModeHomeTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnection connection;
        string conn_string = String.Empty;
        DataTable dataTable;
        SqlDataReader reader;
  

        public MainWindow()
        {
            InitializeComponent();
            connection = new SqlConnection();
            conn_string = App.ConnectionStringSql;
        }

        DataSet ds;
        SqlDataAdapter da;

        private void ShowAllAuthorsBtn_Click(object sender, RoutedEventArgs e)
        {


            using (connection = new SqlConnection())
            {
                da = new SqlDataAdapter();
                connection.ConnectionString = conn_string;
                connection.Open();
                ds = new DataSet();

                da = new SqlDataAdapter("SELECT * FROM Authors", connection);


                da.Fill(ds);

                data_grid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            data_grid.ItemsSource = null;
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {

            SqlCommand cmd = new SqlCommand();


            using (connection = new SqlConnection())
            {
                da = new SqlDataAdapter();
                connection.ConnectionString = conn_string;
                connection.Open();
                ds = new DataSet();

                da = new SqlDataAdapter("SELECT * FROM Authors", connection);



                cmd = new SqlCommand(@"INSERT INTO Authors(Id,FirstName,LastName)
                                       VALUES(@id,@firstname,@lastname)",connection);


                cmd.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@id",
                    Value = int.Parse(id_txtb.Text)
                });


                cmd.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@firstname",
                    Value = firstname_txtb.Text
                });


                cmd.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@lastname",
                    Value = lastname_txtb.Text
                });




                da.InsertCommand = cmd;
                da.InsertCommand.ExecuteNonQuery();

                ds.Clear();

                da.Fill(ds);


                


            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();

            using (connection = new SqlConnection())
            {
                da = new SqlDataAdapter();
                ds = new DataSet();
                connection.ConnectionString = conn_string;
                connection.Open();

                da = new SqlDataAdapter("SELECT * FROM Authors", connection);


                cmd = new SqlCommand("UPDATE Authors SET FirstName = @firstname, LastName = @lastname WHERE Id = @id", connection);

                cmd.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@id",
                    Value = int.Parse(id_txtb.Text)
                });

                cmd.Parameters.Add(new SqlParameter()
                {
                    SqlDbType= SqlDbType.NVarChar,
                    ParameterName = "@firstname",
                    Value = firstname_txtb.Text
                });

                cmd.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@lastname",
                    Value = lastname_txtb.Text
                });

                da.UpdateCommand = cmd;
                da.UpdateCommand.ExecuteNonQuery();
                ds.Clear();
                da.Fill(ds);

            }

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();


            using (connection = new SqlConnection())
            {
                connection.ConnectionString = conn_string;
                connection.Open();
                da = new SqlDataAdapter();
                ds = new DataSet();

                da = new SqlDataAdapter("SELECT * FROM Authors", connection);

                cmd = new SqlCommand("DELETE Authors WHERE Id = @id",connection);

                cmd.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@id",
                    Value = int.Parse(id_txtb.Text)
                });

                da.DeleteCommand = cmd;
                da.DeleteCommand.ExecuteNonQuery();
                ds.Clear();
                da.Fill(ds);



            }
        }
    }
}
