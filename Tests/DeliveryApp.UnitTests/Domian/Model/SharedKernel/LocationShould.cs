using DeliveryApp.Core.Domian.Model.SharedKernel;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DeliveryApp.UnitTests.Domian.Model.SharedKernel
{
    public class LocationShould
    {
        [Theory]
        [InlineData(1,1)]
        [InlineData(2,9)]
        public void BeCorrectWhenParamsAreCorrectOnCreated(int x, int y)
        {
            var location = Location.Create(x, y);
            location.IsSuccess.Should().BeTrue();
            location.Value.X.Should().Be(x);
            location.Value.Y.Should().Be(y);
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(2, -9)]
        [InlineData(0, 1)]
        [InlineData(2,0)]
        [InlineData(3, 20)]
        [InlineData(21, 2)]
        public void ReturnErrorWhenParamsAreNotCorrectOnCreated(int x, int y)
        {
            var location = Location.Create(x, y);
            location.IsSuccess.Should().BeFalse();
            location.Error.Should().NotBeNull();           
        }

        [Fact]
        public void  CanCareteRandomLocation()
        {
            Location location = Location.CreateRandom();
            location.Should().NotBeNull();
            location.X.Should().BeGreaterThanOrEqualTo(1).And.BeLessThanOrEqualTo(10);
            location.Y.Should().BeGreaterThanOrEqualTo(1).And.BeLessThanOrEqualTo(10);
        }

        [Fact]
        public void BeNotEqualWhenAllPropertiesIsEqual()
        {
            Location firstLocation = Location.Create(2, 2).Value;
            Location secondLocation = Location.Create(2, 2).Value;
            (firstLocation == secondLocation).Should().BeTrue();
        }

        [Fact]
        public void BeNotEqualWhenOneOfPropertiesIsNotEqual()
        {
            Location firstLocation = Location.Create(2, 2).Value;
            Location secondLocation = Location.Create(2, 10).Value;
            (firstLocation != secondLocation).Should().BeFalse();
        }
        public static IEnumerable<object[]> GetFakeLocations()
        {
            yield return [Location.Create(1, 1).Value, 0];
            yield return [Location.Create(1, 2).Value, 1];
            yield return [Location.Create(3, 1).Value, 2];
            yield return [Location.Create(10, 10).Value, 18];
        }

        [Theory]
        [MemberData(nameof(GetFakeLocations))]
        public void ReturnDistanceBetweenTwoLocations(Location anotherLocation, int distance)
        {
            Location location = Location.Create(1, 1).Value;       
            var result = location.DistanceTo(anotherLocation);
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(distance);
        }

    }
}
