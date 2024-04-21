using ObjetosRemotos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cliente_remoto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ChannelServices.RegisterChannel(new TcpClientChannel(), false);
            ControlRemoto remoto = new ControlRemoto();
            dataGridView1.DataSource = remoto.consultarTodos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ControlRemoto remoto = new ControlRemoto();
            List<persona> lista = remoto.consultarTodos();

            string nombre = txtNombre.Text;
            int edad;
            string telefono = txtTelefono.Text;

            if (string.IsNullOrWhiteSpace(nombre) || !int.TryParse(txtEdad.Text, out edad) || string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Por favor, completa todos los campos.", "Campos Requeridos");
                return;
            }

            persona p = new persona()
            {
                nombre = nombre,
                edad = edad,
                telefono = telefono
            };

            bool existeTelefono = lista.Any(persona => persona.telefono == telefono);
            if (existeTelefono)
            {
                DialogResult dialogResult = MessageBox.Show("El teléfono ya existe. ¿Deseas modificar los datos existentes?", "Confirmación", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    remoto.agregar(p);
                    MessageBox.Show("Modificando persona", "Modificando");
                }
            }
            else
            {
                remoto.agregar(p);
                MessageBox.Show("Se agregó correctamente.", "Agregando");
            }

            lista = remoto.consultarTodos();
            dataGridView1.DataSource = lista;
            limpiar();
        }



        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (txtTelefono.Text == "")
            {
                MessageBox.Show("Por favor, llenar el campo telefono.", "Campos Requeridos");
                return;
            }
            ControlRemoto remoto = new ControlRemoto();
            persona p = new persona();
            p = remoto.buscar(txtTelefono.Text);

            if (p != null)
            {
                txtNombre.Text = p.nombre;
                txtEdad.Text = p.edad.ToString();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un numero valido.", "Campos Requeridos");
                limpiar();
            }
        }

        public void limpiar()
        {
            txtEdad.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
        }
    }
}
