using Firebase.Database;
using Firebase.Database.Query;
using GerenciadorEventos.Domain.Model;
using GerenciadorEventos.Domain.Service;
using GerenciadorEventos.Domain.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorEventos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create : ContentPage
    {
        private FirebaseClient _firebase = FirebaseService.getConnection();

        public Create()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string errors = "";
            ErrorsValue.IsVisible = false;
            try
            {
                string date = DateValueTxt.Text;
                string time = TimeValueTxt.Text;

                date = date.Split(' ')[0];
                DateTime dateTime = DateTime.Parse(date + " " + time);
                Event newEvent = new Event()
                {
                    EventName = EventNameValue.Text,
                    DateTime = dateTime,
                    Contact = ContactValue.Text,
                    Link = LinkValue.Text,
                    Local = LocalValue.Text,
                    Organization = OrganizationValue.Text
                };

                EventValidator validator = new EventValidator()
                {
                    EventToValidate = newEvent
                };

                if (validator.IsValid())
                {
                    _ = await _firebase.Child("Events").PostAsync(newEvent);

                    await DisplayAlert("Sucesso", "Evento registrado com sucesso", "Ok");

                    EventNameValue.Text = "";
                    LinkValue.Text = "";
                    LocalValue.Text = "";
                    ContactValue.Text = "";
                    OrganizationValue.Text = "";
                }
                else
                {
                    validator.Erros.ForEach(error => errors += "- " + error + '\n');
                    ErrorsValue.Text = errors;
                    ErrorsValue.IsVisible = true;
                }
            }
            catch
            {
                await DisplayAlert("Erro", "Evento não registrado", "Ok");
            }
        }
    }
}