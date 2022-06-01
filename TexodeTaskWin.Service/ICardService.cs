using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexodeTaskWin.Service.Model;

namespace TexodeTaskWin.Service
{
    public interface ICardService
    {
        Task<IEnumerable<CardModel>> GetAllCardsAsync();
        Task<IEnumerable<CardModel>> SortCardsByNameAsync();
        Task<int> DeleteCardAsync(int id);
        Task<int> DeleteListOFCardsAsync(IEnumerable<int> listOfId);
        Task<int> AddCardAsync(CardModel card);
        Task<CardModel> GetCardAsync(int id);
        Task<int> UpdateCardAsync(CardModel card);
    }
}
