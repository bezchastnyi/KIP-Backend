// <copyright file="CurrentRank.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace KIP_server_Auth.Models.KIP
{
    /// <summary>
    /// Current Rank KIP.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CurrentRank
    {
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the id of student.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets the fio of student.
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// Gets or sets the group of student.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the full form of rank mark.
        /// </summary>
        public float? FullRankMark { get; set; }

        /// <summary>
        /// Gets or sets the short form of rank mark.
        /// </summary>
        public float? ShortRankMark { get; set; }

        /// <summary>
        /// Gets or sets the formula of rank calculating.
        /// </summary>
        public string RankFormula { get; set; }
    }
}
