namespace Melior.InterviewQuestion.Unit.Tests;

[TestClass]
public class PaymentSchemeHelperTests
{
    private static IPaymentSchemeHelper CreateSut => new PaymentSchemeHelper();

    [TestMethod]
    public void HandleBacsPaymentScheme_ReturnsUnsuccessful()
    {
        var sut = CreateSut;

        var result = sut.HandleBacsPaymentScheme(new Account());

        result.Success.Should().BeFalse();
    }

    [TestMethod]
    public void HandleFasterPaymentsPaymentScheme_ReturnsUnsuccessful()
    {
        var sut = CreateSut;

        var result = sut.HandleFasterPaymentsPaymentScheme(new Account(), new MakePaymentRequest());

        result.Success.Should().BeFalse();
    }

    [TestMethod]
    public void HandleChapsPaymentScheme_ReturnsUnsuccessful()
    {
        var sut = CreateSut;

        var result = sut.HandleChapsPaymentScheme(new Account());

        result.Success.Should().BeFalse();
    }
}