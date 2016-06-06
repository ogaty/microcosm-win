using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace microcosm.DB
{
    /// <summary>
    /// GoogleSearchWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class GoogleSearchWindow : Window
    {
        public UserEditWindow editWindow;
        public GoogleSearchWindowViewModel searchResultList { get; set; }

        public GoogleSearchWindow(UserEditWindow editWindow, string searchStr)
        {
            this.editWindow = editWindow;
            InitializeComponent();

            searchPlace.Text = searchStr;
            searchResultList = new GoogleSearchWindowViewModel();
            searchResultList.resultList = new List<AddrSearchResult>();
            resultBox.DataContext = searchResultList;
        }

        private async void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            HttpClient http = new HttpClient();
            string url = "http://maps.google.com/maps/api/geocode/json?address=" + searchPlace.Text + "&language=ja";
            var response = await http.GetAsync(url);

            var contents = await response.Content.ReadAsStringAsync();

            var jsonresult = JsonConvert.DeserializeObject<GoogleLatLng>(contents);
            if (jsonresult.status == "OK")
            {
                List<AddrSearchResult> resultList = new List<AddrSearchResult>();
                foreach (Result res in jsonresult.results)
                {
                    resultList.Add(new AddrSearchResult(res.formatted_address,
                        res.geometry.location.lat, res.geometry.location.lng));
                }
                searchResultList.resultList = resultList;
            }
            else
            {
                MessageBox.Show(Properties.Resources.ERROR_ERROR_RESPONSE);
            }

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            AddrSearchResult searchItem = (AddrSearchResult)resultBox.SelectedItem;
            if (searchItem == null)
            {
                return;
            }
            editWindow.UserEditSet(searchItem.resultPlace, searchItem.resultLat.ToString(), searchItem.resultLng.ToString());
            this.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
