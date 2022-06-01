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

        public async Task<int> AddCardAsync(CardModel card)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "cards");
            request.Content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var idOfAddedCard = int.Parse(responseString);

            return idOfAddedCard;
        }

        public async Task<int> DeleteCardAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"cards/{id}");
            var responseString = await response.Content.ReadAsStringAsync();
            var idOfDeletedCard = int.Parse(responseString);

            return idOfDeletedCard;
        }

        public async Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "cards");
            request.Content = new StringContent(JsonConvert.SerializeObject(listOfId), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var idOfDeletedCard = int.Parse(responseString);

            return idOfDeletedCard;
        }

        public async Task<IEnumerable<CardModel>> GetAllCardsAsync()
        {
            var response = await _httpClient.GetAsync("cards");
            var responseString = await response.Content.ReadAsStringAsync();
            var cards = JsonConvert.DeserializeObject<IEnumerable<CardModel>>(responseString);

            return cards;
        }

        public async Task<CardModel> GetCardAsync(int id)
        {
            var response = await _httpClient.GetAsync($"cards/{id}");
            var responseString = await response.Content.ReadAsStringAsync();
            var card = JsonConvert.DeserializeObject<CardModel>(responseString);

            return card;
        }

        public async Task<IEnumerable<CardModel>> SortCardsByNameAsync()
        {
            var response = await _httpClient.GetAsync("cards/sort");
            var responseString = await response.Content.ReadAsStringAsync();
            var cards = JsonConvert.DeserializeObject<IEnumerable<CardModel>>(responseString);

            return cards;
        }

        public async Task<int> UpdateCardAsync(CardModel card)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "cards");
            request.Content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var idOfUpdatedCard = int.Parse(responseString);

            return idOfUpdatedCard;
        }
    }
}
