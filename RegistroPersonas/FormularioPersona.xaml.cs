using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace RegistroPersonas
{
    /// <summary>
    /// Lógica de interacción para RegistroPersona.xaml
    /// </summary>
    public partial class FormularioPersona : Window
    {
        private String operacionARealizar;

        public FormularioPersona(String tipoOperacion)
        {
            InitializeComponent();
            operacionARealizar = tipoOperacion;
            if (tipoOperacion.Equals("Registrar"))
            {
                lbTitulo.Content = "Registro Persona";
            }else if (tipoOperacion.Equals("Editar"))
            {
                lbTitulo.Content = "Edición Persona";
            }
            //FN = Nombre; MN: Paterno; LN: Materno
        }

        private void clicGuardar(object sender, RoutedEventArgs e)
        {
            if (operacionARealizar.Equals("Registrar"))
            {
                realizarRegistro();
            }
            else if (operacionARealizar.Equals("Editar"))
            {
                realizarEdicion();
            }
        }

        private void realizarRegistro()
        {

        }

        private void realizarEdicion()
        {

        }

        private bool verificarCorreo()
        {
            if(tbCorreo.Text != "")
            {
                return true;
            }
            else
            {
                return false;
                MessageBox.Show("Escriba su correo eléctronico", "Error en los datos");
            }
        }

        private bool verificarTarjeta()
        {
            if (cbTipoTarjeta.SelectedIndex >= 0 && cbMes.SelectedIndex >= 0 && tbNumeroTarjeta.Text != "" && tbAnio.Text != "")
            {
                if(int.Parse(tbAnio.Text) >= 2022)
                {
                    return true;
                }
                else
                {
                    return false;
                    MessageBox.Show("El año de expiración debe ser mayor o igual al año actual", "Error en los datos");
                } 
            }
            else
            {
                return false;
                MessageBox.Show("Llene todos los campos de la tarjeta de crédito", "Error en los datos");
            }
        }

        private bool verificarTelefono()
        {
            if (cbTipoTelefono.SelectedIndex >= 0 && tbNumero.Text != "")
            {
                return true;
            }
            else
            {
                return false;
                MessageBox.Show("Llene todos los campos del número de télefono", "Error en los datos");
            }
        }

        private bool verificarPersona()
        {
            if (tbNombre.Text != "" && tbApellidoMaterno.Text != "")
            {
                return true;

            }
            else
            {
                return false;
                MessageBox.Show("Su Nombre y Apellido Materno son necesarios para el registro", "Error en los datos");
            }
        }
    }
}
