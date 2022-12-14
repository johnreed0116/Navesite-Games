using System;
using System.Web.UI;
using Navebsite.App_Code;
using NavebsiteBL;

namespace Navebsite
{
    public partial class UserSettings : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RedeemError.Text = "";
            ErrorBox.Text = "";
            var user = (User) Session["user"];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            CurrentBalance.Text = "" + user.Balance;
            CurrentBackground.ImageUrl = user.BackgroundUrl;
            CurrentProfilePicture.ImageUrl = user.ProfilePictureUrl;
            if (IsPostBack) return;
            Bio.Text = user.Description;
            
        }

        protected void UpdateBio_OnClick(object sender, EventArgs e)
        {
            Validate("Bio");
            if (!IsValid) return;
            var user = (User)Session["user"];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            user.UpdateDescription(Bio.Text);
        }

        //protected void UploadImage_OnClick(object sender, EventArgs e)
        //{
        //    Validate("Image");
        //    if (!IsValid) return;
        //    var user = (User)Session["user"];
        //    if (user == null)
        //    {
        //        Response.Redirect("Login.aspx");
        //        return;
        //    }
        //    string filename = UploadHelper.ImageFileUpload(ImageUpload, "Images/UserProfiles/", "image.png", Server);
        //    UserPhoto.InsertPhoto(user.Id, filename);
        //    Response.Redirect("UserSettings.aspx");
        //}

        protected void UploadProfile_OnClick(object sender, EventArgs e)
        {
            Validate("ProfilePicture");
            if (!IsValid) return;
            var user = (User)Session["user"];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            string filename = UploadHelper.ImageFileUpload(ProfilePicture, "Images/UserProfiles/", "profile.png", Server);
            user.UpdateProfilePicture(filename);
            Response.Redirect("UserSettings.aspx");
        }

        protected void UploadBackground_OnClick(object sender, EventArgs e)
        {
            Validate("Background");
            if (!IsValid) return;
            var user = (User)Session["user"];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            string filename = UploadHelper.ImageFileUpload(Background, "Images/UserBackgrounds/", "no.png", Server);
            user.UpdateBackground(filename);
            Response.Redirect("UserSettings.aspx");

        }

        protected void UpdatePassword_OnClick(object sender, EventArgs e)
        {
            Validate("ChangePassword");
            if (!IsValid) return;
            var user = (User)Session["user"];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            var authenticatedUser = NavebsiteBL.User.AuthUser(user.Username, OldPassword.Text);
            if (authenticatedUser != null)
            {
                user.UpdatePassword(Password.Text);

            }
            else
            {
                ErrorBox.Text = "Old password is not correct";
            }

        }

        protected void AddBalanceButton_OnClick(object sender, EventArgs e)
        {
            Response.Redirect($"Pay.aspx?am={AddBalance.Text}&for=bal");
        }

        protected void RedeemGameButton_OnClick(object sender, EventArgs e)
        {
            string code = RedeemGame.Text.ToUpper();

            var user = (User)Session["user"];
            
            int game = GameCode.RedeemCode(user.Id, code);

            if (game == -1)
            {
                RedeemError.Text = "code entered is either invalid or used";
            }
            else
            {
                Game gameObject = new Game(game);
                user.AddActivity("Redeemed code for " + gameObject.GameName);
                Response.Redirect("GamePage.aspx?id=" + game);
            }
        }
    }
}