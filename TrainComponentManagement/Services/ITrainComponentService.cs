using TrainComponentManagement.Models;

namespace TrainComponentManagement.Services;

public interface ITrainComponentService
{
    // Gets a paginated and potentially filtered list of components
    Task<(IEnumerable<TrainComponentDto> Components, int TotalItems)> GetAllComponentsAsync(int pageNumber, int pageSize, string? searchTerm);

    // Gets a single component by ID
    Task<TrainComponentDto?> GetComponentByIdAsync (int id);

    // Creates a new component
    Task<TrainComponentDto> CreateComponentAsync(CreateTrainComponentDto createDto);
    
    Task<TrainComponentDto> UpdateComponentAsync(int id, UpdateTrainComponentDto updateDto);

    // Updates the quantity of a component
    Task<bool> UpdateComponentQuantityAsync(int id, UpdateQuantityDto quantityDto);

    Task<bool> DeleteComponentAsync(int id);
}