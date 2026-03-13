using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Application.Dto.Output.MeasureUnit;
using WarehouseManagementWeb.Application.Interfaces.Repositories.MeasureUnit;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;
using WarehouseManagementWeb.Infrastructure.Data;

namespace WarehouseManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория единиц измерений.
    /// </summary>
    public class MeasureUnitRepository : IMeasureUnitRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс контекста.</param>
        public MeasureUnitRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<MeasureUnitOutput>> GetMeasureUnitsAsync()
        {
            IEnumerable<MeasureUnitOutput> result = await _db.MeasureUnits
            .AsNoTracking()
            .Select(mu => new MeasureUnitOutput
            {
                Id = mu.Id,
                Title = mu.Title,
                MeasureUnitStatusEnum = mu.MeasureUnitStatusEnum
            })
            .OrderByDescending(mu => mu.Id)
            .ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MeasureUnitOutput>> GetActiveMeasureUnitsAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<MeasureUnitOutput?> GetMeasureUnitByIdAsync(int measureUnitId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<bool> CheckMeasureUnitExistsByIdAndTitleAsync(int measureUnitId, string measureUnitTitle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<bool> CheckMeasureUnitExistsByTitleAsync(string measureUnitTitle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task CreateMeasureUnitAsync(MeasureUnitEntity measureUnitEntity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task UpdateMeasureUnitAsync(MeasureUnitEntity measureUnitEntity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task ChangeStatusMeasureUnitAsync(int measureUnitId, DirectoryStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task RemoveMeasureUnitAsync(int measureUnitId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
