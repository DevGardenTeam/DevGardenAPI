using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Auth
{
    public class BcryptAuthHandler
    {

        private const int MinimumPasswordLength = 12;
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Le mot de passe ne peut pas être vide ou seulement des espaces.");
            }

            try
            {
                // Utiliser une fonction de hachage forte comme BCrypt
                return BCrypt.Net.BCrypt.HashPassword(password);
            }
            catch (Exception ex)
            {
                // Log l'exception (exemple: dans un fichier, base de données, etc.)
                Console.WriteLine($"Erreur lors du hachage du mot de passe : {ex.Message}");
                throw new ApplicationException("Une erreur est survenue lors du hachage du mot de passe.");
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Le mot de passe ne peut pas être vide ou seulement des espaces.");
            }

            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentException("Le mot de passe haché ne peut pas être vide ou seulement des espaces.");
            }

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (Exception ex)
            {
                // Log l'exception
                Console.WriteLine($"Erreur lors de la vérification du mot de passe : {ex.Message}");
                throw new ApplicationException("Une erreur est survenue lors de la vérification du mot de passe.");
            }
        }

        public static bool IsPasswordComplexEnough(string password)
        {
            if (password.Length < MinimumPasswordLength)
            {
                return false;
            }

            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]");

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        static public string CleanUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Le nom d'utilisateur ne peut pas être vide ou seulement des espaces.");
            }

            username = username.Trim();

            // Vérifier si le nom d'utilisateur contient des caractères interdits
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_.-]+$"))
            {
                throw new ArgumentException("Le nom d'utilisateur contient des caractères interdits.");
            }

            return username;
        }

        // Méthode pour nettoyer un mot de passe
        static public string CleanPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Le mot de passe ne peut pas être vide ou seulement des espaces.");
            }

            password = password.Trim();

            return password;
        }
    }
}
