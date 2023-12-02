namespace DevGardenAPI
{
    /*
     * Represents the response from the GitHub API.
     */
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
    }
}