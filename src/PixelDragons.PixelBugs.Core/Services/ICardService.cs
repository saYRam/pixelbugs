using System;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Services
{
    public interface ICardService
    {
        /// <summary>
        /// Gets all the cards
        /// </summary>
        /// <returns>Returns an array of cards</returns>
        Card[] GetCards();
        
        /// <summary>
        /// Gets a list of users that can own cards
        /// </summary>
        /// <returns>Returns an array of users</returns>
        User[] GetUsersThatCanOwnCards();

        /// <summary>
        /// Saves an card and records the user that saved it
        /// </summary>
        /// <param name="card">The card to save</param>
        /// <param name="currentUser">The current user that is saving the card</param>
        /// <returns>Returns the saved card</returns>
        Card SaveCard(Card card, User currentUser);

        /// <summary>
        /// Gets all the card types available
        /// </summary>
        /// <returns>Returns an array of card types</returns>
        CardType[] GetCardTypes();

        /// <summary>
        /// Gets all the card statuses available
        /// </summary>
        /// <returns>Returns an array of card statuses</returns>
        CardStatus[] GetCardStatuses();

        /// <summary>
        /// Gets all the card priorities available
        /// </summary>
        /// <returns>Returns an array of card priorities</returns>
        CardPriority[] GetCardPriorities();

        /// <summary>
        /// Gets a card with the given id
        /// </summary>
        /// <param name="id">The id of the card to get</param>
        /// <returns>Returns the matching card</returns>
        Card GetCard(Guid id);
    }
}
