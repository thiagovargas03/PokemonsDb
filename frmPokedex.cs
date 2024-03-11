using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace PokemonsDB
{
    public partial class Pokedex : Form
    {
        private List<Pokemon> listaPokemon;
        public Pokedex()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                listaPokemon = negocio.listar();
                dvgPokemons.DataSource = listaPokemon;
                dvgPokemons.Columns["UrlImagen"].Visible = false;
                dvgPokemons.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }



        }

        private void dvgPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dvgPokemons.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarPokemon frmAgregarPokemon = new frmAgregarPokemon();
            frmAgregarPokemon.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
            seleccionado = (Pokemon)dvgPokemons.CurrentRow.DataBoundItem;


            frmAgregarPokemon frmModificarPokemon = new frmAgregarPokemon(seleccionado);
            frmModificarPokemon.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }


        private void eliminar(bool logico = false)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("Se va a eliminar el pokemon seleccionado", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Pokemon)dvgPokemons.CurrentRow.DataBoundItem;

                    if (logico)
                        negocio.eliminarLogico(seleccionado.Id);
                    else
                        negocio.eliminar(seleccionado.Id);


                    cargar();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
