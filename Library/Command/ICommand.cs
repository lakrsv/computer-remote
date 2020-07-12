using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Response;

namespace Computer_Wifi_Remote.Command
{
    public interface ICommand<out T>
    {
        string Name { get; }

        bool HasPayload { get; }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <returns>Whether the command executed successfully</returns>
        IResponsePayload<T> Execute(Request request);
    }
}
