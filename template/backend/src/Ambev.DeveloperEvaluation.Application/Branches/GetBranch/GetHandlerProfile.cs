using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch
{
    public class GetHandlerProfile: Profile
    {
        public GetHandlerProfile()
        {
            CreateMap<Branch, GetBranchResult>();
        }
    }
}
