using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Input.Client;
using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Client;
using WarehouseManagementWeb.Application.Interfaces.Services.Client;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Services.Client
{
    /// <summary>
    /// Класс реализует методы сервиса клиентов.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="clientRepository">Репозиторий клиентов.</param>
        /// <param name="logger">Логгер.</param>
        public ClientService(IClientRepository clientRepository,
            ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<ClientListByStatusOutput> GetClientsAsync()
        {
            try
            {
                IEnumerable<ClientOutput> clients = await _clientRepository.GetClientsAsync();

                if (clients is null || !clients.Any())
                {
                    return new ClientListByStatusOutput();
                }

                List<ClientOutput> activeClients = new List<ClientOutput>(clients.Count(
                    c => c.ClientStatusEnum == DirectoryStatusEnum.Active));

                List<ClientOutput> archivedClients = new List<ClientOutput>(clients.Count(
                   c => c.ClientStatusEnum == DirectoryStatusEnum.Archived));

                foreach (var client in clients)
                {
                    switch (client.ClientStatusEnum)
                    {
                        case DirectoryStatusEnum.Active:
                            activeClients.Add(client);
                            break;

                        case DirectoryStatusEnum.Archived:
                            archivedClients.Add(client);
                            break;
                    }
                }

                ClientListByStatusOutput result = new ClientListByStatusOutput
                {
                    ActiveClients = activeClients.OrderByDescending(c => c.Id).ToList(),
                    ArchivedClients = archivedClients.OrderByDescending(c => c.Id).ToList()
                };

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ClientOutput>> GetActiveClientsAsync()
        {
            try
            {
                IEnumerable<ClientOutput> result = await _clientRepository.GetActiveClientsAsync();

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<ClientOutput?> GetClientByIdAsync(int clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id клиента. " +
                                                        $"ClientId: {clientId}.");
                }

                ClientOutput? result = await _clientRepository.GetClientByIdAsync(clientId);

                if (result is null)
                {
                    return new ClientOutput();
                }

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task CreateClientAsync(CreateClientInput createClientInput)
        {
            try
            {
                if (createClientInput is null)
                {
                    throw new InvalidOperationException("Недопустимые данные клиента.");
                }

                bool isClientNameExist = await _clientRepository.CheckClientExistsByNameAsync(createClientInput.Name!);

                if (isClientNameExist)
                {
                    throw new InvalidOperationException(
                        $"Клиент с наименованием: '{createClientInput.Name}' уже существует в системе.");
                }

                ClientEntity entity = new ClientEntity
                {
                    Name = createClientInput.Name!,
                    Address = createClientInput.Address!
                };

                await _clientRepository.CreateClientAsync(entity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task UpdateClientAsync(UpdateClientInput updateClientInput)
        {
            try
            {
                if (updateClientInput is null)
                {
                    throw new InvalidOperationException("Недопустимые данные клиента.");
                }

                bool isClientNameExist = await _clientRepository.CheckClientExistsByNameAndIdAsync(
                    updateClientInput.Id, updateClientInput.Name!);

                if (isClientNameExist)
                {
                    throw new InvalidOperationException(
                        $"Клиент с наименованием: '{updateClientInput.Name}' уже существует в системе.");
                }

                ClientEntity entity = new ClientEntity
                {
                    Id = updateClientInput.Id,
                    Name = updateClientInput.Name!,
                    Address = updateClientInput.Address!
                };

                await _clientRepository.UpdateClientAsync(entity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task ChangeStatusClientAsync(ChangeStatusClientInput changeStatusClientInput)
        {
            try
            {
                if (changeStatusClientInput is null)
                {
                    throw new InvalidOperationException("Недопустимые данные клиента.");
                }

                await _clientRepository.ChangeStatusClientAsync(changeStatusClientInput.ClientId,
                    changeStatusClientInput.ClientStatusEnum);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task RemoveClientAsync(int clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id клиента. " +
                                                        $"ClientId: {clientId}.");
                }

                await _clientRepository.RemoveClientAsync(clientId);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}