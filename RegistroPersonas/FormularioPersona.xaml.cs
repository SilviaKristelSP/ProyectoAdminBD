using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RegistroPersonas.Clases;
using RegistroPersonas.Conexion;

namespace RegistroPersonas
{
    /// <summary>
    /// Lógica de interacción para RegistroPersona.xaml
    /// </summary>
    public partial class FormularioPersona : Window
    {
        private String operacionARealizar;
        Persona personaAEditar;

        public FormularioPersona(String tipoOperacion, Persona editable)
        {
            InitializeComponent();
            operacionARealizar = tipoOperacion;
            personaAEditar = editable;

            if (tipoOperacion.Equals("Registrar"))
            {
                lbTitulo.Content = "Registro Persona";
            } else if (tipoOperacion.Equals("Editar"))
            {
                lbTitulo.Content = "Edición Persona";
                tbNombre.Text = personaAEditar.FirstName;
                tbApellidoMaterno.Text = personaAEditar.LastName;
                tbApellidoPaterno.Text = personaAEditar.MiddleName;
                tbCorreo.Text = personaAEditar.EmailAddress;
                tbNumero.Text = personaAEditar.PhoneNumber;
                tbNumeroTarjeta.Text = personaAEditar.CardNumber;
                if (personaAEditar.IdCreditCard > 0)
                {
                    tbAnio.Text = personaAEditar.ExpYear.ToString();
                    cbTipoTarjeta.Text = personaAEditar.CardType;
                }
                if (!personaAEditar.Title.Equals(""))
                {
                    cbTitulo.Text = personaAEditar.Title;
                }
                if (personaAEditar.PhoneNumberType > 0)
                {
                    switch (personaAEditar.PhoneNumberType)
                    {
                        case 1:
                            cbTipoTelefono.Text = "Cell";
                            break;
                        case 2:
                            cbTipoTelefono.Text = "Home";
                            break;
                        case 3:
                            cbTipoTelefono.Text = "Work";
                            break;
                        default:
                            cbTipoTelefono.Text = null;
                            break;
                    }
                }
                if(personaAEditar.ExpMonth > 0)
                {
                    cbMes.Text = personaAEditar.ExpMonth.ToString();
                }

            }
        }

        private void clicGuardar(object sender, RoutedEventArgs e)
        {
            if (operacionARealizar.Equals("Registrar"))
            {
                if (realizarRegistro())
                {
                    ListaPersonas listaPersonas = new ListaPersonas();
                    listaPersonas.Show();
                    this.Close();
                }
                
            }
            else if (operacionARealizar.Equals("Editar"))
            {
                if (realizarEdicion())
                {
                    ListaPersonas listaPersonas = new ListaPersonas();
                    listaPersonas.Show();
                    this.Close();
                }
            }
        }

        private bool realizarRegistro()
        {
            bool respuesta = false;
            if (verificarPersona() && verificarCorreo() && verificarTarjeta() && verificarTelefono())
            {
                int id = obtenerBusinessEntityID(crearPersona());
                if(id > 0)
                {
                    registrarEmail(crearCorreo(), id);
                    registrarTarjeta(crearTarjeta(), id);
                    registrarTelefono(crearTelefono(), id);
                    MessageBox.Show("Registro realizado exitosamente");
                }
                else
                {
                    MessageBox.Show("Hubo un error durante el registro", "Error");
                }
                respuesta = true;
            }
            return respuesta;
        }

        private bool realizarEdicion()
        {
            bool respuesta = false;
            if (verificarPersona() && verificarCorreo() && verificarTarjeta() && verificarTelefono())
            {
                int id = personaAEditar.Id;
                if (id > 0)
                {
                    Email correoAEditar = crearCorreo();
                    correoAEditar.EmailID = personaAEditar.IdEmailAddress;
                    CreditCard tarjetaAEditar = crearTarjeta();
                    tarjetaAEditar.Id = personaAEditar.IdCreditCard;
                    TelefonoPersona telefonoAEditar = crearTelefono();
                    telefonoAEditar.BusinessEntityID = personaAEditar.Id;

                    actualizarDatosPersona();
                    
                    PersonaDAO.EditarPersona(personaAEditar);
                    EmailDAO.EditarEmail(correoAEditar);
                    TelefonoPersonaDAO.EditarTelefono(telefonoAEditar);

                    if (tarjetaAEditar.Id > 0)
                    {
                        CreditCardDAO.EditarCreditCard(tarjetaAEditar);
                    }
                    else
                    {
                        tarjetaAEditar.BusinessEntityID = personaAEditar.Id;
                        CreditCardDAO.RegistrarCreditCard(tarjetaAEditar);
                    }

                    MessageBox.Show("Edición realizada exitosamente");
                    respuesta = true;
                }
                else
                {
                    MessageBox.Show("Hubo un error durante el registro", "Error");
                }

            }
            return respuesta;
        }

