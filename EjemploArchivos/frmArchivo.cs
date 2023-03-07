using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjemploArchivos
{
    public partial class frmArchivo : Form
    {
        public frmArchivo()
        {
            InitializeComponent();
        }

        private void frmArchivo_Load(object sender, EventArgs e)
        {

        }


        //se invoca cuando el usuario oprime una tecla
        private void txtEntrada_KeyDown(object sender, KeyEventArgs e)
        {
            //determina si el usuario oprimió la tecla intro
            if (e.KeyCode == Keys.Enter)
            {
                string nombreArchivo; // nombre del archivo o directorio

                // obtiene el archivo o directorio especificado por el usuario
                nombreArchivo = txtEntrada.Text;

                // determina si nombreArchivo es un archivo 
                if (File.Exists(nombreArchivo))
                {
                    // obtiene la fecha de creacion del archivo,
                    //su fecha de modificacion, etc
                    txtSalida.Text = obtenerInformacion(nombreArchivo);

                    try
                    {
                        //obtiene lector y contenido del archivo
                        StreamReader sr = new StreamReader(nombreArchivo);
                        txtSalida.Text += sr.ReadToEnd();
                    }
                    // maneja excepcion si StreanReader no está disponible
                    catch (IOException)
                    {
                        MessageBox.Show("Error al leer el archivo", "Error de archivo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }

                //determina si nombreArchivo es un directorio
                else if (Directory.Exists(nombreArchivo))
                {
                    string[] listaDirectorios; //arreglo para los directorios

                    // obtiene la fecha de creacion del archivo,
                    //su fecha de modificacion, etc
                    txtSalida.Text = obtenerInformacion(nombreArchivo);

                    //obtiene la lista de archivos/directorios del directorio especificado
                    listaDirectorios = Directory.GetDirectories(nombreArchivo);

                    txtSalida.Text += "\r\n\r\nContenido del directorio: \r\n";

                    //imprime en pantalla el contenido de listaDirectorios

                    for(int i = 0; i < listaDirectorios.Length; i++)
                    {
                        txtSalida.Text+= listaDirectorios[i] + "\r\n";
                    }

                }
                else
                {
                    // notifica al usuario que no exisre el directorio o archivo

                    MessageBox.Show(txtEntrada.Text +
                        "no existe", "Error de archivo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // obtiene informacion sobre el archivo o directorio
        private string obtenerInformacion(string nombreArchivo)
        {
            string informacion;

            //imprime mensaje indicando que existe el archivo o directorio
            informacion = nombreArchivo + "existe\r\n\r\n";

            //imprime en pantalla la fecha y hora de creacioN del archivo o directorio
            informacion += "Creacion: " +
                File.GetCreationTime(nombreArchivo) + "\r\n";

            //imprime en pantalla la fecha y hora de modificacion del archivo o directorio
            informacion += "Ultima modificacion: " +
                File.GetLastWriteTime(nombreArchivo) + "\r\n";

            //imprime en pantalla la fecha y hora de ultimo acceso del archivo o directorio
            informacion += "Ultimo acceso: " +
                File.GetLastAccessTime(nombreArchivo) + "\r\n" + "\r\n";



            return informacion;
        }
    }
}
