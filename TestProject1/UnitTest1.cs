using System;
using System.Collections.Generic;
using Xunit;


public class DataProcessorTests
{
    // ���� 1: �������� �� ������������ ������ � ��������� �������
    [Fact]
    public void GetMaxRateDifference_ValidData_ReturnsCorrectResult()
    {
        // Arrange
        var bankInfos = new List<BankInfo>
        {
            new BankInfo { Bank = "�����������", BuyRate = 2.3, SellRate = 2.6, Address = "�����, ��. ������ 5" },
            new BankInfo { Bank = "���������", BuyRate = 2.2, SellRate = 2.7, Address = "������, ��. ��������� 9" },
            new BankInfo { Bank = "���������������", BuyRate = 2.1, SellRate = 2.4, Address = "�������, ��. �������� 12" },
        };

        // Act
        double result = DataProcessor.GetMaxRateDifference(bankInfos);

        // Assert
        Assert.Equal(0.5, result, precision: 2); // ��������� �������: 2.7 - 2.2 = 0.5
    }

    // ���� 2: �������� � ������ ������� (������ ����������� ����������)
    [Fact]
    public void GetMaxRateDifference_EmptyList_ThrowsArgumentException()
    {
        // Arrange
        var bankInfos = new List<BankInfo>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => DataProcessor.GetMaxRateDifference(bankInfos));
    }

    // ���� 3: �������� � ����� ��������� � ������
    [Fact]
    public void GetMaxRateDifference_SingleElement_ReturnsCorrectDifference()
    {
        // Arrange
        var bankInfos = new List<BankInfo>
        {
            new BankInfo { Bank = "�����������", BuyRate = 2.0, SellRate = 2.5, Address = "�����, ��. ������ 5" }
        };

        // Act
        double result = DataProcessor.GetMaxRateDifference(bankInfos);

        // Assert
        Assert.Equal(0.5, result, precision: 2); // ������� ����� �������� � ��������: 2.5 - 2.0 = 0.5
    }

    // ���� 4: �������� �� null ������ (������ ����������� ����������)
    [Fact]
    public void GetMaxRateDifference_NullList_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => DataProcessor.GetMaxRateDifference(null));
    }
}
