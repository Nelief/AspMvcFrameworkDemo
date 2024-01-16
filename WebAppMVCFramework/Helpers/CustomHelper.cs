using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages.Html;

namespace WebAppMVCFramework.Helpers
{
    public static class CustomHelper
    {
        public static MvcHtmlString MyDate<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            //Estrae il model dalla view 
            var model = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;

            //Crea la data formattata correttamente per yyyy-MM-dd a partire dal valore ottenuto dal model 
            var formattedDate = model is DateTime time ? time.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");

            //Attributi HTML
            var htmlAttributes = new
            {
                @class = "form-control",
                type = "date",
                value = formattedDate
            };

            //creazione del markup HTML a partire dall' helper "EditFor"
            var result = htmlHelper.EditorFor(expression, new { htmlAttributes });

            return MvcHtmlString.Create(result.ToString());
        }
    }
}