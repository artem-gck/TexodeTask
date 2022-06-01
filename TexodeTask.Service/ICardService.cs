using System.Collections.Generic;
using System.Threading.Tasks;
using TexodeTask.Service.Model;

namespace TexodeTask.Service
{
    /// <summary>
    /// Business logic for working with cards.
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// Adds the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>Id of added card.</returns>
        public Task<int> AddCardAsync(Card card);

        /// <summary>
        /// Gets all cards asynchronous.
        /// </summary>
        /// <returns>All cards.</returns>
        public Task<IEnumerable<Card>> GetAllCardsAsync();

        /// <summary>
        /// Gets the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Card by id.</returns>
        public Task<Card> GetCardAsync(int id);

        /// <summary>
        /// Updates the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>Id of updated card.</returns>
        public Task<int> UpdateCardAsync(Card card);

        /// <summary>
        /// Deletes the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Id of deleted card.</returns>
        public Task<int> DeleteCardAsync(int id);

        /// <summary>
        /// Deletes the list of cards asynchronous.
        /// </summary>
        /// <param name="listOfId">The list of identifier.</param>
        /// <returns>Number of deleted cards.</returns>
        public Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId);

        /// <summary>
        /// Sorts the cards by name asynchronous.
        /// </summary>
        /// <returns>Sorted cards.</returns>
        public Task<IEnumerable<Card>> SortCardsByNameAsync();
    }
}
