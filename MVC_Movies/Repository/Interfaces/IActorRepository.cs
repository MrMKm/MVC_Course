﻿using MVC_Movies.Models;
using MVC_Movies.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Repository.Interfaces
{
    public interface IActorRepository 
    {
        public Task<List<Actor>> GetActors(ActorFilters filters);
        public Task<Actor> GetActorByID(int ActorID);
        public Task<Actor> CreateActor(Actor actor);
        public Task<Actor> UpdateActor(Actor actor);
        public Task DeleteActor(int ActorID);
        public Task<Actor> GetActorWithRates(int ActorID);
        public Task<bool> RateActor(ActorRate rate);
    }
}
