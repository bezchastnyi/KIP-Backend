// <copyright file="PersonalInformationConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_Backend.Extensions;
using KIP_Backend.Models.KIP.Auth;
using KIP_server_Auth.Models.KhPI;

namespace KIP_server_Auth.Mapping.Converters
{
    /// <summary>
    /// Convert KhPI PersonalInformation model to the KIP model.
    /// </summary>
    public class PersonalInformationConverter : ITypeConverter<PersonalInformationKhPI, PersonalInformation>
    {
        /// <summary>
        /// Convert model of PersonalInformation from KhPI to KIP.
        /// </summary>
        /// <param name="source">The model of KhPI PersonalInformation.</param>
        /// <param name = "destination">The model of KIP PersonalInformation.</param>
        /// <param name= "context">The context. </param>
        /// <returns>Object of the KIP PersonalInformation model.</returns>
        public PersonalInformation Convert(
            PersonalInformationKhPI source, PersonalInformation destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new PersonalInformation()
            {
                StudentId = ConvertExtensions.StringToInt(source.st_cod),
                LastName = source.fam,
                FirstName = source.imya,
                Patronymic = source.otch,
                Course = ConvertExtensions.StringToInt(source.kurs),
                GroupId = ConvertExtensions.StringToInt(source.gid),
                Group = source.grupa,
                FacultyId = ConvertExtensions.StringToInt(source.fid),
                Faculty = source.fakultet,
                CathedraId = ConvertExtensions.StringToInt(source.kid),
                Cathedra = source.kafedra,
                Specialization = source.specialization,
                Specialty = source.speciality,
                StudyingProgram = source.osvitprog,
                StudyingLevel = source.train_level,
                StudyingForm = source.train_form,
                BudgetForm = source.oplata,
            };
        }
    }
}
