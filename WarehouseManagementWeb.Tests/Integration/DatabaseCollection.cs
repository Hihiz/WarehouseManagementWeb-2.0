namespace WarehouseManagementWeb.Tests.Integration
{
    /// <summary>
    /// Класс коллекции тестов, использующий один общий экземпляр фикстуры.
    /// </summary>
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}
