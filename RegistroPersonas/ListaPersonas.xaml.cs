using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            FormularioPersona formularioPersona = new FormularioPersona("Registrar", null);
            formularioPersona.Show();
            this.Close();
        }

        private void clicEditar(object sender, RoutedEventArgs e)
        {
            if (dgPersonas.SelectedIndex > -1)
            {
                Persona personaSeleccionada = PersonaDAO.RecuperarPersonaCompleta(((Persona)dgPersonas.SelectedValue).Id);
                if (personaSeleccionada != null)
                {
                    FormularioPersona formularioPersona = new FormularioPersona("Editar", personaSeleccionada);
                    formularioPersona.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al recuperarDatos Persona");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un celda valida para editar");
            }
            
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
                    resultado = PersonaDAO.EliminarPersona(((Persona)dgPersonas.SelectedValue).Id);
                    if (resultado)
                    {
                        MessageBox.Show("Eliminado Correctamente");
                        
                        MessageBox.Show("La lista se está actualizando, esto puede tradar unos segundos...", "Espere");
                        btnEditar.IsEnabled = false;
                        btnEliminar.IsEnabled = false;
                        btnRegistrar.IsEnabled = false;
                        Thread.Sleep(5000);

                        cargarPersonas();
                        MessageBox.Show("La lista se actualizo con éxito...");
                        btnEditar.IsEnabled = true;
                        btnEliminar.IsEnabled = true;
                        btnRegistrar.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Registro eliminado correctamente");
                    }

                }
                else MessageBox.Show("Debe seleccionar un celda valida para eliminar");
            }
        }

        private void cargarPersonas()
        {
            List<Persona> personas = new List<Persona>();
            personas = PersonaDAO.RecuperarPersonas();
            if (personas != null)
            {
                dgPersonas.ItemsSource = null;
                dgPersonas.ItemsSource = personas;
            }
            else
            {
                MessageBox.Show("No hay personas registradas", "Sin personas");
            }
        }

        private void clicRecargar(object sender, RoutedEventArgs e)
        {
            cargarPersonas();
        }
    }
}
