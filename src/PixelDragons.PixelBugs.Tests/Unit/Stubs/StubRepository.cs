using System;
using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;

namespace PixelDragons.PixelBugs.Tests.Unit.Stubs
{
    /// <summary>
    /// A helper class which will return a populated stub
    /// </summary>
    public class StubRepository
    {
        private readonly List<object> repository;

        public StubRepository()
        {
            Guid cardId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid cardStatusId = Guid.NewGuid();
            Guid cardPriorityId = Guid.NewGuid();
            Guid cardTypeId = Guid.NewGuid();

            repository = new List<object>();

            repository.Add(new User { Id = userId, FirstName = "Andy", LastName = "Pike" });
            repository.Add(new CardStatus { Id = cardStatusId, Name = "Open" });
            repository.Add(new CardPriority { Id = cardPriorityId, Name = "High", Colour = "#ff0000" });
            repository.Add(new CardType { Id = cardTypeId, Name = "Story", Colour = "#0000ff" });
            repository.Add(new Card
                            {
                                Id = cardId,
                                Title = "A card title",
                                Body = "A card body",
                                Number = 1,
                                Points = 2.5f,
                                Status = GetStub<CardStatus>(),
                                Priority = GetStub<CardPriority>(),
                                Type = GetStub<CardType>(),
                                Owner = GetStub<User>()
                            });

            repository.Add(new UserDTO { Id = userId, FullName = "Andy Pike" });
            repository.Add(new CardStatusDTO { Id = cardStatusId, Name = "Closed" });
            repository.Add(new CardPriorityDTO { Id = cardPriorityId, Name = "Low", Colour = "#ff00ff" });
            repository.Add(new CardTypeDTO { Id = cardTypeId, Name = "Low", Colour = "#ff00ff" });
            repository.Add(new CardDetailsDTO
                               {
                                   Id = cardId,
                                   Title = "A new card title from a dto",
                                   Body = "A card body from a dto",
                                   Points = 5.0f,
                                   Number = 1,
                                   Status = GetStub<CardStatusDTO>(),
                                   Owner = GetStub<UserDTO>(),
                                   Priority = GetStub<CardPriorityDTO>(),
                                   Type = GetStub<CardTypeDTO>()
                               });
        }

        public T GetStub<T>()
        {
            foreach(object dto in repository)
            {
                if (dto is T)
                    return (T)dto;
            }

            throw new TypeLoadException(string.Format("Unable to find stub '{0}' in repository", typeof(T).FullName));
        }
    }
}