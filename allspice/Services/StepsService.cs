using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class StepsService
  {
    private readonly StepsRepository _repo;
    public StepsService(StepsRepository repo)
    {
      _repo = repo;
    }
    internal List<Step> Get()
    {
      return _repo.GetAll();
    }

    internal Step Create(Step newStep)
    {
      return _repo.Create(newStep);
    }

    internal Step Get(int id)
    {
      Step found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal void RemoveStep(int id, string userId)
    {
      Step step = Get(id);
      if (step.CreatorId != userId)
      {
        throw new Exception("You are not allowed to remove this ingredient");
      }
      _repo.RemoveStep(id);
    }
  }
}