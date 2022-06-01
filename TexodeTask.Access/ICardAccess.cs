using System.Collections.Generic;
using System.Threading.Tasks;
using TexodeTask.Access.Entity;

namespace TexodeTask.Access
{
    /// <summary>
    /// Access to data source.
    /// </summary>
    public interface ICardAccess
    {
        /// <summary>
        /// Adds the card asynchronous.
        /// </summary>
        /// <param name="cardEntity">The card entity.</param>
        /// <returns>Id of added card.</returns>
        public Task<int> AddCardAsync(CardEntity cardEntity);

        /// <summary>
        /// Gets all cards asynchronous.
        /// </summary>
        /// <returns>All cards.</returns>
        public Task<IEnumerable<CardEntity>> GetAllCardsAsync();

        /// <summary>
        /// Gets the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Card by id.</returns>
        public Task<CardEntity> GetCardAsync(int id);

        /// <summary>
        /// Updates the card asynchronous.
        /// </summary>
        /// <param name="cardEntity">The card entity.</param>
        /// <returns>Id of updated card.</returns>
        public Task<int> UpdateCardAsync(CardEntity cardEntity);

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
        /// <returns>Count of deleted cards.</returns>
        public Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId);

        /// <summary>
        /// Sorts the cards by name asynchronous.
        /// </summary>
        /// <returns>Sorted cards.</returns>
        public Task<IEnumerable<CardEntity>> SortCardsByNameAsync();
    }
}
