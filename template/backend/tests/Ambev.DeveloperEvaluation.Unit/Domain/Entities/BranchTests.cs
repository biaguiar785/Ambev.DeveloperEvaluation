using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class BranchTests
    {
        [Fact(DisplayName = "Should desactivate a branch correctly")]
        public void ShouldDeactivateBranchCorrectly()
        {
            // Arrange
            var branch = BranchTestData.GenerateBranch();

            // Act
            branch.Deactivate();

            // Assert
            Assert.False(branch.IsActive);
            Assert.NotNull(branch.UpdatedAt);
        }

        [Fact(DisplayName = "Should validate a branch correctly")]
        public void ShouldValidateBranchCorrectly()
        {
            // Arrange
            var branch = BranchTestData.GenerateBranch();
            // Act
            var result = branch.Validate();
            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "Should not validate a branch with empty name")]
        public void ShouldUpdateBranchCorrectly()
        {
            // Arrange
            var branch = BranchTestData.GenerateBranch();
            branch.Name = string.Empty;

            //Act
            var result = branch.Validate();

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Detail.Contains("Name of Branch is required."));
        }

        [Fact(DisplayName = "Should not validate a branch with name too long")]
        public void ShouldDeleteBranchCorrectly()
        {
            // Arrange
            var branch = BranchTestData.GenerateBranch();
            branch.Name = new string('a', 101);

            //Act
            var result = branch.Validate();

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Detail.Contains("Name of branch cannot exceed 100 characters."));
        }
    }
}
