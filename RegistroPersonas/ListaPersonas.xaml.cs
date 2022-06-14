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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegistroPersonas.Clases;
using RegistroPersonas.Conexion;
using static RegistroPersonas.Conexion.PersonaDAO;

namespace RegistroPersonas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ListaPersonas : Window
    {
        public ListaPersonas()
        {
            InitializeComponent();
            cargarPersonas();
        }

        private void clicRegistrarNuevo(object sender, RoutedEventArgs e)
        {
            FormularioPersona formularioPersona = new FormularioPersona("Registrar");
            formularioPersona.Show();
        }

        private void clicEditar(object sender, RoutedEventArgs e)
        {
            FormularioPersona formularioPersona = new FormularioPersona("Editar");
            formularioPersona.Show();
        }

        private void clicEliminar(object sender, RoutedEventArgs e)
        {
            var confirmacionEliminacion = MessageBox.Show("¿Realmente quiere eliminar el registro?",
                "Confirmación", MessageBoxButton.YesNo);

            if (confirmacionEliminacion == MessageBoxResult.Yes)
            {
                if (dgPersonas.SelectedIndex > -1)
                {
                    bool resultado = false;
                    resultado = EliminarPersona(((Persona)dgPersonas.SelectedValue).Id);

                    if (resultado)
                    {

                        MessageBox.Show("Eliminado Correctamente");

                    }
                    else
                    {
                        MessageBox.Show("Registro eliminado correctamente");
                    }
                    
                }
                else MessageBox.Show("Debe seleccionar un celda valida para eliminar");
            }
            else
            {
                // If 'No', do something here. 
            }
        }

        private void cargarPersonas()
        {
            List<Persona> personas = new List<Persona>();
            personas = PersonaDAO.RecuperarPersonas();
            if (personas != null)
            {
                dgPersonas.ItemsSource = personas;
            }
            else
            {
                MessageBox.Show("No hay personas registradas", "Sin personas");
            }
        }
    }
}
