using Application.Common.Errors;
using Application.Common.Extenstions;
using Application.DTOs.Customer;
using Application.Service.Interface;
using AutoMapper;
using Domain.Model;
using FluentResults;
using FluentValidation;
using Infrastructure.Interface.UnitOfWork;


namespace Application.Service.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerRegisterDTO> _validator;

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            IMapper mapper, IValidator<CustomerRegisterDTO> validator  )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        

        public async Task<Result<CustomerViewDTO>> GetByEmailAsync(string email)
        {
            var customer = (await _unitOfWork.Customers.GetByEmailAsync(email))
                .ToResultOrNotFound("Customer not found");

            if (customer.IsFailed)
            {
                return customer.ToResult();
            }

            return Result.Ok(_mapper.Map<CustomerViewDTO>(customer));
        }

        public async Task<Result<CustomerViewDTO>> Login(CustomerLoginDTO loginDTO)
        {
            var customerResult = (await _unitOfWork.Customers.GetByEmailAsync(loginDTO.Email))
                .ToResultOrNotFound("Invalid email or password!");

            // Not found any account with email
            if (customerResult.IsFailed)
            {
                return customerResult.ToResult();
            }

            // Found an account
            var customer = customerResult.Value;

            // MOCK test
            bool isPasswordValid = string.Equals(loginDTO.Password, customer.PasswordHash);

            // Login Fail
            if (!isPasswordValid)
            {
                return Result.Fail<CustomerViewDTO>(new Error("Invalid email or password"));
            }

            // Login success
            var customerDto = _mapper.Map<CustomerViewDTO>(customer);
            return Result.Ok(customerDto);
        }

        public async Task<Result<CustomerViewDTO>> RegisterAsync(CustomerRegisterDTO registerDTO)
        {
            var validationResult = await _validator.ValidateAsync(registerDTO);

            if (!validationResult.IsValid)
            {
                // Chuyển đổi lỗi của FluentValidation sang lỗi của Result Pattern
                // Giả sử Result.Fail nhận vào string hoặc list error
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorMessage)).ToList();
                return Result.Fail(errors);
            }

            var existingCustomer = await _unitOfWork.Customers.GetByEmailAsync(registerDTO.Email);

            if (existingCustomer != null)
            {
                return Result.Fail(new Error("Account already exists, please login"));
            }

            // Add new account
            var newCustomer = _mapper.Map<Customer>(registerDTO);
            newCustomer.Id = Guid.NewGuid();
            newCustomer.CreatedAt = DateTime.UtcNow;
            newCustomer.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Customers.AddAsync(newCustomer);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok(_mapper.Map<CustomerViewDTO>(newCustomer));
        }

    }
}
