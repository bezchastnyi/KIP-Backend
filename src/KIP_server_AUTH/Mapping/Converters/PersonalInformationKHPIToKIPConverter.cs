// <copyright file="PersonalInformationKHPIToKIPConverter.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using KIP_auth_mode.Models.KHPI;
using KIP_auth_mode.Models.KIP;

namespace KIP_auth_mode.Mapping.Converters
{
    /// <summary>
    /// Building of the KIP audience model from the KhPI audience.
    /// </summary>
    public class PersonalInformationKHPIToKIPConverter : ITypeConverter<PersonalInformationKHPI, PersonalInformation>
    {
        /// <summary>
        /// Convert model of audience from KHPI to KIP.
        /// </summary>
        /// <returns>
        /// Object of audience of model audience KIP.
        /// </returns>
        /// <param name="source">Model of audience KHPI.</param>
        /// <param name = "destination">Model of audience KIP.</param>
        /// <param name= "context">The context. </param>
        public PersonalInformation Convert(PersonalInformationKHPI source, PersonalInformation destination, ResolutionContext context)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new PersonalInformation()
            {
                StudentId = System.Convert.ToInt32(source.st_cod),
                LastName = source.fam,
                FirstName = source.imya,
                Patronymic = source.otch,
                Course = System.Convert.ToInt32(source.kurs),
                GroupId = System.Convert.ToInt32(source.gid),
                Group = source.grupa,
                FacultyId = System.Convert.ToInt32(source.fid),
                Faculty = source.fakultet,
                CathedraId = System.Convert.ToInt32(source.kid),
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
