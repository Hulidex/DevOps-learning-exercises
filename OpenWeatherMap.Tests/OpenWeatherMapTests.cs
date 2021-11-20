using Xunit;
using OpenWeatherMap;
using System.Collections.Generic;
using System.IO;

namespace OpenWeatherMap.Tests;

public class OpenWeatherMapTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void Test(List<string> args, int expectedResult)
    {
        // Redirect Output and Error to dummy places
        System.Console.SetOut(new StringWriter());
        System.Console.SetError(new StringWriter());

        var result = WeatherApp.Main(args.ToArray());

        Assert.True(result.Equals(expectedResult));
    }

    #region TestData
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[]
            {
                new List<string>()
                {
                    "--Lat",
                    "23.3343",
                    "--Lon",
                    "23.3356"
                },
                0
            },
            new object[]
            {
                new List<string>()
                {
                    "--Lat"
                },
                1
            },
            new object[]
            {
                new List<string>()
                {
                    "--Lat",
                    "23.33",
                    "--Lon",
                },
                1
            },
            new object[]
            {
                new List<string>()
                {
                    "--Lat",
                    "23.33",
                },
                1
            },
        };
    #endregion
}