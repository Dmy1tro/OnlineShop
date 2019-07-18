using OnlineShop.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace OnlineShop.Infrastructure
{
    public static class PageHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, Pagination pagingInfo, Func<int, string> pageToUrl)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tagUl = new TagBuilder("ul");
            tagUl.AddCssClass("pagination");
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tagLi = new TagBuilder("li");
                tagLi.AddCssClass("page-item");
                if (i == pagingInfo.CurrentPage)
                {
                    tagLi.AddCssClass("active");
                }

                TagBuilder tagA = new TagBuilder("a");
                tagA.AddCssClass("page-link");
                tagA.MergeAttribute("href", pageToUrl(i));
                tagA.InnerHtml = i.ToString();

                tagLi.InnerHtml = tagA.ToString();
                result.Append(tagLi);
            }
            tagUl.InnerHtml = result.ToString();
            return MvcHtmlString.Create(tagUl.ToString());
        }
    }
}