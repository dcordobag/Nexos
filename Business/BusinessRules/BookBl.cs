
namespace BusinessRules.BusinessRules
{
    using DataAccess.Dao;
    using DataAccess.DB;
    using DataAccess.Dto;
    using EntityLayer.Entities.InputData;
    using EntityLayer.Entities.OutputData;
    using global::BusinessRules.BusinessRulesControl;
    using global::BusinessRules.Common;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class BookBl : BookDao
    {
        protected Response response;

        private EditorialBl editorialBl;
        private AuthorBl authorBl;

        public BookBl(DbContextOptions<ConnectionContext> options) : base(options)
        {
            editorialBl = new EditorialBl(options);
            authorBl = new AuthorBl(options);
        }

        public Response CreateBook(BookEntity data)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
             {
                 response = ValidateEditorial(data);
                 return response;
             });
        }

        #region Validations
        protected Response ValidateEditorial(BookEntity data)
        {
            if (BookControl.EditorialExists(data.EditorialID, editorialBl))
            {
                return ValidateAuthor(data);
            }
            return new Response
            {
                EstatusCode = 500,
                Message = "La editorial no está registrada."
            };
        }
        protected Response ValidateAuthor(BookEntity data)
        {
            if (BookControl.AuthorExists(data.AuthorID, authorBl))
            {
                return ValidateMax(data);
            }
            return new Response
            {
                EstatusCode = 404,
                Message = "El autor no está registrado."
            };
        }
        protected Response ValidateMax(BookEntity data)
        {
            if ((GetListBooksFiltered(b => b.EditorialID == data.EditorialID).Data as List<Book>).Count <
                BookControl.MaxBookByEditorial(data.EditorialID, editorialBl))
            {
                return new Response
                {
                    Data = Create(new Book
                    {
                        AuthorID = data.AuthorID,
                        EditorialID = data.EditorialID,
                        Genre = data.Genre,
                        PagesNumber = data.PagesNumber,
                        Title = data.Title,
                        Year = data.Year,
                    }),
                    EstatusCode = 200,
                    Message = String.Empty
                };
            }
            return new Response
            {
                EstatusCode = 404,
                Message = "No es posible registrar el libro, se alcanzó el máximo permitido."
            };

        }
        #endregion

        public Response DeleteBook(BookEntity data)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = Delete(new Book
                    {
                        ID = data.Id
                    }),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }
        public Response GetListBooks()
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = GetList(),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }
        public Response GetListBooksFiltered(Expression<Func<Book, bool>> filter)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = GetListFiltered(filter),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }
        public Response UpdateBook(BookEntity data)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = Update(new DataAccess.Dto.Book
                    {
                        ID = data.Id,
                        Year = data.Year,
                        AuthorID = data.AuthorID,
                        EditorialID = data.EditorialID,
                        Genre = data.Genre,
                        PagesNumber = data.PagesNumber,
                        Title = data.Title
                    }),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }
    }
}
