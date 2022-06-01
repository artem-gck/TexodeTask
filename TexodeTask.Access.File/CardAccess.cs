using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TexodeTask.Access.Entity;

namespace TexodeTask.Access.File
{
    public class CardAccess : ICardAccess
    {
        private readonly string _path;

        public CardAccess(string path) 
            => _path = path;

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

        public async Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId)
        {
            _ = listOfId ?? throw new ArgumentNullException(nameof(listOfId), "List of id is null");

            var cards = await ReadCardsFromFile();
            cards.RemoveAll(card => listOfId.Contains(card.Id));
            
            await WriteCardsToFile(cards);

            return listOfId.Count();
        }

        public async Task<IEnumerable<CardEntity>> GetAllCardsAsync()
            => await ReadCardsFromFile();

        public async Task<CardEntity> GetCardAsync(int id)
        {
            _ = id >= 0 ? id : throw new ArgumentNullException(nameof(id));

            var cards = await ReadCardsFromFile();
            var card = cards.FirstOrDefault(card => card.Id == id);
            card = card ?? throw new ArgumentNullException(nameof(card), "Card does not exist");

            return card;
        }

        public async Task<IEnumerable<CardEntity>> SortCardsByNameAsync()
            => (await ReadCardsFromFile()).OrderBy(card => card.Name);

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
