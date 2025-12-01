using Application.DTOs.Customer;
using Domain.Model;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IAuthenticationService
    {
        Task<Result<CustomerViewDTO>> Login(CustomerLoginDTO loginDTO);
        Task<Result<CustomerViewDTO>> RegisterAsync(CustomerRegisterDTO registerDTO);

        Task<Result<CustomerViewDTO>> GetByEmailAsync(string email);
    }
}
