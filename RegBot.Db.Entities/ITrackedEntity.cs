using System;

namespace RegBot.Db.Entities
{
    public interface ITrackedEntity
    {
        string ChangeBy { get; set; }
        DateTime? ChangeAt { get; set; }
    }
}