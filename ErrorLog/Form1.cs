using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;


namespace ErrorLog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection Cnn = new MySqlConnection();
        List<String> lista = new List<string>();

        public void LogFile(string error)

        {

            StreamWriter log;

            if (!File.Exists("logfile.txt"))

            {

                log = new StreamWriter("logfile.txt");

            }

            else

            {

                log = File.AppendText("logfile.txt");

            }

            // Write to the file:

            log.WriteLine("Data Time:" + DateTime.Now);

            log.WriteLine("Exception Name:" + error);





            log.Close();

        }

        private void guardarPalabra()
        {
            try
            {
                Cnn.ConnectionString = "Server = localhost; Database = conjuntosYo; User Id = root; Password =; ";
            }
            catch (Exception e)
            {
               LogFile(e.ToString ());
            }
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarPalabra();
        }
    }

    public static class ExceptionHelper

    {

        public static int LineNumber(this Exception e)

        {

            int linenum = 0;

            try

            {

                linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":line") + 5));

            }

            catch

            {

                //Stack trace is not available!

            }

            return linenum;

        }

    }
}
