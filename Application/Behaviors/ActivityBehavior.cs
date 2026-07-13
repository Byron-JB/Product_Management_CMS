using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors
{
    // Records create/update/delete activities automatically.
    public class ActivityBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ApplicationDbContext _db;
        private readonly IActivityRepository _activityRepo;
        private readonly ILogger<ActivityBehavior<TRequest, TResponse>> _logger;

        public ActivityBehavior(ApplicationDbContext db, IActivityRepository activityRepo, ILogger<ActivityBehavior<TRequest, TResponse>> logger)
        {
            _db = db;
            _activityRepo = activityRepo;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var typeName = typeof(TRequest).Name; // e.g., CreateProductCommand

            var isCreate = typeName.StartsWith("Create", StringComparison.OrdinalIgnoreCase);
            var isUpdate = typeName.StartsWith("Update", StringComparison.OrdinalIgnoreCase);
            var isDelete = typeName.StartsWith("Delete", StringComparison.OrdinalIgnoreCase);

            string entityName = null;
            object entityObj = null;
            int? entityId = null;
            string beforeJson = null;

            try
            {
                if (isUpdate || isDelete)
                {
                    // Try to find an Id property on the request
                    var idProp = typeof(TRequest).GetProperties().FirstOrDefault(p => string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase));
                    if (idProp != null)
                    {
                        var idVal = idProp.GetValue(request);
                        if (idVal is int iid) entityId = iid;
                    }

                    // If the request contains an entity property, capture its type name
                    var entityProp = typeof(TRequest).GetProperties().FirstOrDefault(p => p.PropertyType.Namespace == "Domain.Entities");
                    if (entityProp != null)
                    {
                        entityObj = entityProp.GetValue(request);
                        entityName = entityProp.PropertyType.Name;
                    }

                    // If we have an entityId, load current state
                    if (entityId.HasValue && string.IsNullOrEmpty(entityName))
                    {
                        // Try to detect entity set by convention from request type name (e.g., UpdateProductCommand -> Product)
                        var inferred = InferEntityNameFromRequest(typeName);
                        entityName = inferred;
                    }

                    if (!string.IsNullOrEmpty(entityName) && entityId.HasValue)
                    {
                        var entityType = Type.GetType($"Domain.Entities.{entityName}, {typeof(Domain.Entities.Product).Assembly.GetName().Name}");
                        if (entityType != null)
                        {
                            var found = await _db.FindAsync(entityType, new object[] { entityId.Value }, cancellationToken);
                            if (found != null)
                            {
                                beforeJson = JsonSerializer.Serialize(found, new JsonSerializerOptions { WriteIndented = false });
                            }
                        }
                    }
                }

                var response = await next();

                // After handler, capture after-state
                string afterJson = null;
                if (isCreate)
                {
                    // Try to use response if it is an entity
                    if (response != null && response.GetType().Namespace == "Domain.Entities")
                    {
                        entityName = response.GetType().Name;
                        afterJson = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = false });
                    }
                    else
                    {
                        // Possibly the request contained the entity
                        var entityProp = typeof(TRequest).GetProperties().FirstOrDefault(p => p.PropertyType.Namespace == "Domain.Entities");
                        if (entityProp != null)
                        {
                            entityObj = entityProp.GetValue(request);
                            entityName = entityProp.PropertyType.Name;
                            afterJson = JsonSerializer.Serialize(entityObj, new JsonSerializerOptions { WriteIndented = false });
                        }
                    }
                }
                else if (isUpdate)
                {
                    // Use entityObj if available
                    if (entityObj != null)
                    {
                        afterJson = JsonSerializer.Serialize(entityObj, new JsonSerializerOptions { WriteIndented = false });
                    }
                    else if (entityId.HasValue && !string.IsNullOrEmpty(entityName))
                    {
                        var entityType = Type.GetType($"Domain.Entities.{entityName}, {typeof(Domain.Entities.Product).Assembly.GetName().Name}");
                        if (entityType != null)
                        {
                            var found = await _db.FindAsync(entityType, new object[] { entityId.Value }, cancellationToken);
                            if (found != null)
                            {
                                afterJson = JsonSerializer.Serialize(found, new JsonSerializerOptions { WriteIndented = false });
                            }
                        }
                    }
                }

                // For delete operations, afterJson remains null

                if (isCreate || isUpdate || isDelete)
                {
                    var activity = new Activity
                    {
                        strEntityName = entityName ?? typeName,
                        iEntityId = entityId,
                        strAction = isCreate ? "Create" : isUpdate ? "Update" : "Delete",
                        iPerformedBy = null,
                        strDataBefore = beforeJson,
                        strDataAfter = afterJson,
                        dtTimestamp = DateTime.UtcNow
                    };

                    try
                    {
                        await _activityRepo.CreateAsync(activity, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to save activity for {Type}", typeName);
                    }
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string InferEntityNameFromRequest(string requestTypeName)
        {
            // e.g., UpdateProductCommand -> Product
            if (requestTypeName.StartsWith("Create") || requestTypeName.StartsWith("Update") || requestTypeName.StartsWith("Delete"))
            {
                var rest = requestTypeName.Substring(requestTypeName.IndexOfAny(new[] { 'C', 'U', 'D' }) + 1);
                // Very naive: remove leading action and trailing 'Command'
            }
            // Fallback: try to remove prefix 'Create'/'Update'/'Delete' and suffix 'Command'
            var name = requestTypeName;
            foreach (var p in new[] { "Create", "Update", "Delete" })
            {
                if (name.StartsWith(p)) name = name.Substring(p.Length);
            }
            if (name.EndsWith("Command")) name = name.Substring(0, name.Length - "Command".Length);
            return name;
        }
    }
}
