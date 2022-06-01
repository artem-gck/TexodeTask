using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TexodeTaskWin.Service.Model;

namespace TexodeTaskWin.Service.Logic
{
    /// <summary>
    /// Business logic for working with cards.
    /// </summary>
    /// <seealso cref="TexodeTaskWin.Service.ICardService" />
    public class CardService : ICardService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public CardService(HttpClient httpClient)
            => _httpClient = httpClient;

        /// <summary>
        /// Adds the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>
        /// Id of added card.
        /// </returns>
        public async Task<int> AddCardAsync(CardModel card)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "cards");
            request.Content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var idOfAddedCard = int.Parse(responseString);

            return idOfAddedCard;
        }

        /// <summary>
        /// Deletes the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Id of deleted card.
        /// </returns>
        public async Task<int> DeleteCardAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"cards/{id}");
            var responseString = await response.Content.ReadAsStringAsync();
            var idOfDeletedCard = int.Parse(responseString);

            return idOfDeletedCard;
        }

        /// <summary>
        /// Deletes the list of cards asynchronous.
        /// </summary>
        /// <param name="listOfId">The list of identifier.</param>
        /// <returns>
        /// Number of deleted cards.
        /// </returns>
        public async Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "cards");
            request.Content = new StringContent(JsonConvert.SerializeObject(listOfId), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var idOfDeletedCard = int.Parse(responseString);

            return idOfDeletedCard;
        }

        /// <summary>
        /// Gets all cards asynchronous.
        /// </summary>
        /// <returns>
        /// All cards.
        /// </returns>
        public async Task<IEnumerable<CardModel>> GetAllCardsAsync()
        {
            var response = await _httpClient.GetAsync("cards");
            var responseString = await response.Content.ReadAsStringAsync();
            var cards = JsonConvert.DeserializeObject<IEnumerable<CardModel>>(responseString);

            return cards;
        }

        /// <summary>
        /// Gets the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Card by id.
        /// </returns>
        public async Task<CardModel> GetCardAsync(int id)
        {
            var response = await _httpClient.GetAsync($"cards/{id}");
            var responseString = await response.Content.ReadAsStringAsync();
            var card = JsonConvert.DeserializeObject<CardModel>(responseString);

            return card;
        }

        /// <summary>
        /// Sorts the cards by name asynchronous.
        /// </summary>
        /// <returns>
        /// Sorted cards.
        /// </returns>
        public async Task<IEnumerable<CardModel>> SortCardsByNameAsync()
        {
            var response = await _httpClient.GetAsync("cards/sort");
            var responseString = await response.Content.ReadAsStringAsync();
            var cards = JsonConvert.DeserializeObject<IEnumerable<CardModel>>(responseString);

            return cards;
        }

        /// <summary>
        /// Updates the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>
        /// Id of updated card.
        /// </returns>
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
