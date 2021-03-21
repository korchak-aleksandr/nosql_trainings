using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoSqlStress.WebApi.Dto;

namespace NoSqlStress.WebApi.Controllers
{
    /// <summary>
    /// Общий интерфейс для тестирования нагрузки БД.
    /// </summary>
    public interface IStressController
    {
        /// <summary>
        /// Запись сущности.
        /// </summary>
        Task<IActionResult> WriteEntity(TestEntity entity);

        /// <summary>
        /// Чтение сущности.
        /// </summary>
        Task<IActionResult> ReadEntity(Guid id);
    }
}
