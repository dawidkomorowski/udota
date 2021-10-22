using System;

namespace UDota.CoreLib.OpenDota
{
    public sealed record Player
    {
        public int AccountId { get; init; }
        public string Name { get; init; }
        public Uri AvatarFull { get; init; }
        public DateTime LastMatchTime { get; init; }
    }
}