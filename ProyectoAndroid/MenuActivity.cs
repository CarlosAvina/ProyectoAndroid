
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
using ProyectoAndroid.DAL;
using ProyectoAndroid.DAL.DataAccessObjects;
using ProyectoAndroid.Models;

namespace ProyectoAndroid
{
    [Activity(Label = "MenuActivity")]
    public class MenuActivity : Activity
    {
        List<Producto> _productos;

        Button _btnAgregar;
        ListView _lvwProductos;

		public override void OnBackPressed()
		{
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
		}

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Menu);

            _btnAgregar = FindViewById<Button>(Resource.Id.btnAgregar);
            _lvwProductos = FindViewById<ListView>(Resource.Id.lvwProductos);

            CargarProductos();

            _btnAgregar.Click += delegate
            {
                var intent = new Intent(this, typeof(CreateProductActivity));
                intent.PutExtra("accion", "crear");
                StartActivity(intent);
            };
        }

        public async void CargarProductos()
        {
            _productos = await ProductoDAO.GetAll();

            var itemsLista = _productos
                .Select(p => $"{p.Nombre}" + " - " + $"{p.Cantidad} {p.Unidad}s")
                .ToArray();

            var adaptador = new ArrayAdapter<string>(
                this, Android.Resource.Layout.SimpleListItem1, itemsLista);

            _lvwProductos.Adapter = adaptador;

            _lvwProductos.ItemClick += (sender, e) =>
            {
                var posicion = e.Position;
                var productoSeleccionado = _productos[posicion];
                var intent = new Intent(this, typeof(MovimientosProductosActivity));

                intent.PutExtra("accion", "editar");
                intent.PutExtra("idProducto", productoSeleccionado.Id);
                StartActivity(intent);
            };
        }
    }
}
