using Firebase.Database;
using Firebase.Database.Query;
using GerenciadorEventos.Domain.Model;
using GerenciadorEventos.Domain.Service;
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
    public partial class List : ContentPage
    {

        public List<Event> events;
        private FirebaseClient _firebase = FirebaseService.getConnection();

        public List()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            events = (await _firebase.Child("Events").OnceAsync<Event>())
                .Select(item => new Event
                {   
                    EventId = item.Object.EventId,
                    EventName = item.Object.EventName,
                    DateTime = item.Object.DateTime,
                    Local = item.Object.Local,
                    Contact = item.Object.Contact,
                    Organization = item.Object.Organization,
                    Link = item.Object.Link
                }).ToList();

            EventsList.ItemsSource = events;
        }

        private void OnEdit(object sender, EventArgs e)
        {

        }

        private async void OnDelete(object sender, EventArgs e)
        {
            try
            { 
                var mi = ((MenuItem)sender);
                Event eventToRemove = (Event)mi.CommandParameter;

                var toDelete = (await _firebase.Child("Events").OnceAsync<Event>())
                    .Where(ev =>
                        ev.Object.EventId.Equals(eventToRemove.EventId))
                    .FirstOrDefault();

                await _firebase.Child("Events")
                    .Child(toDelete.Key).DeleteAsync();

                await DisplayAlert("Remover", "O Evento(" + eventToRemove.EventName + ") foi removido com sucesso", "OK");

                OnAppearing();
            
            }
            catch
            {
                await DisplayAlert("Remover", "Não foi possível remover o evento", "OK");
            }
        }
    }
}