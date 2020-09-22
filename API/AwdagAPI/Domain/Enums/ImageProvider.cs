namespace Domain.Models.Entities
{
    /// <summary>
    /// Pochodzenie obrazka
    /// </summary>
    public enum ImageProvider
    {
        /// <summary>
        /// Obraz prywatny wrzucony przez użytkownika.
        /// Nie jest wyświetlany w wyszukiwarce
        /// </summary>
        Private,

        /// <summary>
        /// Obraz, który pochodzi z AdobeStock.
        /// Jest wyświetlany w wyszukiwarce
        /// </summary>
        AdobeStock,

        /// <summary>
        /// Obraz wprowadzony przez Administratora.
        /// Jest wyświetlany w wyszukiwarce.
        /// </summary>
        Admin,

        /// <summary>
        /// Obraz wprowadzony przez kontrybutora.
        /// Jest wyświetlany w wyszukiwarce.
        /// </summary>
        Contributor
    }
}