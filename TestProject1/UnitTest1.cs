using System;
using System.Collections.Generic;
using Xunit;


public class DataProcessorTests
{
    // Тест 1: Проверка на правильность работы с валидными данными
    [Fact]
    public void GetMaxRateDifference_ValidData_ReturnsCorrectResult()
    {
        // Arrange
        var bankInfos = new List<BankInfo>
        {
            new BankInfo { Bank = "Беларусбанк", BuyRate = 2.3, SellRate = 2.6, Address = "Минск, ул. Ленина 5" },
            new BankInfo { Bank = "Приорбанк", BuyRate = 2.2, SellRate = 2.7, Address = "Гродно, ул. Советская 9" },
            new BankInfo { Bank = "Белагропромбанк", BuyRate = 2.1, SellRate = 2.4, Address = "Витебск, ул. Суворова 12" },
        };

        // Act
        double result = DataProcessor.GetMaxRateDifference(bankInfos);

        // Assert
        Assert.Equal(0.5, result, precision: 2); // Ожидаемая разница: 2.7 - 2.2 = 0.5
    }

    // Тест 2: Проверка с пустым списком (должно выбрасывать исключение)
    [Fact]
    public void GetMaxRateDifference_EmptyList_ThrowsArgumentException()
    {
        // Arrange
        var bankInfos = new List<BankInfo>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => DataProcessor.GetMaxRateDifference(bankInfos));
    }

    // Тест 3: Проверка с одним элементом в списке
    [Fact]
    public void GetMaxRateDifference_SingleElement_ReturnsCorrectDifference()
    {
        // Arrange
        var bankInfos = new List<BankInfo>
        {
            new BankInfo { Bank = "Беларусбанк", BuyRate = 2.0, SellRate = 2.5, Address = "Минск, ул. Ленина 5" }
        };

        // Act
        double result = DataProcessor.GetMaxRateDifference(bankInfos);

        // Assert
        Assert.Equal(0.5, result, precision: 2); // Разница между покупкой и продажей: 2.5 - 2.0 = 0.5
    }

    // Тест 4: Проверка на null список (должно выбрасывать исключение)
    [Fact]
    public void GetMaxRateDifference_NullList_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => DataProcessor.GetMaxRateDifference(null));
    }
}
