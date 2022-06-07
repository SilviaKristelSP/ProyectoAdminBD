﻿using System;
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
        public FormularioPersona(String tipoOperacion)
        {
            InitializeComponent();
            if (tipoOperacion.Equals("Registrar"))
            {
                lbTitulo.Content = "Registro Persona";
            }else if (tipoOperacion.Equals("Editar"))
            {
                lbTitulo.Content = "Edición Persona";
            }
        }

        private void clicGuardar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
