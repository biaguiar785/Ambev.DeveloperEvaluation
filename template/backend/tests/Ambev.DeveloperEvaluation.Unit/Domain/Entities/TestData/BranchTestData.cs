using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class BranchTestData
    {
        private static readonly Faker<Branch> BranchFaker = new Faker<Branch>()
            .RuleFor(x => x.Name, f => f.Company.CompanyName())
            .RuleFor(x => x.IsActive, true)
            .RuleFor(x => x.CreatedAt, f => f.Date.Past());

        public static Branch GenerateBranch() => BranchFaker.Generate();
    }
}
