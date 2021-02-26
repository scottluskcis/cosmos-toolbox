using CosmosToolbox.Core.Data;
using CosmosToolbox.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CosmosToolbox.App.Strategy.Tests
{
    [TestClass()]
    public class CreateContainersStrategyTests
    {
        private ClientContextOptionsGroup testClientContextOptionsgroup;

        private CreateContainersStrategy testCreateContainersStrategy;

        private readonly Mock<IClientContextFactory> mockClientContextFactory = new Mock<IClientContextFactory>();

        private readonly Mock<ILogger<CreateContainersStrategy>> mockLogger = new Mock<ILogger<CreateContainersStrategy>>();

        private readonly Mock<IApplicationArgs> mockApplicationArgs = new Mock<IApplicationArgs>();

        private readonly Mock<IOptions<ClientContextOptionsGroup>> mockOptions = new Mock<IOptions<ClientContextOptionsGroup>>();

        [TestInitialize]
        public void Startup()
        {
            testCreateContainersStrategy = new CreateContainersStrategy(mockOptions.Object, mockClientContextFactory.Object, mockLogger.Object);
        }

        [TestMethod]
        [DataRow(true, true, true, "Should be true when IApplicationArgs carries CreateDatabase and CreateContainers")]
        [DataRow(false, true, true, "Should be true when IApplicationArgs carries at least CreateContainers")]
        [DataRow(false, false, false, "Should be false when IApplicationArgs does not carry CreateCDatabase or CreateContainers")]
        [DataRow(true, false, true, "Should be true when IApplicationArgs carries at least CreateDatabase")]
        public void IsApplicableTest(bool createDatabase, bool createContainers, bool expectedResult, string message)
        {
            // Arrange
            mockApplicationArgs.Setup(m => m.CreateDatabase).Returns(createDatabase);
            mockApplicationArgs.Setup(m => m.CreateContainers).Returns(createContainers);

            // Act
            bool actualResult = testCreateContainersStrategy.IsApplicable(mockApplicationArgs.Object);

            // Assert
            Assert.AreEqual(expectedResult, actualResult, message);
        }

        [TestMethod]
        [DataRow(true, true, false, "Shouldn't be false when IApplicationArgs carries CreateDatabase and CreateContainers")]
        [DataRow(false, true, false, "Shouldn't be false when IApplicationArgs carries at least CreateContainers")]
        [DataRow(false, false, true, "Shouldn't be true when IApplicationArgs does not carry CreateCDatabase or CreateContainers")]
        [DataRow(true, false, false, "Shouldn't be false when IApplicationArgs carries at least CreateDatabase")]
        public void IsNotApplicableTest(bool createDatabase, bool createContainers, bool expectedResult, string message)
        {
            // Arrange
            mockApplicationArgs.Setup(m => m.CreateDatabase).Returns(createDatabase);
            mockApplicationArgs.Setup(m => m.CreateContainers).Returns(createContainers);

            // Act
            bool actualResult = testCreateContainersStrategy.IsApplicable(mockApplicationArgs.Object);

            // Assert
            Assert.AreNotEqual(expectedResult, actualResult, message);
        }
    }
}