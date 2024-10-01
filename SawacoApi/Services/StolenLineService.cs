using AutoMapper;
using Microsoft.Extensions.Logging;
using SawacoApi.Domain.Models;
using SawacoApi.Domain.Persistances.Exceptions;
using SawacoApi.Domain.Repositories;
using SawacoApi.Domain.Services;
using SawacoApi.Resources.StolenLine;

namespace SawacoApi.Services
{
    public class StolenLineService : IStolenLineService
    {
        public IMapper _mapper {  get; set; }
        public IStolenLineRepository _stolenLineRepository { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public StolenLineService(IMapper mapper, IStolenLineRepository stolenLineRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _stolenLineRepository = stolenLineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StolenLineViewModel>> GetByLoggerId(string loggerId)
        {
            var source = await _stolenLineRepository.GetByLoggerIdAsync(loggerId) ?? throw new ResourceNotFoundException("Not found this logger!");
            var stolenLine = _mapper.Map<List<StolenLine>, List<StolenLineViewModel>>(source);  
            return stolenLine;
        }

        public async Task<List<StolenLineViewModel>> GetByDate(string id, DateTime startDate, DateTime endDate)
        {
            var source = await _stolenLineRepository.GetByDateAsync(id, startDate, endDate) ?? throw new ResourceNotFoundException("Not found this logger!");
            var stolenLine = _mapper.Map<List<StolenLine>, List<StolenLineViewModel>>(source);
            return stolenLine;
        }

        public async Task<bool> DeleteByLoggerId(string loggerId)
        {
            var stolenLine = await _stolenLineRepository.GetByLoggerIdAsync(loggerId) ?? throw new ResourceNotFoundException("Not found this logger!");
            _stolenLineRepository.DeleteAsync(stolenLine);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteByDate(string id, DateTime startDate, DateTime endDate)
        {
            var stolenLine = await _stolenLineRepository.GetByDateAsync(id, startDate, endDate) ?? throw new ResourceNotFoundException("Not found this logger!");
            _stolenLineRepository.DeleteAsync(stolenLine);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> AddNewStolenLine(AddStolenLineViewModel stolenLineViewModel)
        {
            var mapping = _mapper.Map<StolenLine>(stolenLineViewModel);
            _stolenLineRepository.AddStolenLine(mapping);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
