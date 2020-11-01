using System.Linq.Expressions;

namespace System.Web.Mvc.Html {
    public static class Helper {
        public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) {
            return MvcHtmlString.Create(ModelMetadata.FromLambdaExpression(expression, html.ViewData).Description);
        }
    }
}