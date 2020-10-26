using System;
using System.Web.UI;
using Domain;

namespace WebAppUsingAutofac.Pages
{
    public partial class HomePage : Page
    {
        private readonly IDummyService _dummyService;

        public HomePage(IDummyService dummyService)
        {
            _dummyService = dummyService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            myHeaderId.InnerHtml = _dummyService.GetInfos();
        }
    }
}