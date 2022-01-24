using ArtGalleryStore.WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace ArtGalleryStore.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        [System.Obsolete]
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }
    }
}