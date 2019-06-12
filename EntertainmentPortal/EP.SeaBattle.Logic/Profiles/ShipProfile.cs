﻿using System;
using AutoMapper;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Models;

namespace EP.SeaBattle.Logic.Profiles
{
    public class ShipProfile : Profile
    {
        public ShipProfile()
        {
            CreateMap<ShipDb, Ship>().ReverseMap();
        }
    }
}
