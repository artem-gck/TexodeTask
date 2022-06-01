namespace TexodeTaskWin.Service.Model
{
    /// <summary>
    /// Card model.
    /// </summary>
    public class CardModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public byte[] Photo { get; set; }
    }
}
