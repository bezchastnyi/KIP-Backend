// <copyright file="PersonalInformationKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_server_AUTH.Extensions;
using KIP_server_AUTH.Models.KHPI;
using KIP_server_AUTH.Models.KIP;

namespace KIP_server_AUTH.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP PersonalInformation model from the KhPI PersonalInformation.
    /// </summary>
    public class PersonalInformationKHPIToKIPConverter : ITypeConverter<PersonalInformationKHPI, PersonalInformation>
    {
        /// <summary>
        /// Convert model of PersonalInformation from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of PersonalInformation of KIP model.
        /// </returns>
        /// <param name="source">Model of PersonalInformation KHPI.</param>
        /// <param name = "destination">Model of PersonalInformation KIP.</param>
        /// <param name= "context">The context. </param>
        public PersonalInformation Convert(PersonalInformationKHPI source, PersonalInformation destination, ResolutionContext context)
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
