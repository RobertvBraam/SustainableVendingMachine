using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using SustainableVendingMachine.Domain.Enitities;
using SustainableVendingMachine.Domain.UseCases;
using Xunit;

namespace SustainableVendingMachine.Domain.Tests.UseCases
{
    public class PurchaseProductUseCaseTests
    {
        [Fact]
        public void InsertCoin_When_FiftyCentCoinInserted_Then_ReturnSuccess()
        {
            //Arrange
            decimal expectedCurrentAmount = 0.50m;
            var vendingMachine = new VendingMachine();
            var coin = Coin.FiftyCent;
            var sut = new PurchaseProductUseCase(vendingMachine);
            
            //Act
            var actual = sut.InsertCoin(coin);

            //Assert
            actual.HasFailed.Should().BeFalse();
            actual.CurrentAmount.Should().Be(expectedCurrentAmount);
        }

        [Fact]
        public void InsertCoin_When_TwoFiftyCentCoinInserted_Then_ReturnSuccess()
        {
            //Arrange
            decimal expectedCurrentAmount = 1.00m;
            var vendingMachine = new VendingMachine();
            var coin = Coin.FiftyCent;
            var sut = new PurchaseProductUseCase(vendingMachine);
            
            //Act
            sut.InsertCoin(coin);
            var actual = sut.InsertCoin(coin);

            //Assert
            actual.HasFailed.Should().BeFalse();
            actual.CurrentAmount.Should().Be(expectedCurrentAmount);
        }

        [Fact]
        public void PurchaseProduct_When_ChickenSoupProductPurchased_Then_ReturnSuccess()
        {
            //Arrange
            var vendingMachine = new VendingMachine();
            var coin = Product.ChickenSoup;
            var sut = new PurchaseProductUseCase(vendingMachine);

            //Act
            var actual = sut.PurchaseProduct(coin);

            //Assert
            actual.HasFailed.Should().BeFalse();
        }

        [Fact]
        public void CancelPurchase_When_PurchaseCanceled_Then_ReturnSuccess()
        {
            //Arrange
            var vendingMachine = new VendingMachine();
            var sut = new PurchaseProductUseCase(vendingMachine);

            //Act
            var actual = sut.CancelPurchase();

            //Assert
            actual.HasFailed.Should().BeFalse();
        }

    }
}
