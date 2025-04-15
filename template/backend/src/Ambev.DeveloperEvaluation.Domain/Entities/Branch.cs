using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a company branch with name, active status, and timestamps.
    /// </summary>
    public class Branch : BaseEntity
    {
        /// <summary>
        /// The name of the branch.
        /// </summary>
        public required string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the branch is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The date and time when the branch was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The date and time when the branch was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Branch"/> class.
        /// Sets the creation date and marks the branch as active by default.
        /// </summary>
        public Branch()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        /// <summary>
        /// Marks the branch as inactive and updates the timestamp.
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Validates the current branch instance using <see cref="BranchValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing the validation result and any errors.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new BranchValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
            };
        }
    }
}