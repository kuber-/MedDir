using System;
using Xunit;
using Web.Services;

namespace Tests.Services
{
    public class PatientGroupsServiceShould
    {
        private readonly IPatientGroupsService sut = new PatientGroupsService();

        [Fact]
        public void ThrowWhenNullData() {
            Assert.Throws<ArgumentNullException>(() => sut.Calculate(null));
        }

        [Fact]
        public void ThrowWhenOutOfRangeData() {
            var outOfRangeData = new int[,] {
                { 1, 1, 1, 0, 1},
                { 0, 0, 0, 2, 0}
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Calculate(outOfRangeData));
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Calculate(int[,] matrix, int numberOfGroups) {
            Assert.Equal(numberOfGroups, sut.Calculate(matrix));
        }

        public static TheoryData<int[,], int> Data => new TheoryData<int[,], int>
        {
            {
                new int[,] {
                    {1, 1, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0},
                    {1, 0, 1, 0, 0, 0},
                    {0, 0, 0, 0, 1, 0},
                    {0, 0, 0, 0, 0, 1},
                    {1, 1, 0, 1, 0, 0}
                }, 4
            },
            {
                new int[,] {
                    {1, 1, 0}, {0, 1, 0}, {1, 0, 1}, {0, 0, 0}, {0, 0, 0}, {1, 1, 0}
                }, 2
            },
            {
                new int[,] {
                    {1}
                }, 1
            },
            {
                new int[,] {
                    {0}
                }, 0
            },
            // row only groups
            {
                new int[,] {
                    {1, 1, 1, 1, 1}
                }, 1
            },
            {
                new int[,] {
                    {0, 0, 0, 0, 0}
                }, 0
            },
            // column only groups
            {
                new int[,] {
                    {1}, {1}, {1}
                }, 1
            },
            {
                new int[,] {
                    {0}, {0}, {0}
                }, 0
            },
            // diag only groups
            {
                new int[,] {
                    {1, 0, 0}, {0, 1, 0}, {0, 0, 1},
                }, 1
            },
            // no groups
            {
                new int[,] { }, 0
            },
            {
                new int[,] {
                    {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0},
                }, 0
            },
            // one group
            {
                new int[,] {
                    {1, 1}, {1, 1}, {1, 1}, {1, 1},
                }, 1
            },
            {
                new int[,] { {1, 1}, {1, 1}}, 1
            }
        };
    }
}
