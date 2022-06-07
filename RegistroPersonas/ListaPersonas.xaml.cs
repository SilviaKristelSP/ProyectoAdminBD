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
                MessageBox.Show("El registro de eliminó exitosamente", "Borrado exitoso");
            }
            else
            {
                // If 'No', do something here. 
            }
        }
    }
}
