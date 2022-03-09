
namespace BusinessRules.Common
{
    using System;
    using System.Threading.Tasks;

    public class BusinessRulesAudit
    {
        protected BusinessRulesAudit()
        {

        }
        public static TReturn ExceptionBehavior<TReturn>(Func<TReturn> fnAccion)
        {
            TReturn returnBehavior;

            try
            {
                returnBehavior = fnAccion();
            }
            catch (Exception ex)
            {
                returnBehavior = default;
                throw ex;
            }

            return returnBehavior;
        }
    }
}
