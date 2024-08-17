using Xunit;
using NSubstitute;
using System.Collections.Generic;
using app.machine;
using app.symbols;
using app.user;

namespace app.tests.machine
{
    public class MachineServiceTests
    {
        [Fact]
        public void Spin_ShouldReturnValidLine()
        {
            // Arrange
            var symbols = new List<SymbolModel>
            {
                new SymbolModel("A", 0.3, 1, 'A'),
                new SymbolModel("B", 0.2, 2, 'B'),
                new SymbolModel("C", 0.5, 3, 'C')
            };

            var machineService = new MachineService(Substitute.For<IUserService>());

            // Act
            var result = machineService.Spin(symbols);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Contains(result[0], symbols);
            Assert.Contains(result[1], symbols);
            Assert.Contains(result[2], symbols);
        }

        [Theory]
        [InlineData("A", 0.4, 0.45, 'A', 1, 10, 8)]
        [InlineData("B", 0.3, 0.35, 'B', 2, 5, 1)]
        [InlineData("C", 0.2, 0.25, 'C', 0, 20, 12)]
        public void CalculateWinnings_Should_Return_Correct_Winnings(string symbolName, double symbolPayout, double symbolProbability, char symbolChar, int numberOfWildCards, int stake, int expectedWinnings)
        {
            // Arrange
            var symbol = new SymbolModel(symbolName, symbolPayout, symbolProbability, symbolChar);

            var machineService = new MachineService(Substitute.For<IUserService>());

            // Act
            var actualWinnings = machineService.CalculateWinnings(symbol, numberOfWildCards, stake);

            // Assert
            Assert.Equal(expectedWinnings, actualWinnings);
        }

        [Fact]
        public void CheckLine_AllSymbolsAreSame_ReturnsTrue()
        {
            // Arrange
            var line = new List<SymbolModel>
            {
                new SymbolModel("A", 1, 0.25, 'A'),
                new SymbolModel("A", 1, 0.25, 'A'),
                new SymbolModel("A", 1, 0.25, 'A')
            };

            var machineService = new MachineService(Substitute.For<IUserService>());

            // Act
            bool result;
            SymbolModel winSymbol;
            int numberOfWildcards;
            machineService.CheckLine(line, out result, out winSymbol, out numberOfWildcards);

            // Assert
            Assert.True(result);
            Assert.Equal(line[0], winSymbol);
            Assert.Equal(0, numberOfWildcards);
        }

        [Fact]
        public void CheckLine_TwoNonWildcardSymbolsAreSame_ReturnsTrue()
        {
            // Arrange
            var line = new List<SymbolModel>
            {
                new SymbolModel("A", 1, 0.25, 'A'),
                new SymbolModel("B", 2, 0.25, 'B'),
                new SymbolModel("A", 1, 0.25, 'A')
            };

            var machineService = new MachineService(Substitute.For<IUserService>());

            // Act
            bool result;
            SymbolModel winSymbol;
            int numberOfWildcards;
            machineService.CheckLine(line, out result, out winSymbol, out numberOfWildcards);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckLine_OneNonWildcardSymbol_ReturnsTrue()
        {
            // Arrange
            var line = new List<SymbolModel>
            {
                new SymbolModel("A", 1, 0.25, 'A'),
                new SymbolModel("*", 0, 0.25, '*'),
                new SymbolModel("A", 1, 0.25, 'A')
            };

            var machineService = new MachineService(Substitute.For<IUserService>());

            // Act
            bool result;
            SymbolModel winSymbol;
            int numberOfWildcards;
            machineService.CheckLine(line, out result, out winSymbol, out numberOfWildcards);

            // Assert
            Assert.True(result);
            Assert.Equal(line[0], winSymbol);
            Assert.Equal(2, numberOfWildcards);
        }

        [Fact]
        public void CheckLine_NoMatchingSymbols_ReturnsFalse()
        {
            // Arrange
            var line = new List<SymbolModel>
            {
                new SymbolModel("A", 1, 0.25, 'A'),
                new SymbolModel("B", 2, 0.25, 'B'),
                new SymbolModel("C", 3, 0.25, 'C')
            };

            var machineService = new MachineService(Substitute.For<IUserService>());

            // Act
            bool result;
            SymbolModel winSymbol;
            int numberOfWildcards;
            machineService.CheckLine(line, out result, out winSymbol, out numberOfWildcards);

            // Assert
            Assert.False(result);
        }


    }
}
