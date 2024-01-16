using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMVCFramework.DTO.ApiDTO;
using WebAppMVCFramework.Models;

namespace WebAppMVCFramework.App_Start
{

    /// <summary>
    /// Custom Mapper con l' uso della libreria AutoMapper, questo automatizza il processo di mapping tra oggetti di tipo diverso 
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerApiDTO>();
            Mapper.CreateMap<Movie, MovieApiDTO>();
            Mapper.CreateMap<MembershipType, MembershipTypeApiDTO>();
            Mapper.CreateMap<Genre, GenreApiDTO>();

            Mapper.CreateMap<CustomerApiDTO, Customer>().ForMember(x => x.Id, c => c.Ignore());
            Mapper.CreateMap<MovieApiDTO, Movie>().ForMember(x => x.Id, c => c.Ignore());
            



        }
    }
}