using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TexodeTask.Access.Entity;

namespace TexodeTask.Access
{
    public interface ICardAccess
    {
        public Task<IEnumerable<CardEntity>> GetAllCardsAsync();
        public Task<CardEntity> GetCardAsync(int id);
        public Task<int> UpdateCard(CardEntity cardEntity);
        public Task<int> DeleteCard(int id);
        public Task<int> DeleteListOFCards(IEnumerable<int> listOfId);
        public Task<IEnumerable<CardEntity>> SortCardsByName();
    }
}
