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


        public static MvcHtmlString DisplayLikes(this HtmlHelper html, int count)
        {
         
            var msg = " <div class='badge'> <hr /> <span class='text-info'> " + count  
                + " likes</span></div>";
            return MvcHtmlString.Create(msg);
        }

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
        public static MvcHtmlString ErrorAlert(this HtmlHelper htmlHelper, bool hasPropInfo = false)
        {
            //if (ViewData.ModelState.Keys.Any(k => ViewData.ModelState[k].Errors.Count() > 0))
            //          {
            //              <div class="alert alert-danger">
            //                  <button class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            //                  @Html.ValidationSummary(false, "Errors: ")
            //              </div>
            //          }
            var msg = "";
            if (!htmlHelper.ViewData.ModelState.IsValid)
            {
                msg = " <div class=\"alert alert-danger\"> <button class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>" 
                    + htmlHelper.ValidationSummary(hasPropInfo, "Errors: ") 
                    + "</div>";
            }

            return new MvcHtmlString(msg);
        }
        public static MvcHtmlString ErrorAlert(this HtmlHelper htmlHelper, MvcHtmlString error)
        {
            //if (ViewData.ModelState.Keys.Any(k => ViewData.ModelState[k].Errors.Count() > 0))
            //          {
            //              <div class="alert alert-danger">
            //                  <button class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            //                  @Html.ValidationSummary(false, "Errors: ")
            //              </div>
            //          }
            var msg = "";
            if (!htmlHelper.ViewData.ModelState.IsValid)
            {
                msg = " <div class=\"alert alert-danger\"> <button class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>" 
                    + error.ToString() + "</div>";
            }

            return new MvcHtmlString(msg);
        }

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