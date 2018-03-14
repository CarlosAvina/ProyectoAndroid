
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
    [Activity(Label = "AddUserActivity")]
    public class AddUserActivity : Activity
    {
        EditText _txtUsuario;
        EditText _txtPassword;
        EditText _txtComprobarPassword;
        Button _btnRegistrar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AddUser);

            _txtUsuario = FindViewById<EditText>(Resource.Id.txtUser);
            _txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            _txtComprobarPassword = FindViewById<EditText>(Resource.Id.txtComprobarPassword);
            _btnRegistrar = FindViewById<Button>(Resource.Id.btnAddUser);

            _btnRegistrar.Click += delegate {

                Usuario usuario = new Usuario();

                try{
                    usuario.Nickname = _txtUsuario.Text;

                    if(ValidarUsuario(_txtPassword.Text, _txtComprobarPassword.Text) &&
                       _txtUsuario.Text != "" &&
                       _txtPassword.Text != "" &&
                       _txtComprobarPassword.Text != ""){
                        
                        usuario.Password = _txtPassword.Text;

                        GuardaUsuario(usuario);

                        Intent intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                    }
                    else {
                        Toast.MakeText(
                            this,
                            $"Completa los todos los campos",
                            ToastLength.Short).Show();
                    }
                }catch(Exception ex){
                    Toast.MakeText(
                        this,
                        $"Ha ocurrido un error... {ex.Message}",
                        ToastLength.Short).Show();
                }
            };
        }

        public bool ValidarUsuario(string pass1, string pass2){
            if (pass1 == pass2) return true;
            else return false;
        }

        public async void GuardaUsuario(Usuario usuario)
        {
            try
            {
                await UsuarioDAO.Create(usuario);

                Toast.MakeText(
                    this,
                    "Usuario creado correctamente!",
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
    }
}
