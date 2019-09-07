using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using SustainableVendingMachine.Domain.Enitity;
using SustainableVendingMachine.Domain.UseCases;
using SustainableVendingMachine.Domain.UseCases.Result;
using Xunit;

namespace SustainableVendingMachine.Domain.Tests.UseCases
{
    public class PurchaseProductUseCaseTests
    {
        [Fact]
        public void InsertCoin_When_FiftyCentCoinInserted_Then_ReturnSuccess()
        {
            //Arrange
            var coin = Coin.FiftyCent;
            var sut = new PurchaseProductUseCase();
            
            //Act
            var actual = sut.InsertCoin(coin);

            //Assert
            actual.HasFailed.Should().BeFalse();
        }
    }
}
