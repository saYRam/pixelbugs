using System;
using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;

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
            repository = new List<object>();

            repository.Add(new User {Id = Guid.NewGuid(), FirstName = "Andy", LastName = "Pike"});
            repository.Add(new CardStatus { Id = Guid.NewGuid(), Name = "Open" });
            repository.Add(new CardPriority { Id = Guid.NewGuid(), Name = "High", Colour = "#ff0000" });
            repository.Add(new CardType { Id = Guid.NewGuid(), Name = "Story", Colour = "#0000ff" });
            repository.Add(new Card
                            {
                                Id = Guid.NewGuid(),
                                CreatedDate = DateTime.Now,
                                CreatedBy = GetStub<User>(),
                                Title = "A card title",
                                Body = "A card body",
                                Number = 1,
                                Points = 2.5f,
                                Status = GetStub<CardStatus>(),
                                Priority = GetStub<CardPriority>(),
                                Type = GetStub<CardType>(),
                                Owner = GetStub<User>()
                            });
        }

        public T GetStub<T>()
        {
            foreach(object dto in repository)
            {
                if (dto is T)
                    return (T)dto;
            }

            throw new TypeLoadException(string.Format("Unable to find DTO '{0}' in repository", typeof(T).FullName));
        }
    }
}