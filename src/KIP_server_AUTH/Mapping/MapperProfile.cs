// <copyright file="MapperProfile.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using AutoMapper;
using KIP_auth_mode.Mapping.Converters;
using KIP_auth_mode.Models.KHPI;
using KIP_auth_mode.Models.KIP;

namespace KIP_auth_mode.Mapping
{
    /// <summary>
    /// Building of the profile KIP model from the KhPI.
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            this.CreateMap<PersonalInformationKHPI, PersonalInformation>()
                .ConvertUsing<PersonalInformationKHPIToKIPConverter>();
        }
    }
}
