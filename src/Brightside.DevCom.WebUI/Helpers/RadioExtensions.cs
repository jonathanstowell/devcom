// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The radio extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Helpers
{
    using System;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    ///     The radio extensions.
    /// </summary>
    public static class RadioExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The radio button for enum.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString RadioButtonForEnum<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string[] names = Enum.GetNames(metaData.ModelType);
            var sb = new StringBuilder();
            foreach (string name in names)
            {
                string id = string.Format(
                    "{0}_{1}_{2}", htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix, metaData.PropertyName, name);

                string radio = htmlHelper.RadioButtonFor(expression, name, new { id }).ToHtmlString();
                sb.AppendFormat("<label for=\"{0}\">{1}</label> {2}", id, HttpUtility.HtmlEncode(name), radio);
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion
    }
}