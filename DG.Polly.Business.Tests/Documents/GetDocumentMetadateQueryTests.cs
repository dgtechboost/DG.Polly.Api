using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using DG.Polly.Business.Documents.Queries.GetMetadata;
using DG.Polly.Contracts;
using Moq;
using NUnit.Framework;

namespace DG.Polly.Business.Tests.Documents
{
    public class GetDocumentMetadateQueryTests
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
        public async Task ShouldGetDocumentMetadata()
        {
            // Arrange
            var messageId = "12345";

            var documentMetadata = _fixture
                .Build<DocumentMetadataContract>()
                .With(x => x.MessageId, messageId)
                .Create();

            _mockDocumentsService
                .Setup(x => x.GetDocumentMetadataAsync(messageId))
                .ReturnsAsync(documentMetadata);

            var query = _fixture.Create<GetDocumentMetadateQuery>();

            // Act

            var result = await query.ExecuteAsync(messageId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.MessageId, documentMetadata.MessageId);
            Assert.AreEqual(result.Name, documentMetadata.Name);
            Assert.AreEqual(result.AdditionalInfo.AdditionalProp1, documentMetadata.AdditionalInfo.AdditionalProp1);
            Assert.AreEqual(result.AdditionalInfo.AdditionalProp2, documentMetadata.AdditionalInfo.AdditionalProp2);
            Assert.AreEqual(result.AdditionalInfo.AdditionalProp3, documentMetadata.AdditionalInfo.AdditionalProp3);
        }
    }
}
