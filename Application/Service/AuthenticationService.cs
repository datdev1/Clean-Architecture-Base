using Application.Common.Errors;
using Application.Common.Extenstions;
using Application.DTOs.Customer;
using Application.Interface.Service;
using AutoMapper;
using Domain.Model;
using FluentResults;
using Infrastructure.Interface.UnitOfWork;


namespace Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(Customer customer)
        {
            // Validate before add
            customer.Id = Guid.NewGuid();
            customer.UpdatedAt = DateTime.Now;
            customer.CreatedAt = DateTime.Now;
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = (_unitOfWork.Customers.GetByIdAsync(id)).Result;
            if (customer != null)
            {
                await _unitOfWork.Customers.DeleteAsync(customer);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(int? pageIndex = null, int? pageSize = null)
        {
            return await _unitOfWork.Customers.GetAllAsync(pageIndex, pageSize);
        }

        public async Task<Result<CustomerViewDTO>> GetByEmailAsync(string email)
        {
            var customer = await _unitOfWork.Customers.GetByEmailAsync(email);
            if(customer == null)
            {
                return Result.Fail(new NotFoundError("Customer not found"));
            }
            return Result.Ok(_mapper.Map<CustomerViewDTO>(customer));
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Customers.GetByIdAsync(id);
        }

        public Task UpdateAsync(Customer destination)
        {
            throw new NotImplementedException();
        }



        public async Task<Result<CustomerViewDTO>> Login(CustomerLoginDTO loginDTO)
        {
            var customerResult = (await _unitOfWork.Customers.GetByEmailAsync(loginDTO.Email))
                .ToResultOrNotFound("User not found, please Register");

            if (customerResult.IsFailed)
            {
                return Result.Fail<CustomerViewDTO>(customerResult.Errors);
            }

            var customer = customerResult.Value;

            // 3. KIỂM TRA MẬT KHẨU (BẮT BUỘC)
            // TODO: For modck
            bool isPasswordValid = string.Equals(loginDTO.Password, customer.PasswordHash);

            if (!isPasswordValid)
            {
                return Result.Fail<CustomerViewDTO>(new Error("Invalid email or password"));
            }

            // 4. Nếu mật khẩu đúng -> Map sang DTO và trả về
            var customerDto = _mapper.Map<CustomerViewDTO>(customer);
            return Result.Ok(customerDto);
        }

        public async Task<Result<CustomerViewDTO>> RegisterAsync(CustomerRegisterDTO registerDTO)
        {
            var customerRsult = (await _unitOfWork.Customers.GetByEmailAsync(registerDTO.Email))
                    .ToResultOrNotFound("");

            if (customerRsult.IsSuccess)
            {
                return Result.Fail(new Error("Account exist, please login"));
            }

            var newCustomer = _mapper.Map<Customer>(registerDTO);
            newCustomer.Id = Guid.NewGuid();
            newCustomer.CreatedAt = DateTime.Now;
            newCustomer.UpdatedAt = DateTime.Now;
            await _unitOfWork.Customers.AddAsync(newCustomer);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(_mapper.Map<CustomerViewDTO>(newCustomer));
        }

    }
}
