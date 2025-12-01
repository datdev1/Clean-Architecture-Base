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
    public interface IAuthenticationService : IBaseService<Customer>
    {
        Task<Result<CustomerDTO>> Login(CustomerLoginDTO loginDTO);
        Task<Result<CustomerDTO>> RegisterAsync(CustomerRegisterDTO registerDTO);

        Task<Result<CustomerDTO>> GetByEmailAsync(string email);
    }
}
