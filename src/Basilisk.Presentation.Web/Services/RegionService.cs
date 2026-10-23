using System;
using System.Linq;
using Basilisk.Business.Interface;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels.Region;

namespace Basilisk.Presentation.Web.Services;

public class RegionService
{
    private readonly IRegionRepository _regionRepository;

    public RegionService(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    public RegionIndexViewModel GetRegion()
    {
        return new RegionIndexViewModel()
        {
            Regions = _regionRepository.GetAllRegions()
                .Select(r => new RegionViewModel()
                {
                    Id = r.Id,
                    City = r.City,
                    Remark = r.Remark
                }).ToList()
        };
    }

    public void InsertRegion(RegionViewModel vm)
    {
        _regionRepository.AddRegion(new Region
        {
            City = vm.City,
            Remark = vm.Remark
        });
    }

    public RegionViewModel? GetRegionById(long id)
    {
        var region = _regionRepository.GetRegionById(id);

        if (region == null)
        {
            return null;
        }

        return new RegionViewModel
        {
            Id = region.Id,
            City = region.City,
            Remark = region.Remark
        };
    }

    public bool UpdateRegion(RegionViewModel vm)
    {
        var region = _regionRepository.GetRegionById(vm.Id);

        if (region == null)
        {
            return false;
        }

        region.City = vm.City;
        region.Remark = vm.Remark;

        _regionRepository.UpdateRegion(region);

        return true;
    }

    public bool DeleteRegion(long id)
    {
        return _regionRepository.DeleteRegion(id);
    }

    public RegionIndexViewModel SearchRegionByCity(string searchCity)
    {
        return new RegionIndexViewModel
        {
            Regions = _regionRepository
                .SearchRegionByCity(searchCity)
                .Select(r => new RegionViewModel
                {
                    Id = r.Id,
                    City = r.City,
                    Remark = r.Remark
                }).ToList()
        };
    }
}
