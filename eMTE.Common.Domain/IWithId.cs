using System;

namespace eMTE.Common.Domain
{
    /// <summary>
    /// Represents identifier contract
    /// </summary>
    public interface IWithId
    {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        Guid Id { get; set; }
    }
}
