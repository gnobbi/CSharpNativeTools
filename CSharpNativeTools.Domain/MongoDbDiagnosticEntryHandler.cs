using GeneratedDI;
using MongoDB.Driver;

namespace CSharpNativeTools.Domain;

public class MongoDbDiagnosticEntryHandler : IDiagnosticEntryHandler
{
    private IMongoCollection<DiagnosticEntiyBase> _collection;

    public MongoDbDiagnosticEntryHandler()
    {
        var client = new MongoClient("mongodb://localhost:5000/");
        var database = client.GetDatabase("CSharpNativeTools");
        _collection = database.GetCollection<DiagnosticEntiyBase>("Diagnostics");
    }

    public async Task HandleDiagnosticEntryAsync(DiagnosticEntiyBase entity)
    {
        await _collection.InsertOneAsync(entity);
    }
}
