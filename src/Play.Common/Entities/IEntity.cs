using System;

namespace Play.Common.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
}