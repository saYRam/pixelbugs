using System;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Services
{
    public interface IIssueService
    {
        /// <summary>
        /// Gets all the issues
        /// </summary>
        /// <returns>Returns an array of issues</returns>
        Issue[] GetAllIssues();
        
        /// <summary>
        /// Gets a list of users that can own issues
        /// </summary>
        /// <returns>Returns an array of users</returns>
        User[] GetUsersThatCanOwnIssues();

        /// <summary>
        /// Saves an issue and records the user that saved it
        /// </summary>
        /// <param name="issue">The issue to save</param>
        /// <param name="currentUser">The current user that is saving the issue</param>
        /// <returns>Returns the saved issue</returns>
        Issue SaveIssue(Issue issue, User currentUser);
    }
}
