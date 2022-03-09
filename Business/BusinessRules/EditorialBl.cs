
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
    public class EditorialBl : EditorialDao
    {
        protected Response response;

        public EditorialBl(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }

        public Response CreateEditorial(EditorialEntity data)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = Create(new Editorial
                    {
                        Email = data.Email,
                        CorrespondenceAddress = data.CorrespondenceAddress,
                        MaxBooks = data.MaxBooks,
                        Name = data.Name,
                        Phone = data.Phone
                    }),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }

        public Response DeleteEditorial(EditorialEntity data)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = Delete(new Editorial
                    {
                        ID = data.Id,
                        Email = data.Email,
                        CorrespondenceAddress = data.CorrespondenceAddress,
                        MaxBooks = data.MaxBooks,
                        Name = data.Name,
                        Phone = data.Phone
                    }),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }

        public Response GetListEditorial()
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

        public Response GetEditorialById(Expression<Func<Editorial, bool>> filter)
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

        public Response UpdateEditorial(EditorialEntity data)
        {
            return BusinessRulesAudit.ExceptionBehavior(() =>
            {
                response = new Response
                {
                    Data = Update(new Editorial
                    {
                        ID = data.Id,
                        Email = data.Email,
                        CorrespondenceAddress = data.CorrespondenceAddress,
                        MaxBooks = data.MaxBooks,
                        Name = data.Name,
                        Phone = data.Phone
                    }),
                    EstatusCode = 200,
                    Message = string.Empty
                };
                return response;
            });
        }
    }
}
