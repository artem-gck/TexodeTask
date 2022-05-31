using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexodeTask.Service.Model;

namespace TexodeTask.Service
{
    public interface ICardService
    {
        public Task<IEnumerable<Card>> GetAllCardsAsync();
        public Task<Card> GetCardAsync(int id);
        public Task<int> UpdateCard(Card card);
        public Task<int> DeleteCard(int id);
        public Task<int> DeleteListOFCards(IEnumerable<int> listOfId);
        public Task<IEnumerable<Card>> SortCardsByName();
    }
}
