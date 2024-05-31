using Auth;
using Microsoft.AspNetCore.Mvc;
using log4net;

namespace DevGardenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthentificationController: BcryptAuthHandler
    {
        public static bool Register(String username, String password)
        {
            username = BcryptAuthHandler.CleanUsername(username);
            password = BcryptAuthHandler.CleanPassword(password);

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Le mot de passe ne peut pas être vide ou seulement des espaces.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Le nom d'utilisateur ne peut pas être vide ou seulement des espaces.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Le mot de passe haché ne peut pas être vide ou seulement des espaces.");
            }

            if(!BcryptAuthHandler.IsPasswordComplexEnough(password))
            {
                throw new ArgumentException("Le mot de passe n'est pas assez long ou ne comporte pas les caractères nécessaires.");
            }

            //Insertion en base de données avec le user et BcryptAuthHandler.HashPassword(password)
            return true;
        }

        public static bool login(String username, String password)
        {
            username = BcryptAuthHandler.CleanUsername(username);
            password = BcryptAuthHandler.CleanPassword(password);

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Le mot de passe ne peut pas être vide ou seulement des espaces.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Le nom d'utilisateur ne peut pas être vide ou seulement des espaces.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Le mot de passe haché ne peut pas être vide ou seulement des espaces.");
            }

            if (!BcryptAuthHandler.IsPasswordComplexEnough(password))
            {
                throw new ArgumentException("Le mot de passe n'est pas assez long ou ne comporte pas les caractères nécessaires.");
            }

            return true;
        }

    }
}
