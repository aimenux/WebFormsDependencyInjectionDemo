using System;
using System.Linq;

namespace Domain
{
    public class DummyService : IDummyService
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        public string GetInfos()
        {
            const int length = 10;
            const string chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var infos = Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)])
                .ToArray();
            return new string(infos);
        }
    }
}