using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using ProyectoAndroid.DAL;
using ProyectoAndroid.Models;
using System.Collections.Generic;
using ProyectoAndroid.DAL.DataAccessObjects;
using System.Threading.Tasks;
using System;

namespace ProyectoAndroid
{
    [Activity(Label = "ProyectoAndroid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText _txtUsername;
        EditText _txtPassword;
        Button _btnSingIn;
        Button _btnSingUp;

        List<Usuario> listUsuarios = new List<Usuario>();

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
                Usuario usuario = new Usuario();
                usuario.Nickname = _txtUsername.Text;
                usuario.Password = _txtPassword.Text;

                GetUsuario(usuario);

                if(ValidarLogin(usuario)) {
                    Intent intent = new Intent(this, typeof(MenuActivity));
                    StartActivity(intent);
                }
                else {
                    Toast.MakeText(
                        this,
                        "Usuario/Contraseña incorrectos",
                        ToastLength.Short).Show();
                }
            };

            _btnSingUp.Click += delegate {
                Intent intent = new Intent(this, typeof(AddUserActivity));
                StartActivity(intent);
            };
        }

        public bool ValidarLogin(Usuario usuario) {
            foreach(var item in listUsuarios) {
                if(usuario.Nickname == item.Nickname && usuario.Password == item.Password) {
                    return true;
                }
            }
            return false;
        }

        public async void GetUsuario(Usuario usuario)
        {
            try
            {
                listUsuarios = await UsuarioDAO.GetAll();
            }
            catch (Exception ex)
            {
                Toast.MakeText(
                    this,
                    $"Ha ocurrido un error... {ex.Message}",
                    ToastLength.Short).Show();
            }
        }
    }
}

