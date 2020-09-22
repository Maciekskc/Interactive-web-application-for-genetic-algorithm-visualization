using Application.Dtos.Logs.Responses;
using Application.Utilities;

namespace Application.Interfaces
{
    public interface ILogsService
    {
        /// <summary>
        /// Zwraca listę informacji o plikach z logami.
        /// </summary>
        /// <returns></returns>
        ServiceResponse<GetLogsFilesResponse> GetLogsFiles();

        /// <summary>
        /// Zwraca plik o nazwie zadanej w parametrze w postaci tablicy bajtów.
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        ServiceResponse<byte[]> GetLogsFileBytes(string logName);

        /// <summary>
        /// Zwraca wszystkie pliki w formacie .zip w postaci tablicy bajtów.
        /// </summary>
        /// <returns></returns>
        ServiceResponse<byte[]> GetAllLogsFilesBytes();

        /// <summary>
        /// Zwraca treść pliku o nazwie zadanej w parametrze w postaci tablicy stringów.
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        ServiceResponse<string[]> GetLogsFileText(string logName);
    }
}