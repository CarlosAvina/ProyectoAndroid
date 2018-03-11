using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using ProyectoAndroid.DAL;

namespace ProyectoAndroid
{
    [Activity(Label = "ProyectoAndroid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText _txtUsername;
        EditText _txtPassword;
        Button _btnSingIn;
        Button _btnSingUp;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            await BaseDatos.CrearBaseDatos();

            _txtUsername = FindViewById<EditText>(Resource.Id.txtUser);
            _txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            _btnSingIn = FindViewById<Button>(Resource.Id.btnSignIn);
            _btnSingUp = FindViewById<Button>(Resource.Id.btnSingUp);

            _btnSingIn.Click += delegate {
                Intent intent = new Intent(this, typeof(CreateProductActivity));
                StartActivity(intent);
            };

            _btnSingUp.Click += delegate {
                
            };
        }
    }
}

