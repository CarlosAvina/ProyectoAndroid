
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProyectoAndroid.DAL.DataAccessObjects;
using ProyectoAndroid.Models;

namespace ProyectoAndroid
{
    [Activity(Label = "MovimientosProductosActivity")]
    public class MovimientosProductosActivity : Activity
    {
        string _accion;
        Producto _productoActual = new Producto();

        //Elementos en la pantalla
        EditText _txtNombreProducto;
        EditText _txtCantidadInicial;
        EditText _txtCantidadModif;
        RadioGroup _rgrMovimiento;
        RadioButton _rbnEntrada;
        RadioButton _rbnSalida;
        Button _btnGuardar;
        //Button _btnCancelar;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MovimientosProductos);
            // Create your application here
            ReferenciarComponentes();
            _accion = Intent.GetStringExtra("accion");

            if (_accion == "editar")
            {

                var idProducto = Intent.GetIntExtra("idProducto", 0);
                CargarProducto(idProducto);
            }
            _btnGuardar.Click += delegate
            {
                if (_accion == "crear")
                {
                    GuardarProducto();
                    RegresarMain();
                }
                else
                {
                    ActualizarProducto();
                    //RegresarMain();
                  
                }
            };
            //_btnCancelar.Click += delegate
            //{
                //RegresarMain();
            //};
        }

        void ReferenciarComponentes()
        {
            _txtNombreProducto = FindViewById<EditText>(Resource.Id.TxtNombreProducto);
            _txtCantidadInicial = FindViewById<EditText>(Resource.Id.TxtCantidadInicial);
            _rgrMovimiento = FindViewById<RadioGroup>(Resource.Id.RgrMovimiento);
            _rbnEntrada = FindViewById<RadioButton>(Resource.Id.RbnEntrada);
            _rbnSalida = FindViewById<RadioButton>(Resource.Id.RbnSalida);
            _btnGuardar = FindViewById<Button>(Resource.Id.BtnGuardar);
            _txtCantidadModif = FindViewById<EditText>(Resource.Id.TxtCantidadModif);
            //_btnCancelar = FindViewById<Button>(Resource.Id.BtnCancelar);

            _txtNombreProducto.Enabled = false;
            _txtCantidadInicial.Enabled = false;
        }

        void RegresarMain()
        {
            var intent = new Intent(this, typeof(MenuActivity));
            StartActivity(intent);
        }

        async void ActualizarProducto()
        {
            try
            {
                if (ObtenerProductoCapturado())
                {
                    await ProductoDAO.Update(_productoActual);
                    Toast.MakeText(
                   this,
                   $"Producto actualizado correctamente",
                    ToastLength.Short).Show();
                    RegresarMain();
                }
                else{
                    Toast.MakeText(
                   this,
                   $"Esto no se puede realizar",
                    ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(
                   this,
                   $"Ha ocurrido un error en la aplicación. {ex.Message}",
                    ToastLength.Long);
            }
        }

        bool ObtenerProductoCapturado()
        {
            try
            {
                if((_rbnSalida.Checked == false && _rbnEntrada.Checked == false) || _txtCantidadModif == null) {
                    Toast.MakeText(
                        this,
                        $"Por favor selecciona una opción",
                        ToastLength.Long).Show();
                    return false;
                }

                if (_rbnEntrada.Checked)
                {
                    _productoActual.Cantidad = _productoActual.Cantidad + int.Parse(_txtCantidadModif.Text);
                    return true;

                }
                else if (_rbnSalida.Checked)
                {
                    _productoActual.Cantidad = _productoActual.Cantidad - int.Parse(_txtCantidadModif.Text);
                }
                if (_productoActual.Cantidad < 0)
                {
                    Toast.MakeText(this,
                    $"No se puede realizar esa operacion",
                    ToastLength.Long).Show();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(
                    this,
                    $"Ha ocurrido un error en la aplicación. {ex.Message}",
                    ToastLength.Long).Show();
            }
            return true;
        }

        async void GuardarProducto()
        {
            try
            {
                if (ObtenerProductoCapturado())
                {
                    await ProductoDAO.Create(_productoActual);
                    Toast.MakeText(
                    this,
                    "Persona creada correctamente!",
                    ToastLength.Short).Show();
                   
                }
                else
                {
                    Toast.MakeText(
                    this,
                    "No se puede ejecutar esa operacion",
                    ToastLength.Short).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(
                    this,
                    $"Ha ocurrido un error en la aplicación. {ex.Message}",
                    ToastLength.Long).Show();
            }
        }

        async void CargarProducto(int id)
        {
            _productoActual = await ProductoDAO.GetById(id);
            _txtNombreProducto.Text = _productoActual.Nombre;
            _txtCantidadInicial.Text = _productoActual.Cantidad.ToString();
        }



    }
}
