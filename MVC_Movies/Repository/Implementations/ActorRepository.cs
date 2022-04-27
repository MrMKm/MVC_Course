using Microsoft.EntityFrameworkCore;
using MVC_Movies.Data;
using MVC_Movies.Models;
using MVC_Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Repository.Implementations
{
    public class ActorRepository : IActorRepository
    {
        private readonly RepositoryContext repositoryContext;

        public ActorRepository(RepositoryContext _repositoryContext)
        {
            repositoryContext = _repositoryContext;
        }

        public async Task<Actor> CreateActor(Actor actor)
        {
            try
            {
                await repositoryContext.Actor.AddAsync(actor);
                await repositoryContext.SaveChangesAsync();

                return actor;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteActor(int ActorID)
        {
            try
            {
                var actor = await repositoryContext.Actor.FirstOrDefaultAsync(a => a.ID.Equals(ActorID));

                if (actor == null)
                    throw new NullReferenceException("Actor with ID not found");

                repositoryContext.Actor.Remove(actor);
                await repositoryContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Actor> GetActorByID(int ActorID)
        {
            var actor = await repositoryContext.Actor.FirstOrDefaultAsync(a => a.ID.Equals(ActorID));

            if (actor == null)
                return default;

            return actor;
        }

        public async Task<List<Actor>> GetActors()
        {
            var actors = await repositoryContext.Actor.ToListAsync();

            if (!actors.Any())
                return default;

            return actors;
        }

        public async Task<Actor> UpdateActor(Actor actor)
        {
            var dbActor = await repositoryContext.Actor.FindAsync(actor.ID);

            if (dbActor == null)
                throw new NullReferenceException("Actor not found");

            dbActor.Name = actor.Name;
            dbActor.BirthDate = actor.BirthDate;
            dbActor.OscarWon = actor.OscarWon;

            await repositoryContext.SaveChangesAsync();

            return dbActor;
        }
    }
}
