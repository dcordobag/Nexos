
namespace BusinessRules.BusinessRules
{
    using DataAccess.Dao;
    using DataAccess.DB;
    using DataAccess.Dto;
    using EntityLayer.Entities.InputData;
    using EntityLayer.Entities.OutputData;
    using global::BusinessRules.Common;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq.Expressions;
    public class AuthorBl : AuthorDao
    {
        protected Response response;

        public AuthorBl(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }

        public Response Create(AuthorEntity data)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = Create(new Author
                    {
                        Birthdate = data.Birthdate,
                        Email = data.Email,
                        FullName = data.FullName,
                        OriginCity = data.OriginCity
                    }),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }

        public Response Delete(AuthorEntity data)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = Delete(new Author
                    {
                        ID = data.Id,
                        Birthdate = data.Birthdate,
                        Email = data.Email,
                        FullName = data.FullName,
                        OriginCity = data.OriginCity
                    }),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }

        public Response GetListAuthors()
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

        public Response GetListAuthorsFiltered(Expression<Func<Author, bool>> filter)
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

        public Response UpdateAuthor(Author data)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = Update(new Author
                    {
                        Birthdate = data.Birthdate,
                        Email = data.Email,
                        FullName = data.FullName,
                        ID = data.ID,
                        OriginCity = data.OriginCity
                    }),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }
    }
}
