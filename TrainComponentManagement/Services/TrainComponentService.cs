using Microsoft.EntityFrameworkCore;
using TrainComponentManagement.Data;
using TrainComponentManagement.Entities;
using TrainComponentManagement.Models;

namespace TrainComponentManagement.Services;

public class TrainComponentService: ITrainComponentService
{
    private readonly TrainComponentDbContext _context;

    public TrainComponentService(TrainComponentDbContext context)
    {
        _context = context;
    }
    
    public async Task<(IEnumerable<TrainComponentDto> Components, int TotalItems)> GetAllComponentsAsync(int pageNumber,
        int pageSize, string? searchTerm)
    {
        var query = _context.TrainComponents.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string lowerSearchTerm = searchTerm.ToLower();
            query = query.Where(tc => tc.Name.ToLower().Contains(lowerSearchTerm) || tc.UniqueNumber.ToLower().Contains(lowerSearchTerm));
        }
        var totalItems = await query.CountAsync();
        var componentsData = await query.OrderBy(tc => tc.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var componentDtos = componentsData.Select(tc => new TrainComponentDto { Id = tc.Id, Name = tc.Name, UniqueNumber = tc.UniqueNumber, CanAssignQuantity = tc.CanAssignQuantity, Quantity = tc.Quantity }).ToList();
        return (componentDtos, totalItems);
    }

    public async Task<TrainComponentDto?> GetComponentByIdAsync(int id)
    {
        var componentEntity = await _context.TrainComponents
            .AsNoTracking() 
            .Where(tc => tc.Id == id) 
            .FirstOrDefaultAsync(); 

        
        if (componentEntity == null)
        {
            return null;
        }
        return new TrainComponentDto
        {
            Id = componentEntity.Id,
            Name = componentEntity.Name,
            UniqueNumber = componentEntity.UniqueNumber,
            CanAssignQuantity = componentEntity.CanAssignQuantity,
            Quantity = componentEntity.Quantity
        };
    }

    public async Task<TrainComponentDto> CreateComponentAsync(CreateTrainComponentDto createDto)
    {
        bool uniqueNumberExists = await _context.TrainComponents
            .AnyAsync(tc => tc.UniqueNumber == createDto.UniqueNumber);
        
        if (uniqueNumberExists)
        {
            throw new InvalidOperationException($"Component with Unique Number '{createDto.UniqueNumber}' already exists.");
        }
        
        var componentEntity = new TrainComponent
        {
            Name = createDto.Name,
            UniqueNumber = createDto.UniqueNumber,
            CanAssignQuantity = createDto.CanAssignQuantity,
            Quantity = createDto.CanAssignQuantity ? 0 : null
        };
        
        _context.TrainComponents.Add(componentEntity);
        await _context.SaveChangesAsync();
        
        return new TrainComponentDto
        {
            Id = componentEntity.Id, 
            Name = componentEntity.Name,
            UniqueNumber = componentEntity.UniqueNumber,
            CanAssignQuantity = componentEntity.CanAssignQuantity,
            Quantity = componentEntity.Quantity
        };
    }
    
    public async Task<TrainComponentDto> UpdateComponentAsync(int id, UpdateTrainComponentDto updateDto)
    {
        var componentToUpdate = await _context.TrainComponents.FindAsync(id);

        if (componentToUpdate == null)
        {
            throw new KeyNotFoundException($"Component with Id {id} not found for update.");
        }
        
        componentToUpdate.Name = updateDto.Name;
        componentToUpdate.CanAssignQuantity = updateDto.CanAssignQuantity;

        if (componentToUpdate.CanAssignQuantity)
        {
            if(updateDto.Quantity.HasValue)
            {
                componentToUpdate.Quantity = updateDto.Quantity.Value;
            }
            else
            {
                //if quantity alowed but not provided
                 componentToUpdate.Quantity = 0;
            }
        }
        else
        {
            componentToUpdate.Quantity = null;
        }
        
        await _context.SaveChangesAsync(); // Persist changes to the database
        
        return new TrainComponentDto
        {
            Id = componentToUpdate.Id,
            Name = componentToUpdate.Name,
            UniqueNumber = componentToUpdate.UniqueNumber, // Not updated by this method
            CanAssignQuantity = componentToUpdate.CanAssignQuantity,
            Quantity = componentToUpdate.Quantity
        };
    }

    public async Task<bool> UpdateComponentQuantityAsync(int id, UpdateQuantityDto quantityDto)
    {
        await Task.CompletedTask; 
        throw new NotImplementedException("Functionality to update component quantity is not yet implemented.");
    }
    
    public async Task<bool> DeleteComponentAsync(int id)
    {
        var componentToDelete = await _context.TrainComponents.FindAsync(id);
        
        if (componentToDelete == null)
        {
            throw new KeyNotFoundException($"Component with Id {id} not found for deletion.");
        }
        
        _context.TrainComponents.Remove(componentToDelete); 
        await _context.SaveChangesAsync(); 

        return true;
    }
}