using Application.Interface.Service;
using Application.Interface.UnitOfWork;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class DestinationService : IDestinationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DestinationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Destination destination)
        {
            destination.Id = Guid.NewGuid();
            destination.UpdatedAt = DateTime.UtcNow;
            destination.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Destinations.AddAsync(destination);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var destination = (_unitOfWork.Destinations.GetByIdAsync(id)).Result;
            if (destination != null)
            {
                await _unitOfWork.Destinations.DeleteAsync(destination);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Destination>> GetAllAsync(int? pageIndex = null, int? pageSize = null)
        {
            return await _unitOfWork.Destinations.GetAllAsync(pageIndex, pageSize);
        }

        public async Task<Destination?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Destinations.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Destination>> SearchByAddress(string address)
        {
            return await _unitOfWork.Destinations.SearchByAddress(address);
        }

        public async Task UpdateAsync(Destination destination)
        {
            await _unitOfWork.Destinations.UpdateAsync(destination);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
