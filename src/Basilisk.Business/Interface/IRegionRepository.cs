using System;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Interface;

public interface IRegionRepository
{
    List<Region> GetAllRegions();

    Region? GetRegionById(long id);
    Region AddRegion(Region region);
    Region UpdateRegion(Region region);
    bool DeleteRegion(long id);

    IEnumerable<Region> SearchRegionByCity(string city); 
}
