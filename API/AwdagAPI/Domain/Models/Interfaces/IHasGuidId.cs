using System;

namespace Domain.Models.Interfaces
{
    public interface IHasGuidId
    {
        public Guid Id { get; set; }
    }
}