        private void actualizarDatosPersona()
        {
            personaAEditar.Title = ((cbTitulo.SelectedIndex < 0) ? null : cbTitulo.Text);
            personaAEditar.FirstName = tbNombre.Text;
            personaAEditar.LastName = tbApellidoMaterno.Text;
            personaAEditar.MiddleName = ((tbApellidoPaterno.Text == "") ? null : tbApellidoPaterno.Text);
        }
        
        //BUSINESSENTITYID DEL REGISTRO
        private int obtenerBusinessEntityID(Persona persona)
        {
            int id = PersonaDAO.RegistrarPersona(persona);
            if ( id > 0)
            {
                return id;
            }
            else
            {
                return id;
            }
        }

        private bool registrarTarjeta(CreditCard tarjeta, int businessEntityID)
        {
            tarjeta.BusinessEntityID = businessEntityID;
            return CreditCardDAO.RegistrarCreditCard(tarjeta);
        }

        private bool registrarEmail(Email correo, int businessEntityID)
        {
            correo.BusinessEntityID = businessEntityID;
            return EmailDAO.RegistrarEmail(correo);
        }

        private bool registrarTelefono(TelefonoPersona telefono, int businessEntityID)
        {
            telefono.BusinessEntityID = businessEntityID;
            return TelefonoPersonaDAO.RegistrarTelefono(telefono);
        }

        //CREACIÓN AUXILIAR DE OBJETOS
        private Persona crearPersona()
        {
            Persona persona = new Persona();
            persona.Title = ((cbTitulo.SelectedIndex < 0) ? null : cbTitulo.Text);
            persona.FirstName = tbNombre.Text;
            persona.LastName = tbApellidoMaterno.Text;
            persona.MiddleName = ((tbApellidoPaterno.Text == "") ? null : tbApellidoPaterno.Text);
            return persona;
        }

        private CreditCard crearTarjeta()
        {
            CreditCard tarjeta = new CreditCard();
            tarjeta.CardType = cbTipoTarjeta.Text;
            tarjeta.CardNumber = tbNumeroTarjeta.Text;
            tarjeta.ExpYear = Int32.Parse(tbAnio.Text);
            String mesExp = cbMes.Text;
            tarjeta.ExpMonth = Int32.Parse(mesExp);
            return tarjeta;
        }

        private Email crearCorreo()
        {
            Email correo = new Email();
            correo.EmailAddress = tbCorreo.Text;
            return correo;
        }

        private TelefonoPersona crearTelefono()
        {
            TelefonoPersona telefono = new TelefonoPersona();
            if(cbTipoTelefono.SelectedIndex == 0)
            {
                telefono.PhoneType = 1;
            }
            else if (cbTipoTelefono.SelectedIndex == 1)
            {
                telefono.PhoneType = 2;
            }
            else if(cbTipoTelefono.SelectedIndex == 2)
            {
                telefono.PhoneType = 3;
            }
            telefono.PhoneNumber = tbNumero.Text;

            return telefono;
        }

        //VERIFICACIONES FORMULARIO
        private bool verificarCorreo()
        {
            if(tbCorreo.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Escriba su correo eléctronico", "Error en los datos");
                return false;
            }
        }

        private bool verificarTarjeta()
        {
            bool respuesta = false;
            if (cbTipoTarjeta.SelectedIndex >= 0 && cbMes.SelectedIndex >= 0 && tbNumeroTarjeta.Text != "" && tbAnio.Text != "")
            {
                try
                {
                    if (int.Parse(tbAnio.Text) >= 2022)
                    {
                        respuesta = true;
                    }
                    else
                    {
                        MessageBox.Show("El año de expiración debe ser mayor o igual al año actual", "Error en los datos");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("El año de expiración debe ser numerico", "Error en los datos");
                }  
            }
            else if (cbTipoTarjeta.SelectedIndex >= 0 || cbMes.SelectedIndex >= 0 || tbNumeroTarjeta.Text != "" || tbAnio.Text != "")
            {
                MessageBox.Show("Llene todos los campos de la tarjeta de crédito", "Error en los datos");
            }
            return respuesta;
        }

        private bool verificarTelefono()
        {
            if (cbTipoTelefono.SelectedIndex >= 0 && tbNumero.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Llene todos los campos del número de télefono", "Error en los datos");
                return false;
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
                MessageBox.Show("Su Nombre y Apellido Materno son necesarios para el registro", "Error en los datos");
                return false;
            }
        }

        private void clicRegresar(object sender, RoutedEventArgs e)
        {
            ListaPersonas listaPersonas = new ListaPersonas();
            listaPersonas.Show();
            this.Close();
        }
    }
}
