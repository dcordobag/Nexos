
using BusinessRules.BusinessRules;
using DataAccess.Dto;
using System.Collections.Generic;
using System.Linq;
namespace BusinessRules.BusinessRulesControl
{
    public static class BookControl
    {
        public static int MaxBookByEditorial(int editorialId, EditorialBl editorialBl)
        {
            var editorial = (editorialBl.GetEditorialById(e => e.ID == editorialId).Data as List<Editorial>).FirstOrDefault();
            if (editorial != null)
                return editorial.MaxBooks;
            return -1;
        }

        public static bool AuthorExists(int authorId, AuthorBl authorBl)
        {
            return authorBl.GetListFiltered(a => a.ID == authorId).Any();
        }

        public static bool EditorialExists(int editorialId, EditorialBl editorialBl)
        {
            return editorialBl.GetListFiltered(a => a.ID == editorialId).Any();
        }
    }
}
