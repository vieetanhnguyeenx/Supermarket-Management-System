﻿using DataAccess.DTOs;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Repository
{
    public interface IEmployeeRepository
    {
        Task<IdentityResult> SigUpAsyn(EmployeeSignUpModel user);
        Task<EmployeeSignInResponse> SignInAsyn(EmployeeSignInModel user);

    }
}
