using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexodeTask.Access;
using TexodeTask.Access.Entity;
using TexodeTask.Service.Model;

namespace TexodeTask.Service.Logic
{
    /// <summary>
    /// Business logic for working with cards.
    /// </summary>
    /// <seealso cref="TexodeTask.Service.ICardService" />
    public class CardService : ICardService
    {
        private readonly ICardAccess _cardAccess;
        private readonly IMapper _mapperCard;
        private readonly IMapper _mapperCardEntity;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class.
        /// </summary>
        /// <param name="cardAccess">The card access.</param>
        public CardService(ICardAccess cardAccess)
        { 
            _cardAccess = cardAccess;

            var configCard = new MapperConfiguration(cfg => cfg.CreateMap<CardEntity, Card>());
            var configCardEntity = new MapperConfiguration(cfg => cfg.CreateMap<Card, CardEntity>());

            _mapperCard = new Mapper(configCard);
            _mapperCardEntity = new Mapper(configCardEntity);
        }

        /// <summary>
        /// Adds the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>
        /// Id of added card.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">card - Card is null</exception>
        public async Task<int> AddCardAsync(Card card)
        {
            card = card ?? throw new ArgumentNullException(nameof(card), "Card is null");

            var cardEntity = _mapperCardEntity.Map<CardEntity>(card);

            return await _cardAccess.AddCardAsync(cardEntity);
        }

        /// <summary>
        /// Deletes the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Id of deleted card.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">id - Less than 0</exception>
        public async Task<int> DeleteCardAsync(int id)
        {
            id = id >= 0 ? id : throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            return await _cardAccess.DeleteCardAsync(id);
        }

        /// <summary>
        /// Deletes the list of cards asynchronous.
        /// </summary>
        /// <param name="listOfId">The list of identifier.</param>
        /// <returns>
        /// Number of deleted cards.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">listOfId - List of id is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">id - Less than 0</exception>
        public async Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId)
        {
            listOfId = listOfId ?? throw new ArgumentNullException(nameof(listOfId), "List of id is null");

            foreach (var id in listOfId)
                if (id < 0)
                    throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            return await _cardAccess.DeleteListOFCardsAsync(listOfId);
        }

        /// <summary>
        /// Gets all cards asynchronous.
        /// </summary>
        /// <returns>
        /// All cards.
        /// </returns>
        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            var cardsEntity = await _cardAccess.GetAllCardsAsync();

            return cardsEntity.Select(card => _mapperCard.Map<Card>(card)).AsEnumerable();
        }

        /// <summary>
        /// Gets the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Card by id.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">id - Less than 0</exception>
        public async Task<Card> GetCardAsync(int id)
        {
            id = id >= 0 ? id : throw new ArgumentOutOfRangeException(nameof(id), "Less than 0");

            var card = await _cardAccess.GetCardAsync(id);

            return _mapperCard.Map<Card>(card);
        }

        /// <summary>
        /// Sorts the cards by name asynchronous.
        /// </summary>
        /// <returns>
        /// Sorted cards.
        /// </returns>
        public async Task<IEnumerable<Card>> SortCardsByNameAsync()
        {
            var cardsEntity = await _cardAccess.SortCardsByNameAsync();

            return cardsEntity.Select(card => _mapperCard.Map<Card>(card)).AsEnumerable();
        }

        /// <summary>
        /// Updates the card asynchronous.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>
        /// Id of updated card.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">card - Card is null</exception>
        public async Task<int> UpdateCardAsync(Card card)
        {
            card = card ?? throw new ArgumentNullException(nameof(card), "Card is null");

            var cardEntity = _mapperCardEntity.Map<CardEntity>(card);

            return await _cardAccess.UpdateCardAsync(cardEntity);
        }
    }
}
