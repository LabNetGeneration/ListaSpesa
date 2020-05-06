using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListaSpesa
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Prodotto> _prodotti;
        public MainPage()
        {
            InitializeComponent();
        }
        private int Quantita
        {
            get { return int.Parse(quantita.Text); }
            set { quantita.Text = value.ToString(); }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _prodotti = new ObservableCollection<Prodotto>(await App.Database.GetProdottoAsync());
            listView.ItemsSource = _prodotti;
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(prodotto.Text))
                    throw new Exception("Nome vuoto");
                var p = new Prodotto
                {
                    Nome = prodotto.Text,
                    Quantita = this.Quantita,
                    Unita = picker.SelectedItem.ToString()
                };
                await App.Database.SaveProdottoAsync(p);
                _prodotti.Add(p);
                prodotto.Text = string.Empty;
                picker.SelectedIndex = -1;
            }
            catch
            {
                await DisplayAlert("Errore", "Inserire nome e quantità validi", "OK");
            }
            finally
            {
                Quantita = 0;
            }
        }
        async void OnDelete(object sender, EventArgs e)
        {

            var p = (sender as MenuItem).CommandParameter as Prodotto;
            if (await DisplayAlert("Cancellare?", $"Cancellare {p}", "Sì", "No"))
            {
                await App.Database.DeleteProdottoAsync(p);
                _prodotti.Remove(p);
            }
        }
        void OnRemoveClicked(object sender, EventArgs e)
        {
            try
            {
                if (Quantita > 0)
                    Quantita--;
                else
                    Quantita = 0;
            }
            catch
            {
                Quantita = 0;
            }
        }
        void OnAddClicked(object sender, EventArgs e)
        {
            try
            {
                Quantita++;
            }
            catch
            {
                Quantita = 1;
            }
        }
    }
}
