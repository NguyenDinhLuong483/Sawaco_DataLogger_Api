namespace SawacoApi.Intrastructure.Services.Stolens
{
    public class StolenLineService : IStolenLineService
    {
        public IMapper _mapper { get; set; }
        public IStolenLineRepository _stolenLineRepository { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public StolenLineService(IMapper mapper, IStolenLineRepository stolenLineRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _stolenLineRepository = stolenLineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StolenLineViewModel>> GetByDeviceId(string Id)
        {
            var source = await _stolenLineRepository.GetByDeviceIdAsync(Id) ?? throw new ResourceNotFoundException("Not found this logger!");
            var stolenLine = _mapper.Map<List<StolenLine>, List<StolenLineViewModel>>(source);
            return stolenLine;
        }

        public async Task<List<StolenLineViewModel>> GetByDate(string id, DateTime startDate, DateTime endDate)
        {
            var source = await _stolenLineRepository.GetByDateAsync(id, startDate, endDate) ?? throw new ResourceNotFoundException("Not found this logger!");
            var stolenLine = _mapper.Map<List<StolenLine>, List<StolenLineViewModel>>(source);
            return stolenLine;
        }

        public async Task<bool> DeleteByDeviceId(string deviceId)
        {
            var stolenLine = await _stolenLineRepository.GetByDeviceIdAsync(deviceId) ?? throw new ResourceNotFoundException("Not found this logger!");
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
