using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using U7StandardMembership.Models;
using Umbraco.Core.Persistence.Querying;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace U7StandardMembership.Controllers.SurfaceControllers
{
    public class ViewProfileController : RenderMvcController
    {
        public ViewProfileController()
            : this(UmbracoContext.Current)
        {
        }

        public ViewProfileController(UmbracoContext umbracoContext)
            : base(umbracoContext)
        {
        }

        public override ActionResult Index(RenderModel  model)
        {
            //Get profileURLtoCheck
            string profileURLtoCheck = Request.RequestContext.RouteData.Values["profileURLtoCheck"].ToString();

            //Create a view model
            ViewProfileViewModel profile = new ViewProfileViewModel();

            var membershipService = ApplicationContext.Services.MemberService;

            //Check we have a value in the URL
            if (!String.IsNullOrEmpty(profileURLtoCheck))
            {
                //var currentMember = memmembershipService..GetById(this.base.Umbraco.UmbracoHelper.Members.GetCurrentMemberId()));

                //Try and find member with the QueryString value ?profileURLtoCheck=warrenbuckley
                //Member findMember = Member.GetAllAsList().FirstOrDefault(x => x.getProperty("profileURL").Value.ToString() == profileURLtoCheckMember);
                var findMember = membershipService.GetMembersByPropertyValue("profileURL", profileURLtoCheck, StringPropertyMatchType.Exact).SingleOrDefault();

                //Check if we found member
                if (findMember != null)
                {
                    //Increment profile view counter by one
                    int noOfProfileViews = 0;
                    int.TryParse(findMember.Properties["numberOfProfileViews"].Value.ToString(), out noOfProfileViews);

                    //Increment counter by one
                    findMember.Properties["numberOfProfileViews"].Value = noOfProfileViews + 1;

                    //Save it down to the member
                    membershipService.Save(findMember);

                    int noOfLogins = 0;
                    int.TryParse(findMember.Properties["numberOfLogins"].Value.ToString(), out noOfLogins);

                    //Got the member lets bind the data to the view model
                    profile.Name = findMember.Name;
                    profile.MemberID = findMember.Id;
                    profile.EmailAddress = findMember.Email;

                    profile.Description = findMember.Properties["description"].Value.ToString();

                    profile.LinkedIn = findMember.Properties["linkedIn"].Value.ToString();
                    profile.Skype = findMember.Properties["skype"].Value.ToString();
                    profile.Twitter = findMember.Properties["twitter"].Value.ToString();

                    profile.NumberOfLogins = noOfLogins;
                    profile.LastLoginDate = findMember.LastLoginDate;
                    profile.NumberOfProfileViews = noOfProfileViews;
                }
                else
                {
                    //Couldn't find the member return a 404
                    return new HttpNotFoundResult("The member profile does not exist");
                }
            }
            else
            {
                //Couldn't find the member return a 404
                return new HttpNotFoundResult("No profile URL parameter was provided");
            }

            //Return template with our profile model
            return CurrentTemplate(profile);
        }
    }
}