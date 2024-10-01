﻿using SawacoApi.Domain.Models;

namespace SawacoApi.Domain.Repositories
{
    public interface IStolenLineRepository
    {
        public Task<List<StolenLine>> GetByLoggerIdAsync(string loggerId);
        public Task<List<StolenLine>> GetByDateAsync(string id, DateTime startDate, DateTime endDate);
        public bool DeleteAsync(List<StolenLine> stolenLine);
        public StolenLine AddStolenLine(StolenLine stolenLine);
    }
}
