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
            var productSlots = new List<ProductSlot>();
            var purse = new List<CoinSlot>();
            var vendingMachine = new VendingMachine(productSlots, purse);
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
            var productSlots = new List<ProductSlot>();
            var purse = new List<CoinSlot>();
            var vendingMachine = new VendingMachine(productSlots, purse);
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
        public void InsertCoin_When_ThreeOneEuroCoinInserted_Then_ReturnSuccess()
        {
            //Arrange
            decimal expectedCurrentAmount = 2.00m;
            var productSlots = new List<ProductSlot>();
            var purse = new List<CoinSlot>();
            var vendingMachine = new VendingMachine(productSlots, purse);
            var coin = Coin.OneEuro;
            var sut = new PurchaseProductUseCase(vendingMachine);
            
            //Act
            sut.InsertCoin(coin);
            sut.InsertCoin(coin);
            var actual = sut.InsertCoin(coin);

            //Assert
            actual.HasFailed.Should().BeTrue();
            actual.CurrentAmount.Should().Be(expectedCurrentAmount);
        }

        [Fact]
        public void PurchaseProduct_When_TwoOneEuroCoinsAddedChickenSoupProductPurchased_Then_ReturnSuccess()
        {
            //Arrange
            var coin = Coin.OneEuro;
            var product = Product.ChickenSoup;
            var coinReturned = new List<Coin> { Coin.TwentyCent };
            var productSlots = new List<ProductSlot>();
            var purse = new List<CoinSlot>();
            var vendingMachine = new VendingMachine(productSlots, purse);
            var sut = new PurchaseProductUseCase(vendingMachine);

            //Act
            sut.InsertCoin(coin);
            sut.InsertCoin(coin);
            var actual = sut.PurchaseProduct(product);

            //Assert
            actual.HasFailed.Should().BeFalse();
            actual.ProductPurchased.Should().Be(product);
            actual.CoinsReturned.Should().BeEquivalentTo(coinReturned);
        }

        [Fact]
        public void CancelPurchase_When_PurchaseCanceled_Then_ReturnSuccess()
        {
            //Arrange
            var productSlots = new List<ProductSlot>();
            var purse = new List<CoinSlot>();
            var vendingMachine = new VendingMachine(productSlots, purse);
            var sut = new PurchaseProductUseCase(vendingMachine);

            //Act
            var actual = sut.CancelPurchase();

            //Assert
            actual.HasFailed.Should().BeFalse();
        }

        [Fact]
        public void CancelPurchase_When_TenCentCoinAddedAndPurchaseCanceled_Then_ReturnSuccess()
        {
            //Arrange
            var coin = Coin.TwentyCent;
            var coinsReturned = new List<Coin> { coin };
            var productSlots = new List<ProductSlot>();
            var purse = new List<CoinSlot>();
            var vendingMachine = new VendingMachine(productSlots, purse);
            var sut = new PurchaseProductUseCase(vendingMachine);

            //Act
            sut.InsertCoin(coin);
            var actual = sut.CancelPurchase();

            //Assert
            actual.HasFailed.Should().BeFalse();
            actual.CoinsReturned.Should().BeEquivalentTo(coinsReturned);
        }

        [Fact]
        public void CancelPurchase_When_TwoTenCentCoinsAddedAndPurchaseCanceled_Then_ReturnSuccess()
        {
            //Arrange
            var coin = Coin.TwentyCent;
            var coinsReturned = new List<Coin> { coin, coin };
            var productSlots = new List<ProductSlot>();
            var purse = new List<CoinSlot>();
            var vendingMachine = new VendingMachine(productSlots, purse);
            var sut = new PurchaseProductUseCase(vendingMachine);

            //Act
            sut.InsertCoin(coin);
            sut.InsertCoin(coin);
            var actual = sut.CancelPurchase();

            //Assert
            actual.HasFailed.Should().BeFalse();
            actual.CoinsReturned.Should().BeEquivalentTo(coinsReturned);
        }
    }
}
