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
            guardarPalabras();
            mostrar();
        }

        MySqlConnection Cnn = new MySqlConnection();
        List<String> lista = new List<string>();
        MySqlCommand myCmd;
        MySqlDataReader myReaader;
       

        private void guardarPalabras()
        {
            try
            {
                Cnn.Open();
                myCmd = new MySqlCommand("select * from palabras");
                myCmd.Connection = Cnn;
                myReaader = myCmd.ExecuteReader();
                while (myReaader.Read())
                {
                    //Sigue leyendo la tabla
                    myReaader.Read();
                    //Busca una la columna la convierte en string y la almacena en una variable
                    lista.Add(myReaader["palabra"].ToString());
                    //La ruta le cambiamos la direccion de las diagonales y le ponemos dos en vez de uno             
                }
                Cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al leer la tabla. :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string filePath = @"Error.txt";
                using (StreamWriter log = new StreamWriter(filePath, true))
                {
                    log.WriteLine("Data Time:" + DateTime.Now);
                    log.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine);
                    log.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------  :(" + Environment.NewLine);
                }
            }
        }
        
        private void mostrar()
        {
            foreach (string pala in lista)
            {
                label1.Text += pala + "\n";
            }
        }
    }
}
