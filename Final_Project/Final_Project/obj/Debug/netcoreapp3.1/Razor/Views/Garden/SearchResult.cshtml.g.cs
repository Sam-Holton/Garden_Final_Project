#pragma checksum "C:\Users\broly\Documents\School\Grand Circus\FinalProject\Grand_Circus_Final_Project\Final_Project\Final_Project\Views\Garden\SearchResult.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e25a37f84f2d47a7c23e119813229d071074c6cf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Garden_SearchResult), @"mvc.1.0.view", @"/Views/Garden/SearchResult.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\broly\Documents\School\Grand Circus\FinalProject\Grand_Circus_Final_Project\Final_Project\Final_Project\Views\_ViewImports.cshtml"
using Final_Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\broly\Documents\School\Grand Circus\FinalProject\Grand_Circus_Final_Project\Final_Project\Final_Project\Views\_ViewImports.cshtml"
using Final_Project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e25a37f84f2d47a7c23e119813229d071074c6cf", @"/Views/Garden/SearchResult.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4fb7ef317ae05155a488c52b2e91b7ab94cdc5a", @"/Views/_ViewImports.cshtml")]
    public class Views_Garden_SearchResult : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Final_Project.Models.ViewModels.GardenControllerViewModels.SearchResultViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 10 "C:\Users\broly\Documents\School\Grand Circus\FinalProject\Grand_Circus_Final_Project\Final_Project\Final_Project\Views\Garden\SearchResult.cshtml"
 foreach (var plant in Model.plants)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <ol>\r\n        <li>");
#nullable restore
#line 13 "C:\Users\broly\Documents\School\Grand Circus\FinalProject\Grand_Circus_Final_Project\Final_Project\Final_Project\Views\Garden\SearchResult.cshtml"
       Write(plant.common_name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n        <li>");
#nullable restore
#line 14 "C:\Users\broly\Documents\School\Grand Circus\FinalProject\Grand_Circus_Final_Project\Final_Project\Final_Project\Views\Garden\SearchResult.cshtml"
       Write(plant.scientific_name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n    </ol>\r\n    <img");
            BeginWriteAttribute("src", " src=\"", 373, "\"", 395, 1);
#nullable restore
#line 16 "C:\Users\broly\Documents\School\Grand Circus\FinalProject\Grand_Circus_Final_Project\Final_Project\Final_Project\Views\Garden\SearchResult.cshtml"
WriteAttributeValue("", 379, plant.image_url, 379, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 17 "C:\Users\broly\Documents\School\Grand Circus\FinalProject\Grand_Circus_Final_Project\Final_Project\Final_Project\Views\Garden\SearchResult.cshtml"
    
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Final_Project.Models.ViewModels.GardenControllerViewModels.SearchResultViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591