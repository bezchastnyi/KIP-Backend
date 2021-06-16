// <copyright file="CurrentRank.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_AUTH.Models.KIP
{
    /// <summary>
    /// Current Rank KIP.
    /// </summary>
    public class CurrentRank
    {
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>rank.</value>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the id of student.
        /// </summary>
        /// <value>id of student.</value>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets the fio of student.
        /// </summary>
        /// <value>fio of student.</value>
        public string FIO { get; set; }

        /// <summary>
        /// Gets or sets the group of student.
        /// </summary>
        /// <value>group of student.</value>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the full form of rank mark.
        /// </summary>
        /// <value>full form of rank mark.</value>
        public float? FullRankMark { get; set; }

        /// <summary>
        /// Gets or sets the short form of rank mark.
        /// </summary>
        /// <value>short form of rank mark.</value>
        public float? ShortRankMark { get; set; }

        /// <summary>
        /// Gets or sets the formula of rank calculating.
        /// </summary>
        /// <value>formula of rank calculating.</value>
        public string RankFormula { get; set; }
    }
}
