﻿using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
    public class ListUsersProfile: Profile
    {
        public ListUsersProfile()
        {
            CreateMap<User, ListUsersResponse>();
            CreateMap<ListUsersQuery, ListUsersCommand>();
        }
    }
}
