using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Navebsite.Controls
{
    public partial class Review : UserControl
    {
        public NavebsiteBL.Review ReviewObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < 5; i++)
            {
                var image = new Image {ImageUrl = "./starfilled.svg"};
                if (i >= ReviewObject.Stars) image.ImageUrl = "./starempty.svg";
                stars.Controls.Add(image);
            }

            content.Text = Server.HtmlEncode(ReviewObject.Content);
            title.Text = Server.HtmlEncode(ReviewObject.GameName) + " Review";
            title.NavigateUrl = "../GamePage.aspx?id=" + ReviewObject.GameId;
            author.Text = "review by " + Server.HtmlEncode(ReviewObject.Username);
            author.NavigateUrl = "../Profile.aspx?id=" + ReviewObject.UserId;
        }
    }
}