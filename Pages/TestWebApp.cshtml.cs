using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WebApp.Pages
{
    public class TestWebAppModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        private static string reqUrl = "https://akslabwebapp.azurewebsites.net/weatherforecast";

        public IList<WeatherForecast>? Forecasts { get; set; }

        private static async Task<List<WeatherForecast>> GetForecasts()
        {
            List<WeatherForecast> fcs = new List<WeatherForecast>();
            var streamTask = await client.GetStreamAsync(reqUrl);

            await foreach (var fc in JsonSerializer.DeserializeAsyncEnumerable<WeatherForecast>(streamTask))
                fcs.Add(fc);

            return fcs;
        }

        public bool gfcWrapper()
        {
            Forecasts = GetForecasts().Result;
            if (Forecasts == null)
                return false;
            else
                return true;
        }

        public void OnGet()
        {
            gfcWrapper();
        }

    }
}
