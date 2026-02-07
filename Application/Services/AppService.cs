using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entidades;
using Application.Exceptions;
using Application.DTOs;
using Persistence.Repositories;

namespace Application.Services
{
    public class AppService
    {
        private readonly AppRepository _repo;

        public AppService(AppRepository repo)
        {
            _repo = repo;
        }
        public async Task SetReturnRate(ReturnRateDto dto)
        {
            if (dto.MinReturnRate > dto.MaxReturnRate)
            {
                throw new InvalidReturnRateException("La tasa minima no puede ser mayor que la tasa maxima");
            }

            _repo.SetReturnRate(new AppSettings { MaxReturnRate = dto.MaxReturnRate, MinReturnRate = dto.MinReturnRate });
        }

        public async Task<ReturnRateDto> GetReturnRate()
        {
            AppSettings settings = await _repo.GetReturnRate();
            return new ReturnRateDto { MaxReturnRate = settings.MaxReturnRate, MinReturnRate = settings.MinReturnRate };
        }

        public async Task UpdateReturnRate(ReturnRateDto dto)
        {
            await _repo.UpdateReturnRate(new AppSettings { MinReturnRate = dto.MinReturnRate, MaxReturnRate = dto.MaxReturnRate });
        }
    }
}
