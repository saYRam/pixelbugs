using System;
using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Services
{
    public interface ICardService
    {
        /// <summary>
        /// Gets all the available options required when creating a new card
        /// </summary>
        /// <returns>Returns a response containing the options</returns>
        RetrieveCardOptionsResponse RetrieveCardOptions();

        /// <summary>
        /// Gets all cards for the card wall together with all status lanes
        /// </summary>
        /// <returns>Returns a response containing the wall information</returns>
        RetrieveWallResponse RetrieveWall();

        /// <summary>
        /// Saves an card and records the user that saved it
        /// </summary>
        /// <param name="card">The card to save</param>
        /// <param name="userId">The id of the user that is saving this card</param>
        /// <returns>Returns the saved card</returns>
        Card SaveCard(Card card, Guid userId);

        /// <summary>
        /// Gets a card with the given id
        /// </summary>
        /// <param name="id">The id of the card to get</param>
        /// <returns>Returns the matching card</returns>
        Card GetCard(Guid id);

        /// <summary>
        /// Changes the status of the supplied card
        /// </summary>
        /// <param name="cardId">The id of the card to update</param>
        /// <param name="statusId">The id of the new status to apply to the card</param>
        /// <returns>Returns the updated card</returns>
        Card ChangeCardStatus(Guid cardId, Guid statusId);
    }
}