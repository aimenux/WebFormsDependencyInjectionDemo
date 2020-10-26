using System;
using System.ComponentModel.Composition;
using System.Web.UI;
using Domain;

namespace WebAppUsingSimpleInjector.Pages
{
    public partial class HomePage : Page
    {
        [Import]
        public IDummyService DummyService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            myHeaderId.InnerHtml = DummyService.GetInfos();
        }
    }
}