using System;

namespace FA.JustBlogAPI.Models.BaseEntities
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        bool IsDeleted { get; set; }

        DateTime InsertedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}
