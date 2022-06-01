using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TexodeTask.Access.Entity;

namespace TexodeTask.Access.File
{
    /// <summary>
    /// Access to file source.
    /// </summary>
    /// <seealso cref="TexodeTask.Access.ICardAccess" />
    public class CardAccess : ICardAccess
    {
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardAccess"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public CardAccess(string path) 
            => _path = path;

        /// <summary>
        /// Adds the card asynchronous.
        /// </summary>
        /// <param name="cardEntity">The card entity.</param>
        /// <returns>Id of added card.</returns>
        /// <exception cref="System.ArgumentNullException">cardEntity - Card is null</exception>
        public async Task<int> AddCardAsync(CardEntity cardEntity)
        {
            _ = cardEntity ?? throw new ArgumentNullException(nameof(cardEntity), "Card is null");

            var cards = await ReadCardsFromFile();

            var lastCard = cards.LastOrDefault();
            var lastId = lastCard is null ? -1 : lastCard.Id;
            
            cardEntity.Id = lastId + 1;
            cards.Add(cardEntity);

            await WriteCardsToFile(cards);

            return cardEntity.Id;
        }

        /// <summary>
        /// Deletes the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Id of deleted card.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// id
        /// or
        /// cardForDelete - Card does not exist
        /// </exception>
        public async Task<int> DeleteCardAsync(int id)
        {
            _ = id >= 0 ? id : throw new ArgumentNullException(nameof(id));

            var cards = await ReadCardsFromFile();

            var cardForDelete = cards.FirstOrDefault(card => card.Id == id);
            cardForDelete = cardForDelete ?? throw new ArgumentNullException(nameof(cardForDelete), "Card does not exist");

            cards.Remove(cardForDelete);

            await WriteCardsToFile(cards);

            return id;
        }

        /// <summary>
        /// Deletes the list of cards asynchronous.
        /// </summary>
        /// <param name="listOfId">The list of identifier.</param>
        /// <returns>Count of deleted cards.</returns>
        /// <exception cref="System.ArgumentNullException">listOfId - List of id is null</exception>
        public async Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId)
        {
            _ = listOfId ?? throw new ArgumentNullException(nameof(listOfId), "List of id is null");

            var cards = await ReadCardsFromFile();
            cards.RemoveAll(card => listOfId.Contains(card.Id));
            
            await WriteCardsToFile(cards);

            return listOfId.Count();
        }

        /// <summary>
        /// Gets all cards asynchronous.
        /// </summary>
        /// <returns>All cards.</returns>
        public async Task<IEnumerable<CardEntity>> GetAllCardsAsync()
            => await ReadCardsFromFile();

        /// <summary>
        /// Gets the card asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Card by id.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// id
        /// or
        /// card - Card does not exist
        /// </exception>
        public async Task<CardEntity> GetCardAsync(int id)
        {
            _ = id >= 0 ? id : throw new ArgumentNullException(nameof(id));

            var cards = await ReadCardsFromFile();
            var card = cards.FirstOrDefault(card => card.Id == id);
            card = card ?? throw new ArgumentNullException(nameof(card), "Card does not exist");

            return card;
        }

        /// <summary>
        /// Sorts the cards by name asynchronous.
        /// </summary>
        /// <returns>Sorted cards.</returns>
        public async Task<IEnumerable<CardEntity>> SortCardsByNameAsync()
            => (await ReadCardsFromFile()).OrderBy(card => card.Name);

        /// <summary>
        /// Updates the card asynchronous.
        /// </summary>
        /// <param name="cardEntity">The card entity.</param>
        /// <returns>Id of updated card.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// cardEntity - Card is null
        /// or
        /// card - Card does not exist
        /// </exception>
        public async Task<int> UpdateCardAsync(CardEntity cardEntity)
        {
            _ = cardEntity ?? throw new ArgumentNullException(nameof(cardEntity), "Card is null");

            var cards = await ReadCardsFromFile();
            var card = cards.FirstOrDefault(card => card.Id == cardEntity.Id);
            card = card ?? throw new ArgumentNullException(nameof(card), "Card does not exist");

            card.Name = cardEntity.Name;
            card.Photo = cardEntity.Photo;

            await WriteCardsToFile(cards);

            return card.Id;
        }

        private async Task<List<CardEntity>> ReadCardsFromFile()
        {
            using var fileReadStream = new FileStream(_path, FileMode.OpenOrCreate);
            using var streamReader = new StreamReader(fileReadStream);

            var cardsJson = await streamReader.ReadToEndAsync();
            var cards = JsonConvert.DeserializeObject<List<CardEntity>>(cardsJson);
            cards ??= new List<CardEntity>();

            fileReadStream.Close();

            return cards;
        }

        private async Task WriteCardsToFile(List<CardEntity> cards)
        {
            var cardsJson = JsonConvert.SerializeObject(cards);

            using var fileWriteStream = new FileStream(_path, FileMode.Create);
            using var streamWriter = new StreamWriter(fileWriteStream);

            await streamWriter.WriteLineAsync(cardsJson);
        }
    }
}
