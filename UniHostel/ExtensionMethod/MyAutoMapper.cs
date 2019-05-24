using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniHostel.Models;

namespace UniHostel.ExtensionMethod
{
    public static class MyAutoMapper
    {
        static MapperConfiguration config = null;
        static IMapper mapper = null;

        public static IMapper GetInstance()
        {
            if(config == null || mapper == null)
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Renter, UserRenterViewModel>();
                    cfg.CreateMap<UserRenterViewModel, Renter>();
                    cfg.CreateMap<UserRenterViewModel, User>();
                    cfg.CreateMap<User, UserRenterViewModel>();
                });
                mapper = config.CreateMapper();
            }
            return mapper;
        }

    }
}