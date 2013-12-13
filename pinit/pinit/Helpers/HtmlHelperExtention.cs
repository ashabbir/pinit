using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Linq.Expressions;
using System.Runtime.CompilerServices;


namespace System.Web.Mvc.Html
{
    public static class HtmlHelperExtention
    {

        /// <summary>
        ///this will display likes 1 like 10 likes etc in a badge
        /// </summary>
        /// <param name="html"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayLikes(this HtmlHelper html, int count)
        {
            var likeword = "like";
            if (count > 1) 
            {
                likeword = "likes";
            }
            var msg = " <p class='badge text-info'> " + count + " "
                + likeword+ "</p>";

            if (count == 0) 
            {
                msg = "";
            }
            return MvcHtmlString.Create(msg);
        }


        /// <summary>
        /// this will display the comment in a well
        /// </summary>
        /// <param name="html"></param>
        /// <param name="comment"></param>
        /// <param name="by"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayComment(this HtmlHelper html, string comment, string by, DateTime on) 
        {
             //<div class="well">
             //      @Html.DisplayName("asd")
             //       <hr />
             //       <span class="text-info">by ahmed on 12.1.2013</span>
             //   </div>

           var msg = " <div class='well'> " + comment + "<hr /> <span class='text-info'>  by " 
               + by + " on " + on.ToShortDateString()  
               + "</span>"
               +  "</div>";
            return MvcHtmlString.Create(msg);
        }


        /// <summary>
        /// this will show the pin image
        /// </summary>
        /// <param name="html"></param>
        /// <param name="imagePath"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static MvcHtmlString PinTag(this HtmlHelper html, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            //@class = "form-control", @style = "width: 140px; height: 140px;"
            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.AddCssClass("thumbnail");
            imgBuilder.MergeAttribute("style", "width: 140px; height: 140px;");
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(imgHtml);
        }

        /// <summary>
        /// this will show the image clickable for URL
        /// </summary>
        /// <param name="html"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="routeValues"></param>
        /// <param name="imagePath"></param>
        /// <param name="alt"></param>
        /// <param name="isMine"></param>
        /// <returns></returns>
        public static MvcHtmlString PinTag(this HtmlHelper html, string action, string controller, object routeValues, string imagePath, string alt ,bool isMine = true)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.AddCssClass("thumbnail");
            imgBuilder.MergeAttribute("style", "width: 140px; height: 140px;");
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, controller ,routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }


        /// <summary>
        /// this will show the board clickable to details
        /// </summary>
        /// <param name="html"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        /// <param name="imagePath"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static MvcHtmlString ActionBoard(this HtmlHelper html, string action, object routeValues, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            //@class = "form-control", @style = "width: 140px; height: 140px;"
            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.AddCssClass("thumbnail");
            imgBuilder.MergeAttribute("style", "width: 140px; height: 140px;");
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }


       /// <summary>
        ///  this will show the board clickable to details
       /// </summary>
       /// <param name="html"></param>
       /// <param name="action"></param>
       /// <param name="controller"></param>
       /// <param name="routeValues"></param>
       /// <param name="imagePath"></param>
       /// <param name="alt"></param>
       /// <returns></returns>
        public static MvcHtmlString ActionBoard(this HtmlHelper html, string action, string controller ,object routeValues, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            //@class = "form-control", @style = "width: 140px; height: 140px;"
            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.AddCssClass("thumbnail");
            imgBuilder.MergeAttribute("style", "width: 140px; height: 140px;");
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action( action, controller ,routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }



        /// <summary>
        /// this will do the Bootstrap alert 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="hasPropInfo"></param>
        /// <returns></returns>
        public static MvcHtmlString ErrorAlert(this HtmlHelper htmlHelper, bool hasPropInfo = false)
        {
          
            var msg = "";
            if (!htmlHelper.ViewData.ModelState.IsValid)
            {
                msg = " <div class=\"alert alert-danger\"> <button class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>" 
                    + htmlHelper.ValidationSummary(hasPropInfo, "Errors: ") 
                    + "</div>";
            }

            return new MvcHtmlString(msg);
        }

        /// <summary>
        /// this will do the bootstrap alert
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static MvcHtmlString ErrorAlert(this HtmlHelper htmlHelper, MvcHtmlString error)
        {
         
            var msg = "";
            if (!htmlHelper.ViewData.ModelState.IsValid)
            {
                msg = " <div class=\"alert alert-danger\"> <button class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>" 
                    + error.ToString() + "</div>";
            }

            return new MvcHtmlString(msg);
        }


        /// <summary>
        /// this will do bootstrap alert not that good
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="closeable"></param>
        /// <returns></returns>
        public static MvcHtmlString ValidationSummaryBootstrap(this HtmlHelper helper, bool closeable)
        {
            # region Equivalent view markup
            // var errors = ViewData.ModelState.SelectMany(x => x.Value.Errors.Select(y => y.ErrorMessage));
            //
            // if (errors.Count() > 0)
            // {
            //     <div class="alert alert-error alert-block">
            //         <button type="button" class="close" data-dismiss="alert">&times;</button>
            //         <strong>Validation error</strong> - please fix the errors listed below and try again.
            //         <ul>
            //             @foreach (var error in errors)
            //             {
            //                 <li class="text-error">@error</li>
            //             }
            //         </ul>
            //     </div>
            // }
            # endregion

            var errors = helper.ViewContext.ViewData.ModelState.SelectMany(state => state.Value.Errors.Select(error => error.ErrorMessage));

            int errorCount = errors.Count();

            if (errorCount == 0)
            {
                return new MvcHtmlString(string.Empty);
            }

            var div = new TagBuilder("div");
            div.AddCssClass("alert");
            div.AddCssClass("alert-error");

            string message;

            if (errorCount == 1)
            {
                message = errors.First();
            }
            else
            {
                message = "Please fix the errors listed below and try again.";
                div.AddCssClass("alert-block");
            }

            if (closeable)
            {
                var button = new TagBuilder("button");
                button.AddCssClass("close");
                button.MergeAttribute("type", "button");
                button.MergeAttribute("data-dismiss", "alert");
                button.SetInnerText("x");
                div.InnerHtml += button.ToString();
            }

            div.InnerHtml += "<strong>Validation error</strong> - " + message;

            if (errorCount > 1)
            {
                var ul = new TagBuilder("ul");

                foreach (var error in errors)
                {
                    var li = new TagBuilder("li");
                    li.AddCssClass("text-error");
                    li.SetInnerText(error);
                    ul.InnerHtml += li.ToString();
                }

                div.InnerHtml += ul.ToString();
            }

            return new MvcHtmlString(div.ToString());
        }

        /// <summary>
        /// Overload allowing no arguments.
        /// </summary>
        public static MvcHtmlString ValidationSummaryBootstrap(this HtmlHelper helper)
        {
            return ValidationSummaryBootstrap(helper, true);
        }
    }
}