using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        string apiKey = "f573332d1801aa9e1ec0eb12318c230f";

        public Form1()
        {
            InitializeComponent();
            textBox1.KeyDown += TextBox1_KeyDown;
        }

        private async void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                e.SuppressKeyPress = true;
                await FetchWeatherAsync();
            }
        }

        private async Task FetchWeatherAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={textBox1.Text}&appid={apiKey}");
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync();
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo.Root>(json);
                    UpdateUI(weatherInfo);
                    textBox1.Clear();
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("City doesn't exist or network issues occurred.");
                    textBox1.Clear();
                }
            }
        }

        private void UpdateUI(WeatherInfo.Root weatherInfo)
        {
            // Conversion from Kelvin to Celsius
            int feelsLike = Convert.ToInt32(weatherInfo.main.feelsLike - 273.15);
            int tempCelsius = Convert.ToInt32(weatherInfo.main.Temp - 273.15);
            int minTemp = Convert.ToInt32(weatherInfo.main.tempMin - 273.15);
            int maxTemp = Convert.ToInt32(weatherInfo.main.tempMax - 273.15);

            // Miles per hour to km conversion
            int windSpeedKM = Convert.ToInt32(weatherInfo.wind.speed * 3.6);

            // Grabbing icon for img
            string icon = weatherInfo.weather[0].Icon;

            // Populating labels
            label13.Text = $"Current weather for {textBox1.Text}";
            label14.Text = $"The high will be {maxTemp}°C and the low {minTemp}°C";
            label3.Text = $"{windSpeedKM}km/h";
            label4.Text = $"{weatherInfo.main.pressure}";
            label5.Text = $"{feelsLike}°C";
            label6.Text = $"{tempCelsius}°C";
            label7.Text = $"{weatherInfo.main.Humidity}%";
            label8.Text = $"{weatherInfo.weather[0].Description}";
            pictureBox1.ImageLocation = $"https://openweathermap.org/img/wn/{icon}@2x.png";
        }
    }
}
