using CSharpFunctionalExtensions;
using Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Domian.Model.SharedKernel
{
    public class Location : ValueObject
    {
        private const int MAX_COORDINATE = 10;
        private const int MIN_COORDINATE = 1;

        public int X { get; private set; } 
        public int Y { get; private set; }

        [ExcludeFromCodeCoverage]
        private Location() { }

        private Location(int x, int y): this()
        {
            X = x;
            Y = y;
        }

        public static Location MaxLocation => new Location(MAX_COORDINATE, MAX_COORDINATE);
        public static Location MinLocation => new Location(MIN_COORDINATE, MIN_COORDINATE);

        public static Result<Location,Error> Create(int x, int y)
        {
            if (x < MinLocation.X || x > MaxLocation.X) return GeneralErrors.ValueIsRequired(nameof(x));
            if (y < MinLocation.Y || y > MaxLocation.Y) return GeneralErrors.ValueIsRequired(nameof(y));
            return new Location(x, y);
        }

        public static Location CreateRandom() =>
            new Location(new Random().Next(MinLocation.X, MaxLocation.X + 1), new Random().Next(MinLocation.Y, MaxLocation.Y + 1));
        

        public Result<int,Error> DistanceTo(Location traget)
        {
            if (traget == null) return GeneralErrors.ValueIsRequired(nameof(traget));
            return Math.Abs(X - traget.X) + Math.Abs(Y - traget.Y);
        }

        [ExcludeFromCodeCoverage]
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return X;
        }
    }
}
