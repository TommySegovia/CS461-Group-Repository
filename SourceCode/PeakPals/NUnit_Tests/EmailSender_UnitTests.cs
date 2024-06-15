using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SendGrid;
using SendGrid.Helpers.Mail;
using WebPWrecover.Services;

namespace NUnit_Tests
{
    public class EmailSenderTests
    {
        private Mock<ILogger<EmailSender>> _loggerMock;
        private AuthMessageSenderOptions _options;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<EmailSender>>();
            _options = new AuthMessageSenderOptions
            {
                SendGridKey = "fake-key"
            };
        }

        [Test]
        public async Task SendEmailAsync_NullSendGridKey_ThrowsException()
        {
            // Arrange
            _options.SendGridKey = null;
            var emailSender = new EmailSender(Options.Create(_options), _loggerMock.Object);
            var toEmail = "test@example.com";
            var subject = "Confirm your email";
            var message = "https://example.com/confirm";

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(() => emailSender.SendEmailAsync(toEmail, subject, message));
            Assert.AreEqual("Null SendGridKey", exception.Message);
        }

        [Test]
        public async Task Execute_InvalidSubject_ThrowsException()
        {
            // Arrange
            var emailSender = new EmailSender(Options.Create(_options), _loggerMock.Object);
            var apiKey = "fake-key";
            var subject = "Invalid subject";
            var message = "https://example.com/invalid";

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(() => emailSender.Execute(apiKey, subject, message, "test@example.com"));
            Assert.AreEqual("Invalid subject", exception.Message);
        }

        /*
        [Test]
        public async Task Execute_ConfirmEmail_SendsEmail()
        {
            // Arrange
            var emailSender = new EmailSender(Options.Create(_options), _loggerMock.Object);
            var apiKey = "fake-key";
            var subject = "Confirm your email";
            var message = "https://example.com/confirm";
            var toEmail = "test@example.com";

            // Act
            await emailSender.Execute(apiKey, subject, message, toEmail);

            // Assert
            Assert.AreEqual(1, _loggerMock.Invocations.Count);

        }
        */

    }
}
