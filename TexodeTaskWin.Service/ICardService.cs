using System.Collections.Generic;
using System.Threading.Tasks;
using TexodeTaskWin.Service.Model;

namespace TexodeTaskWin.Service
{
    /// <summary>
    /// Business logic for working with cards.
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// Gets all cards asynchronous.
        /// </summary>
        /// <returns>All cards.</returns>
        Task<IEnumerable<CardModel>> GetAllCardsAsync();

        /// <summary>
        /// Sorts the cards by name asynchronous.
        /// </summary>
        /// <returns>Sorted cards.</returns>
        Task<IEnumerable<CardModel>> SortCardsByNameAsync();

        /// <summary>
        /// Deletes the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Id of deleted card.</returns>
        Task<int> DeleteCardAsync(int id);

        /// <summary>
        /// Deletes the list of cards asynchronous.
        /// </summary>
        /// <param name="listOfId">The list of identifier.</param>
        /// <returns>Number of deleted cards.</returns>
        Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId);

        /// <summary>
        /// Adds the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>Id of added card.</returns>
        Task<int> AddCardAsync(CardModel card);

        /// <summary>
        /// Gets the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Card by id.</returns>
        Task<CardModel> GetCardAsync(int id);

        /// <summary>
        /// Updates the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>Id of updated card.</returns>
        Task<int> UpdateCardAsync(CardModel card);
    }
}
