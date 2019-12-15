using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using DG.Polly.Business.Documents.Queries.GetStatus;
using DG.Polly.Contracts;
using Moq;
using NUnit.Framework;

namespace DG.Polly.Business.Tests.Documents
{
    public class GetDocumentStatusQueryTests
    {
        private IFixture _fixture;
        private Mock<IDocumentsService> _mockDocumentsService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _mockDocumentsService = _fixture.Freeze<Mock<IDocumentsService>>();
        }

        [Test]
        public async Task ShouldGetDocumentStatus()
        {
            // Arrange
            var messageId = "12345";

            var document = _fixture
                .Build<DocumentStatusContract>()
                .With(x=>x.MessageId, messageId)
                .Create();

            _mockDocumentsService
                .Setup(x => x.GetDocumentStatusAsync(messageId))
                .ReturnsAsync(document);

            var query = _fixture.Create<GetDocumentStatusQuery>();

            // Act

            var result = await query.ExecuteAsync(messageId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.MessageId, document.MessageId);
            Assert.AreEqual(result.Status, document.Status);
        }
    }
}
