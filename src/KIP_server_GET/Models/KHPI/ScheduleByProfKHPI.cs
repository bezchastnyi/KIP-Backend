// <copyright file="ScheduleByProfKHPI.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

namespace KIP_server_GET.Models.KHPI
{
    /// <summary>
    /// Schedule by teachers KhPI.
    /// </summary>
    public class ScheduleByProfKHPI
    {
        /// <summary>
        /// Gets or sets the Monday.
        /// </summary>
        /// <value>Monday.</value>
        public Monday Monday { get; set; }

        /// <summary>
        /// Gets or sets the Tuesday.
        /// </summary>
        /// <value>Tuesday.</value>
        public Tuesday Tuesday { get; set; }

        /// <summary>
        /// Gets or sets the Wednesday.
        /// </summary>
        /// <value>Wednesday.</value>
        public Wednesday Wednesday { get; set; }

        /// <summary>
        /// Gets or sets the Thursday.
        /// </summary>
        /// <value>Thursday.</value>
        public Thursday Thursday { get; set; }

        /// <summary>
        /// Gets or sets the Friday.
        /// </summary>
        /// <value>Friday.</value>
        public Friday Friday { get; set; }
    }
}
