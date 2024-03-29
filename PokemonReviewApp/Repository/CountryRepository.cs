﻿using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public bool CreateCountry(CountryModel country)
        {
            _context.Add(country);

            return Save();
        }

        public bool DeleteCountry(CountryModel country)
        {
            _context.Remove(country);
            return Save();
        }

        public ICollection<CountryModel> GetCountriesFromDatabase()
        {
            return _context.Countries.ToList();
        }

        public CountryModel GetCountryById(int id)
        {
            return _context.Countries.Where(p => p.Id == id).FirstOrDefault();
        }

        public CountryModel GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<OwnerModel> GetOwnersFromCountry(int countryId)
        {
            return _context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(CountryModel country)
        {
            _context.Update(country);
            return Save();
        }
    }
}
