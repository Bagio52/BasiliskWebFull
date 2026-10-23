using System;
using System.Linq;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;

namespace Basilisk.Business.Repository;

public class RegionRepository : IRegionRepository
{
    private readonly BasiliskTFContext _context;

    public RegionRepository(BasiliskTFContext context)
    {
        _context =  context;
    }

    public List<Region> GetAllRegions()
    {
        return _context.Regions.ToList();
    }

    public Region? GetRegionById(long id)
    {
        return _context.Regions.Find(id);
    }

    public Region AddRegion(Region region)
    {
        _context.Regions.Add(region);

        _context.SaveChanges();

        return region;
    }

    public Region UpdateRegion(Region region)
    {
        _context.Regions.Update(region);
        _context.SaveChanges();

        return region;
    }

    public bool DeleteRegion(long id)
    {
        var region = _context.Regions.Find(id);

        if (region == null)
        {
            return false;
        }

        _context.Regions.Remove(region);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<Region> SearchRegionByCity(string city)
    {
        return _context.Regions
            .Where(r => r.City.Contains(city))
            .ToList();
    }
}
