using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Managers
{
    /// <summary>
    /// Classe abstraite générique définissant les méthodes disponibles pour les différentes ressources.
    /// </summary>
    public abstract class PlatformController : ControllerBase
    {
        /// <summary>
        /// Récupérer l'ensemble des répertoires de l'utilisateur connecté.
        /// </summary>
        /// <returns>Le statut de réponse de la méthode.</returns>
        public abstract Task<List<Repository>> GetAllRepositories();

        /// <summary>
        /// Récupérer les détails de l'actuel répertoire de l'utilisateur connecté.
        /// </summary>
        /// <param name="owner">L'identifiant du propriétaire du répertoire.</param>
        /// <param name="repository">Le nom du répertoire.</param>
        /// <returns>Le statut de réponse de la méthode.</returns>
        public abstract Task<IActionResult> GetActualRepository(string owner, string repository);

        /// <summary>
        /// Récupérer l'ensemble des issues de l'utilisateur connecté.
        /// </summary>
        /// <returns>Le statut de réponse de la méthode.</returns>
        public abstract Task<List<Issue>> GetAllIssues(string owner, string repository);

        /// <summary>
        /// Récupérer l'ensemble des branches d'un répertoire de l'utilisateur connecté.
        /// </summary>
        /// <param name="owner">L'identifiant du propriétaire du répertoire.</param>
        /// <param name="repository">Le nom du répertoire.</param>
        /// <returns>Le statut de réponse de la méthode.</returns>
        public abstract Task<IActionResult> GetAllBranches(
            string owner,
            string repository
        );

        /// <summary>
        /// Récupérer les détails d'une branche d'un répertoire de l'utilisateur connecté.
        /// </summary>
        /// <param name="owner">L'identifiant du propriétaire du répertoire.</param>
        /// <param name="repository">Le nom du répertoire.</param>
        /// <param name="branch">Le nom de la branche.</param>
        /// <returns>Le statut de réponse de la méthode.</returns>
        public abstract Task<IActionResult> GetBranch(
            string owner,
            string repository,
            string branch
        );

        /// <summary>
        /// Récupérer l'ensemble des commits d'un répertoire de l'utilisateur connecté.
        /// </summary>
        /// <param name="owner">L'identifiant du propriétaire du répertoire.</param>
        /// <param name="repository">Le nom du répertoire.</param>
        /// <returns>Le statut de réponse de la méthode.</returns>
        public abstract Task<List<Commit>> GetAllCommits(string owner, string repository, string? branch = null);

        /// <summary>
        /// Récupérer les détails d'un commit d'un répertoire de l'utilisateur connecté.
        /// </summary>
        /// <param name="owner">L'identifiant du propriétaire du répertoire.</param>
        /// <param name="repository">Le nom du répertoire.</param>
        /// <param name="id">L'identifiant du commit.</param>
        /// <returns>Le statut de réponse de la méthode.</returns>
        public abstract Task<IActionResult> GetCommit(string owner, string repository, string id);

        /// <summary>
        /// Récupérer l'ensemble des fichiers d'un répertoire de l'utilisateur connecté.
        /// </summary>
        /// <param name="owner">L'identifiant du propriétaire du répertoire.</param>
        /// <param name="repository">Le nom du répertoire.</param>
        /// <param name="path">Le nom du répertoire.</param>
        /// <returns>Le statut de réponse de la méthode.</returns>
        public abstract Task<IActionResult> GetAllFiles(string owner, string repository, string? path, bool isFolder);
    }
}
