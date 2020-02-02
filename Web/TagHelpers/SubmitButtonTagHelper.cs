using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpers
{
    [HtmlTargetElement("submit-button")]
    public class SubmitButtonTagHelper : TagHelper
    {
        public string Title { get; set; } = "Submit";
        public string Classes { get; set; }
        public string Id { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output?.Content.SetHtmlContent(
                $@"<span class=""btn btn-primary {Classes}"" id=""{Id}"">{Title}</span>");

            base.Process(context, output);
        }
    }
}