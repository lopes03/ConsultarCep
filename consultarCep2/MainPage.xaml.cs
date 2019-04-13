using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace consultarCep2 {
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage {
        static String url = "https://viacep.com.br/ws/{0}/json";
        public MainPage() {
            InitializeComponent();
            inicialization();
            button.Clicked += printEndereco;
        }

        private void inicialization() {
           Endereco endereco = ObterEndereco("61976665");
        }
        private void printEndereco(Object sender, EventArgs args) {
            String buttonText = input.Text.Trim().Replace("-", "");
            if (isCepValid(buttonText)) {
                Endereco endereco = ObterEndereco(buttonText);
                if (endereco.cep != null) {
                    label.TextColor = Color.White;
                    label.Text = String.Format("Estado: {0}\nCidade: {1}\nBairro: {2}\nRua: {3}",
                        endereco.uf, endereco.localidade, endereco.bairro, endereco.logradouro);
                } else {
                    label.TextColor = Color.Red;
                    label.Text = "CEP not found";
                }
            } else {
                label.Text = "";
                DisplayAlert("Error", "Invalid CEP", "OK");
            }
        }

        public static Endereco ObterEndereco(String cep) {
            String fullUrl = String.Format(url, cep);
            WebClient webClient = new WebClient();
            return JsonConvert.DeserializeObject<Endereco>(webClient.DownloadString(fullUrl));      
        }

        private static bool isCepValid(String cep) {
            if (cep.Length == 8 && int.TryParse(cep, out int n)) {
                return true;
            } else
                return false;
        }
    }
}
