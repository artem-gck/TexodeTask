using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TexodeTaskWin.Service.Model;

namespace TexodeTaskWin.Service.Logic
{
    public class CardService : ICardService
    {
        private readonly HttpClient _httpClient;

        public CardService(HttpClient httpClient)
            => _httpClient = httpClient;

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            var response = await _httpClient.GetAsync("cards");
            var responseString = await response.Content.ReadAsStringAsync();
            var cards = JsonConvert.DeserializeObject<IEnumerable<Card>>(responseString);

            return cards;
        }
    }
}
