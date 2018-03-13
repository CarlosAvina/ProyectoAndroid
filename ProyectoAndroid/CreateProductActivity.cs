
using System;
using Android.Content;
using Android.App;
using Android.OS;
using Android.Widget;

using ProyectoAndroid.Models;
using ProyectoAndroid.DAL.DataAccessObjects;
using Android.Runtime;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Util;

namespace ProyectoAndroid
{
    [Activity(Label = "CreateProductActivity")]
    public class CreateProductActivity : Activity
    {
        EditText _txtNombre;
        Spinner _spnCategoria;
        RadioGroup _rdgUnidad;
        RadioButton _rdbPieza;
        RadioButton _rdbKilo;
        EditText _txtCantidad;
        Button _btnAgregar;

        List<Producto> listProductos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateProduct);
            // Create your application here

            _txtNombre = FindViewById<EditText>(Resource.Id.txtNombre);
            _spnCategoria = FindViewById<Spinner>(Resource.Id.spinnerCategoria);
            _rdgUnidad = FindViewById<RadioGroup>(Resource.Id.rdgUnidad);
            _rdbPieza = FindViewById<RadioButton>(Resource.Id.rdbPieza);
            _rdbKilo = FindViewById<RadioButton>(Resource.Id.rdbKilo);
            _txtCantidad = FindViewById<EditText>(Resource.Id.txtCantidad);
            _btnAgregar = FindViewById<Button>(Resource.Id.btnAgregarProducto);

            llenarSpinner();

            _btnAgregar.Click += delegate {

                Producto producto = new Producto();

                try
                {
                    producto.Nombre = _txtNombre.Text;
                    producto.Categoria = _spnCategoria.SelectedItem.ToString();
                    producto.Unidad = textoRadioButton();
                    producto.Cantidad = int.Parse(_txtCantidad.Text);

                    GuardaProducto(producto);
                    vaciarControles();


                }
                catch(Exception ex)
                {
                    Toast.MakeText(
                        this,
                        $"No has completado todos los campos correctamente. {ex.Message}",
                        ToastLength.Short).Show();
                }

            };
        }

        public void llenarSpinner(){
            ArrayAdapter adaptador = ArrayAdapter.CreateFromResource(
                this, 
                Resource.Array.producto_categorias, 
                Android.Resource.Layout.SimpleSpinnerItem);

            adaptador.SetDropDownViewResource(
                Android.Resource.Layout.SimpleSpinnerDropDownItem);

            _spnCategoria.Adapter = adaptador;
        }

        public string textoRadioButton(){
            var id = _rdgUnidad.CheckedRadioButtonId;
            var itemSeleccionado = FindViewById<RadioButton>(id).Text;
            return itemSeleccionado;
        }

        public void vaciarControles(){
            _txtNombre.Text = "";
            _rdbPieza.Checked = false;
            _rdbKilo.Checked = false;
            _txtCantidad.Text = "";
        }

        public async void GuardaProducto(Producto producto)
        {
            try
            {
                await ProductoDAO.Create(producto);

                Toast.MakeText(
                    this,
                    "Producto creado correctamente!",
                    ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(
                    this,
                    $"Ha ocurrido un error... {ex.Message}",
                    ToastLength.Short).Show();
            }
        }

        public async void consultaTodo(){
            listProductos = await ProductoDAO.GetAll();
        }

    }
}
