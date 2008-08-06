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
        /// Gets a card with the given identifier
        /// </summary>
        /// <param name="request">The request that contains the id of the card to get</param>
        /// <returns>Returns the matching card</returns>
        RetrieveCardResponse RetrieveCard(RetrieveCardRequest request);

        /// <summary>
        /// Changes the status of the supplied card
        /// </summary>
        /// <param name="request">The request that contains the id of the card to update and the id of the new status</param>
        void ChangeCardStatus(ChangeCardStatusRequest request);

        /// <summary>
        /// Saves a card
        /// </summary>
        /// <param name="request">The request that contains details of the card to save</param>
        void SaveCard(SaveCardRequest request);
    }
}